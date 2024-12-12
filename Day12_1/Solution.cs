
internal class Solution
{
    private string[] map;

    public Solution(string test)
    {
        map = test.Replace("\r", string.Empty).Split("\n");
    }

    char Sample(int x, int y) => x < 0 || y < 0 || x >= map[0].Length || y >= map.Length ? ' ' : map[y][x];

    private (int dx, int dy)[] directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];
    internal long Run()
    {
        var regions = new List<(char plant, List<(int x, int y, int b)> items)>();
        var visited = new HashSet<(int x, int y)>();

        while (visited.Count != map.Length * map[0].Length)
        {
            var start = Enumerable.Range(0, map.Length).SelectMany(y => Enumerable.Range(0, map[0].Length).Select(x => (x, y))).First(p => !visited.Contains(p));
            var bfs = new Queue<(int x, int y)>();
            bfs.Enqueue(start);
            regions.Add((Sample(start.x,start.y), new List<(int x, int y, int b)>()));

            while (bfs.Count > 0)
            {
                var p = bfs.Dequeue();
                if (visited.Contains((p.x, p.y)))
                    continue;
                visited.Add((p.x, p.y));
                var c = Sample(p.x, p.y);
                var borders = 0;
                foreach (var (dx, dy) in directions)
                    if (Sample(p.x + dx, p.y + dy) != c)
                        borders++;
                    else
                        bfs.Enqueue((p.x + dx, p.y + dy));
                regions[^1].items.Add((p.x, p.y, borders));
            }
        }
        return regions.Select(r => r.items.Count * r.items.Select(p => p.b).Sum()).Sum();
    }
}