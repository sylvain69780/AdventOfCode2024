using System.Text.RegularExpressions;

internal class Solution
{
    private string test;
    private string[] patterns;
    private string[] designs;

    public Solution(string test)
    {
        var parts = test.Replace("\r",string.Empty).Split("\n\n");
        patterns = parts[0].Split(", ");
        designs = parts[1].Split('\n');
    }

    internal string Run()
    {
        var score = 0;
        for (var i = 0 ; i<designs.Length; i++)
        {
            var design = designs[i];
            var regex = "^("+string.Join("|", patterns)+")+$";
            if (Regex.IsMatch(design, regex))
                score++;
        }
        return score.ToString();
    }
}