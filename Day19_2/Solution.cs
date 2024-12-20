using System.Text.RegularExpressions;

internal class Solution
{
    private string test;
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
        for (var i = 0; i < designs.Length; i++)
        {
            var design = designs[i];
            score += FindPatterns(design);
        }
        return score.ToString();       
    }

    private long FindPatterns(string design)
    {
            var score = 0L;
            foreach (var item in patterns)
            {
                if (design == item) {
                    score+=1;
                    continue;
                }
                if (design.StartsWith(item) && design.Length > item.Length)
                {
                    var remainingDesign = design[item.Length..];
                    score += FindPatterns(remainingDesign);
                }
            }
            return score;
    }

    internal string Run2()
    {
        var score = 0L;
        var cache = new Dictionary<string, long>();
        for (var i = 0; i < designs.Length; i++)
        {
            var design = designs[i];
            var stack = new Stack<(int index, string guess)>();
            foreach (var item in patterns)
                stack.Push((0, item));
            while (stack.Count > 0)
            {
                var (index, guess) = stack.Pop();
                var remainingDesign = design[index..];
                if (remainingDesign == guess)
                {
                    score++;
                    cache[remainingDesign] = 1;
                    continue;
                }
                if (remainingDesign.StartsWith(guess))
                {
                    foreach (var item in patterns)
                    {
                        stack.Push((index + guess.Length, item));
                    }
                }
            }
            Console.WriteLine($"{score} {design}");
        }
        return score.ToString();
    }
}