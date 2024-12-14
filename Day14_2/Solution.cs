
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
        var (qx, qy) = (size.w / 2, size.h / 2);
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
            //            if (q1 == q3 && q2 == q4)
            if (distance < robots.Length * 30)
            {
                var rob = new Stack<(long y, Stack<long> x)>(robots
                    .GroupBy(r => r.y, r => r.x)
                    .Select(g => (y: g.Key, x: new Stack<long>(g.GroupBy(x => x).Select(g => g.Key).OrderByDescending(x => x))))
                    .OrderByDescending(g => g.y));
                var robline = rob.Pop();
                var sb = new StringBuilder();
                for (var row = 0; row < size.h; row++)
                {
                    if (row != robline.y)
                    {
                        Console.WriteLine(new String('.', size.w));
                        continue;
                    }
                    var robcol = robline.x.Pop();
                    sb.Clear();
                    for (var col = 0; col < size.w; col++)
                    {
                        if (col != robcol)
                        {
                            sb.Append('.');
                            continue;
                        }
                        sb.Append('*');
                        if (robline.x.Count > 0)
                            robcol = robline.x.Pop();
                    }
                    Console.WriteLine(sb.ToString());
                    if (rob.Count > 0)
                        robline = rob.Pop();
                }
                Console.WriteLine();
                Console.ReadKey();
                // break;
            }
        }
        return score;
    }
}