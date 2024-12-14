
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
        var (qx, qy) = (size.w / 2, size.h / 2);
        for (var i = 0; i < 100; i++)
        {
            for (var r = 0; r < robots.Length; r++)
            {
                var (x, y, dx, dy) = robots[r];
                robots[r] = ((x + dx + size.w) % size.w, (y + dy + size.h) % size.h, dx, dy);
            }
        }
        var (q1, q2, q3, q4) = (
            robots.Where(r => r.x < qx && r.y < qy).Count(),
            robots.Where(r => r.x < qx && r.y > qy).Count(),
            robots.Where(r => r.x > qx && r.y < qy).Count(),
            robots.Where(r => r.x > qx && r.y > qy).Count()
           );
        return q1*q2*q3*q4;
    }
}