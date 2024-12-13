
using System.Text.RegularExpressions;

internal class Solution
{
    private string test;
    private ((int x, int y) A, (int x, int y) B, (int x, int y) P)[] machines;

    (int x,int y) Parse (string s)
    {
        var matche = Regex.Match(s, @"(\d+).+?(\d+)");
        return (int.Parse(matche.Groups[1].Value),int.Parse(matche.Groups[2].Value));   
    }

    public Solution(string test)
    {
        var records = test.Replace("\r", "").Split("\n\n");
        machines = records.Select(r => {
        var rr = r.Split("\n");
        return (A: Parse(rr[0]),B: Parse(rr[1]),P:  Parse(rr[2]));
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