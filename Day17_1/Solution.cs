
using System.Text.RegularExpressions;

internal class Solution
{
    private readonly long[] registers;
    private readonly int[] prog;
    public Solution(string test)
    {
        var parts = test.Replace("\r",string.Empty).Split("\n\n");
        registers = parts[0].Split("\n")
            .Select(x => long.Parse(Regex.Match(x, @"Register .: (\d+)").Groups[1].Value)).ToArray();
        prog = parts[1].Replace("Program: ",string.Empty).Split(",").Select(x => int.Parse(x)).ToArray();
    }

    internal string Run()
    {
        var pointer = 0;
        var output = new List<long>();
        while (pointer < prog.Length)
        {
            var inst = prog[pointer];
            var op = prog[pointer + 1];
            if ( inst == 0) // adv
            {
                registers[0] = registers[0] >> (int)Combo(op);
                pointer += 2;
            }
            if (inst == 1) // bxl
            {
                registers[1] = registers[1] ^ op;
                pointer += 2;
            }
            if (inst == 2) // bst
            {
                registers[1] = Combo(op) % 8;
                pointer += 2;
            }
            if (inst == 3) // jnz
            {
                if (registers[0] != 0)
                {
                    pointer = op;
                }
                else 
                    pointer += 2;
            }
            if (inst == 4) // bxc
            {
                registers[1] = registers[1] ^ registers[2];
                pointer += 2;
            }
            if (inst == 5) // out
            {
                output.Add(Combo(op) % 8);
                pointer += 2;
            }
            if (inst == 6) // bdv
            {
                registers[1] = registers[0] >> (int)Combo(op);
                pointer += 2;
            }
            if (inst == 7) // cdv
            {
                registers[2] = registers[0] >> (int)Combo(op);
                pointer += 2;
            }


        }

        return string.Join(',',output);
    }

    private long Combo(int op) => op switch
    {
        <= 3 => op,
        _ => registers[op-4]
    };
}