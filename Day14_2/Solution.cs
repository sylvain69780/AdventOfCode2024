
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

internal class Solution
{
    private (long x, long y, long dx, long dy)[] robots;

    (int w, int h) size;

    public Solution(string test, (int w, int h) s)
    {
        robots = test.Replace("\r", string.Empty).Split("\n").Select(l => Regex.Match(l, @"p=([\d\-]+),([\d\-]+) v=([\d\-]+),([\d\-]+)"))
            .Select(l => (long.Parse(l.Groups[1].Value), long.Parse(l.Groups[2].Value), long.Parse(l.Groups[3].Value), long.Parse(l.Groups[4].Value)))
            .ToArray();
        size = s;
    }

    internal long Run()
    {
        var score = 0;
        while (true)
        {
            score++;
            for (var r = 0; r < robots.Length; r++)
            {
                var (x, y, dx, dy) = robots[r];
                robots[r] = ((x + dx + size.w) % size.w, (y + dy + size.h) % size.h, dx, dy);
            }
            var center = (x: robots.Select(r => r.x).Sum() / robots.Length, y: robots.Select(r => r.y).Sum() / robots.Length);
            var distance = robots.Select(r => Math.Abs(r.x - center.x)).Sum() + robots.Select(r => Math.Abs(r.y - center.y)).Sum();
            if (distance < robots.Length * 30)
            {
                var rob = new Stack<(long row, Stack<long> cols)>(robots
                    .GroupBy(r => r.y, r => r.x)
                    .Select(g => (row: g.Key, new Stack<long>(g.GroupBy(x => x).Select(g => g.Key).OrderByDescending(x => x))))
                    .OrderByDescending(g => g.row));
                var robline = rob.Pop();
                for (var row = 0; row < size.h; row++)
                {
                    if (row == robline.row)
                    {
                        var robcol = robline.cols.Pop();
                        var sb = new StringBuilder();
                        for (var col = 0; col < size.w; col++)
                        {
                            if (col == robcol)
                            {
                                sb.Append('*');
                                if (robline.cols.Count > 0)
                                    robcol = robline.cols.Pop();
                            }
                            else
                                sb.Append('.');
                        }
                        if (rob.Count > 0)
                            robline = rob.Pop();
                        Console.WriteLine(sb.ToString());
                    }
                    else
                        Console.WriteLine(new String('.', size.w));
                }
                Console.WriteLine();
                Console.ReadKey();
                break;
            }
        }
        return score;
    }
}