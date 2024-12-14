
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
            var (q1, q2, q3, q4) = (
                robots.Where(r => r.x < qx && r.y < qy).Count(),
                robots.Where(r => r.x < qx && r.y > qy).Count(),
                robots.Where(r => r.x > qx && r.y < qy).Count(),
                robots.Where(r => r.x > qx && r.y > qy).Count()
               );
            var center = (x : robots.Select(r => r.x).Sum() / robots.Length, y: robots.Select(r => r.y).Sum() / robots.Length);
            var distance = robots.Select(r => Math.Abs(r.x - center.x)).Sum() + robots.Select(r => Math.Abs(r.y - center.y)).Sum();
            //            if (q1 == q3 && q2 == q4)
            if (distance < robots.Length*30)
            {
                for (var row = 0; row < size.h; row++)
                {
                    var rob = robots.Where(r => r.y == row).OrderBy(r => r.x).ToArray();
                    var sb = new StringBuilder();
                    var idx = 0;
                    for (var col = 0;col < size.w; col++)
                    {
                        if ( idx >= rob.Length)
                            sb.Append('.');
                        else if ( col == rob[idx].x )
                        {
                            sb.Append('*');
                            idx++;
                        } else
                            sb.Append('.');
                    }
                    Console.WriteLine(sb.ToString());
                }
                Console.WriteLine();
                Console.ReadKey();
                break;
            }
        }
        return score;
    }
}