using System.Text.RegularExpressions;

internal class Solution
{
    private string[] patterns;
    private string[] designs;

    public Solution(string test)
    {
        var parts = test.Replace("\r", string.Empty).Split("\n\n");
        patterns = parts[0].Split(", ");
        designs = parts[1].Split('\n');
    }

    internal string Run()
    {
        var score = 0L;
        var cache = new Dictionary<string, long>();
        for (var i = 0; i < designs.Length; i++)
        {
            var design = designs[i];
            score += FindPatterns(design, cache);
        }
        return score.ToString();       
    }

    private long FindPatterns(string design, Dictionary<string, long> cache)
    {
        if (cache.ContainsKey(design))
        {
            return cache[design];
        }

        var score = 0L;
        foreach (var item in patterns)
        {
            if (design == item) {
                score += 1;
                continue;
            }
            if (design.StartsWith(item) && design.Length > item.Length)
            {
                var remainingDesign = design[item.Length..];
                score += FindPatterns(remainingDesign, cache);
            }
        }

        cache[design] = score;
        return score;
    }
}