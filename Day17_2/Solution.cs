
using System.Text.RegularExpressions;

internal class Solution
{
    private readonly long[] regs;
    private readonly int[] prog;
    private readonly string prg;


    public Solution(string test)
    {
        var parts = test.Replace("\r", string.Empty).Split("\n\n");
        regs = parts[0].Split("\n")
            .Select(x => long.Parse(Regex.Match(x, @"Register .: (\d+)").Groups[1].Value)).ToArray();
        prog = parts[1].Replace("Program: ", string.Empty).Split(",").Select(x => int.Parse(x)).ToArray();
        prg = string.Join("",prog.Reverse().Select(x => x.ToString()));
    }

    void Debug(int pointer, List<int> output)
    {
        string NL = Environment.NewLine; // shortcut
        string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
        string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
        string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
        string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
        string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
        string MAGENTA = Console.IsOutputRedirected ? "" : "\x1b[95m";
        string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
        string GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
        string BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
        string NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
        string UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
        string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";
        string REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
        string NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

        List<int> nocombo = [1, 3, 4];
        Console.WriteLine();
        Console.WriteLine(string.Join(',', prog.Select((x, i) => i == pointer ? $"{RED}{Decode(x)}{NORMAL}" : i == pointer + 1 ? (nocombo.Contains(prog[pointer]) ? $"{GREEN}{x}{NORMAL}" : $"{GREEN}{DecodeOp(x)}{NORMAL}") : x.ToString())));
        Console.WriteLine($"A={regs[0]:B32} B={regs[1]:B32} C={regs[2]:B32}");
        Console.WriteLine(string.Join(',', output.Select(x => x.ToString())));
        Console.ReadKey();
    }

    string Decode(int instr) => instr switch
    {
        0 => "adv",
        1 => "bxl",
        2 => "bst",
        3 => "jnz",
        4 => "bxc",
        5 => "out",
        6 => "bdv",
        7 => "cdv",
        _ => throw new NotImplementedException()
    };

    string DecodeOp(int instr) => instr switch
    {
        < 4 => instr.ToString(),
        4 => "A",
        5 => "B",
        6 => "C",
        _ => throw new NotImplementedException()
    };

    IEnumerable<long> Solve(long sum,int power)
    {
        for (var i = 0;i<8;i++)
        {
            var partialSum = sum + (long)(i*Math.Pow(8, power));
            var query = Query(partialSum);
            query.Reverse();
            var str = string.Join("", query);
            if (str == prg)
                yield return partialSum;
            else if (prg.StartsWith(str.Substring(0, prg.Length-power)))
             {
                foreach (var item in Solve(partialSum,power-1))
                    yield return item;
            }
        }
    }

    public string Run()
    {
        return Solve(0,15).Min().ToString();
    }

    internal List<int> Query(long input)
    {
        regs[0] = input;
        regs[1] = 0;
        regs[2] = 0;
        var pointer = 0;
        var output = new List<int>();
        while (pointer < prog.Length)
        {
           // Debug(pointer, output);
            var inst = prog[pointer];
            var op = prog[pointer + 1];
            if (inst == 0) // adv
            {
                regs[0] = regs[0] >> (int)Combo(op);
                pointer += 2;
            }
            if (inst == 1) // bxl
            {
                regs[1] = regs[1] ^ op;
                pointer += 2;
            }
            if (inst == 2) // bst
            {
                regs[1] = Combo(op) % 8;
                pointer += 2;
            }
            if (inst == 3) // jnz
            {
                if (regs[0] != 0)
                {
                    pointer = op;
                }
                else
                    pointer += 2;
            }
            if (inst == 4) // bxc
            {
                regs[1] = regs[1] ^ regs[2];
                pointer += 2;
            }
            if (inst == 5) // out
            {
                output.Add((int)(Combo(op) % 8));
                pointer += 2;
            }
            if (inst == 6) // bdv
            {
                regs[1] = regs[0] >> (int)Combo(op);
                pointer += 2;
            }
            if (inst == 7) // cdv
            {
                regs[2] = regs[0] >> (int)Combo(op);
                pointer += 2;
            }


        }

        return output;
    }

    private long Combo(int op) => op switch
    {
        <= 3 => op,
        _ => regs[op - 4]
    };

    long ToNumber(IEnumerable<int> list) => list.Select((x, i) => ((long)x) << (i * 3)).Sum();

    //List<int> ToProgram(long number)
    //{
    //    var output = new List<int>();
    //    for (int i = 0; i < 64 / 3; i++)
    //    {
    //        output.Add((int)(number & 3));
    //        number >>= 3;
    //    }
    //    return output;
    //}
}