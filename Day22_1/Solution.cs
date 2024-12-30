

using System.Diagnostics;

internal class Solution
{
    private long[] secrets;

    public Solution(string test)
    {
        secrets = test.Replace("\r", "").Split("\n").Select(long.Parse).ToArray();
    }

    internal long Solve()
    {
        var score = 0L;

        var count = 2000 * 3;
        for (var i = 0; i < count; i++)
        {
            var newSecrets = (i % 3) switch {
                0 => secrets.Select(secret => ((secret * 64) ^ secret ) % 16777216).ToArray(),
                1 => secrets.Select(secret => (((secret) / 32) ^ secret ) % 16777216).ToArray(),
                2 => secrets.Select(secret => ((secret * 2048) ^ secret ) % 16777216).ToArray(),
                _ => throw new InvalidOperationException()
            };
            secrets = newSecrets;
        }

            foreach (var secret in secrets)
            {
                Debug.WriteLine(secret);
            }
            Debug.WriteLine("");
        score = secrets.Sum();
        return score;
    }
}