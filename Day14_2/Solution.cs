
using System.ComponentModel.Design;
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
                var rob = robots
                    .GroupBy(r => r.y, r => r.x)
                    .Select(g => (y: g.Key, x: g.GroupBy(x => x).Select(g => g.Key).OrderBy(x => x).ToArray()))
                    .OrderBy(g => g.y)
                    .ToArray();
                var rowidx = 0;
                for (var row = 0; row < size.h; row++)
                {
                    if (row > rob.Length)
                        Console.WriteLine(new String('.', size.w));
                    if (row == rob[rowidx].y)
                    {
                        var robline = rob[rowidx++].x;
                        var sb = new StringBuilder();
                        var idx = 0;
                        for (var col = 0; col < size.w; col++)
                        {
                            if (idx >= robline.Length)
                                sb.Append('.');
                            else if (col == robline[idx])
                            {
                                sb.Append('*');
                                idx++;
                            }
                            else
                                sb.Append('.');
                        }
                        Console.WriteLine(sb.ToString());
                    }
                    else
                        Console.WriteLine(new String('.', size.w));
                }
                Console.WriteLine();
                Console.ReadKey();
                // break;
            }
        }
        return score;
    }
}