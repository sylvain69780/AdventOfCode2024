
using System.Text.RegularExpressions;

internal class Solution
{
    private string test;
    private ((long x, long y) A, (long x, long y) B, (long x, long y) P)[] machines;

    (long x, long y) Parse (string s)
    {
        var matche = Regex.Match(s, @"(\d+).+?(\d+)");
        return (long.Parse(matche.Groups[1].Value), long.Parse(matche.Groups[2].Value));   
    }
    (long x, long y) ParseP(string s)
    {
        var matche = Regex.Match(s, @"(\d+).+?(\d+)");
        return (10000000000000 + long.Parse(matche.Groups[1].Value), 10000000000000 + long.Parse(matche.Groups[2].Value));
    }

    public Solution(string test)
    {
        var records = test.Replace("\r", "").Split("\n\n");
        machines = records.Select(r => {
        var rr = r.Split("\n");
        return (A: Parse(rr[0]),B: Parse(rr[1]),P:  ParseP(rr[2]));
            }).ToArray();
    }

    internal long Run()
    {
        var score = 0L;

        foreach (var (A, B, P) in machines)
        {
            var (a, b, c, d, e, f) = (A.x, B.x, A.y, B.y, P.x, P.y);
            var (nA, nB) = ((e * d - b * f) / (a * d - b * c), (a * f - e * c) / (a * d - b * c));
            if ( nA * A.x + nB * B.x == P.x && nA * A.y + nB * B.y == P.y)
            score += 3 * nA + nB;
        }
        return score;
    }
}