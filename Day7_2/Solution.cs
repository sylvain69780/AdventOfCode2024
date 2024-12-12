// See https://aka.ms/new-console-template for more information

using System.Globalization;

internal class Solution
{
    private readonly (long result, long[] nums)[] equations;

    public Solution(string input)
    {
        equations = input.Replace("\r", string.Empty)
            .Split('\n')
            .Select(x => x.Split(": "))
            .Select(x => (result: long.Parse(x[0]), nums: x[1].Split(' ').Select(y => long.Parse(y)).ToArray()))
            .ToArray();
    }

    internal long Run()
    {
        var score = 0L;
        foreach ((long result, long[] nums) in equations)
        {
            var dfs = new Stack<(int level, long value,string op)>();
            dfs.Push((0, nums[0], "+"));
            dfs.Push((0, nums[0], "*"));
            dfs.Push((0, nums[0], "|"));
            while (dfs.Count > 0)
            {
                var rec = dfs.Pop();
                var level = rec.level + 1;
                var value = rec.op[^1] == '+' ? rec.value + nums[level] : 
                    rec.op[^1] == '*' ? rec.value * nums[level]
                    : long.Parse(rec.value.ToString() + nums[level].ToString());
    
                if (level == nums.Length - 1)
                {
                    if (value == result)
                    {
                        //Console.WriteLine(value);
                        score += value;
                        break;
                    }
                }
                else
                {
                    dfs.Push((level, value, rec.op + '+'));
                    dfs.Push((level, value, rec.op + '*'));
                    dfs.Push((level, value, rec.op + '|'));
                }
            }
        }
        return score;
    }
}