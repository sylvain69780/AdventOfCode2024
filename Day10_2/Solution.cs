
using System.Diagnostics.CodeAnalysis;

internal class Solution
{
    private string[] map;

    public Solution(string test)
    {
        map = test.Replace("\r", string.Empty).Split("\n");
    }

    private int sample(int x, int y) => x >= 0 && y >= 0 && x < map[0].Length && y < map.Length ? map[y][x] - '0' : -1;

    internal int Run()
    {
        var starts = map.SelectMany((line,y) => line.Select((c,x) => (c,x,y)))
            .Where(a => a.c == '0')
            .Select(a => (h:(int)(a.c - '0'),a.x,a.y))
            .ToArray();
        var scores = new List<int>();
        foreach (var start in starts)
        {
            var dfs = new Stack<(int h,int x,int y)>();
            var score = 0;
            dfs.Push(start);
            while (dfs.Count > 0)
            {
                var (h, x, y) = dfs.Pop();
                if ( h == 9)
                {
                    score++;
                    continue;
                }
                if (sample(x - 1, y) - h == 1)
                    dfs.Push((h + 1,x-1,y));
                if (sample(x + 1, y) - h == 1)
                    dfs.Push((h + 1, x + 1, y));
                if (sample(x , y-1) - h == 1)
                    dfs.Push((h + 1, x, y-1));
                if (sample(x, y + 1) - h == 1)
                    dfs.Push((h + 1, x, y + 1));
            }
            scores.Add(score);
        }

        return scores.Sum();
    }
}