// See https://aka.ms/new-console-template for more information


internal class Solution
{
    private int[][] locks;
    private int[][] keys;

    public Solution(string test)
    {

        var locksAndKeys = test.Replace("\r", "").Split("\n\n").Select(x => x.Split('\n'));
        locks = locksAndKeys.Where(x => x[0] == "#####").Select(
            x =>
            {
                var heights = new int[5] { 0, 0, 0, 0, 0 };
                for (int pin = 0; pin < 5; pin++)
                    for (int h = 0; h < 5; h++)
                    {
                        if (x[h+1][pin] == '#')
                            heights[pin] ++;
                    }
                return heights;
            }
            ).ToArray();
        keys = locksAndKeys.Where(x => x[0] == ".....").Select(
            x =>
            {
                var heights = new int[5] { 0, 0, 0, 0, 0 };
                for (int pin = 0; pin < 5; pin++)
                    for (int h = 0; h < 5; h++)
                    {
                        if (x[h + 1][pin] == '#')
                            heights[pin]++;
                    }
                return heights;
            }
            ).ToArray();
    }

    internal string Solve()
    {
        var count = 0;

        foreach (var key in keys)
        {
            foreach (var @lock in locks)
            {
                if (Enumerable.Range(0,5).All(i => key[i] + @lock[i] <= 5))
                    count++;
            }
        }

        return count.ToString();
    }
}