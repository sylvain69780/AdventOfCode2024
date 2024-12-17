
using System.Text.RegularExpressions;

internal class Solution
{
    private readonly long[] regs;
    private readonly int[] prog;
    public Solution(string test)
    {
        var parts = test.Replace("\r", string.Empty).Split("\n\n");
        regs = parts[0].Split("\n")
            .Select(x => long.Parse(Regex.Match(x, @"Register .: (\d+)").Groups[1].Value)).ToArray();
        prog = parts[1].Replace("Program: ", string.Empty).Split(",").Select(x => int.Parse(x)).ToArray();
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

    internal string Run()
    {
        var soluce = new Stack<int>();
        soluce.Push(0);
        while (soluce.Count < prog.Length)
        {
            var test = ToNumber(soluce.Reverse());
            var query = Query(test);
            var value = soluce.Pop();
            if (query.Count > soluce.Count && query[soluce.Count] == prog[soluce.Count])
            {
                soluce.Push(value);
                Console.WriteLine();
                Console.WriteLine(string.Join(',', prog.Select(x => x.ToString())));
                Console.WriteLine(string.Join(',', query.Select(x => x.ToString())));
                Console.WriteLine(string.Join(',', soluce.Reverse().Select(x => x.ToString())));
                Console.ReadKey();
                soluce.Push(0);
            }
            else if (value < 7)
            {
                value++;
                soluce.Push(value);
            }
            else
            {
                if (soluce.Count > 0)
                {
                    value = soluce.Pop();
                    value++;
                    soluce.Push(value);
                }
                else
                    break;
            }
        }

        return ToNumber(soluce.Reverse()).ToString();
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
//                        Debug(pointer,output);
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