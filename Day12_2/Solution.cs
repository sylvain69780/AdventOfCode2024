
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
        var regions = new List<(char plant, List<(int x, int y, (bool left, bool right, bool up, bool down) b)> items)>();
        var visited = new HashSet<(int x, int y)>();

        while (visited.Count != map.Length * map[0].Length)
        {
            var start = Enumerable.Range(0, map.Length).SelectMany(y => Enumerable.Range(0, map[0].Length).Select(x => (x, y))).First(p => !visited.Contains(p));
            var bfs = new Queue<(int x, int y)>();
            bfs.Enqueue(start);
            regions.Add((Sample(start.x, start.y), new List<(int x, int y, (bool left, bool right, bool up, bool down) borders)>()));
            while (bfs.Count > 0)
            {
                var p = bfs.Dequeue();
                if (visited.Contains((p.x, p.y)))
                    continue;
                visited.Add((p.x, p.y));
                var c = Sample(p.x, p.y);
                var borders = (
                    left: Sample(p.x - 1, p.y) != c,
                    right: Sample(p.x + 1, p.y) != c,
                    up: Sample(p.x, p.y - 1) != c,
                    down: Sample(p.x, p.y + 1) != c
                    );
                foreach (var (dx, dy) in directions)
                    if (Sample(p.x + dx, p.y + dy) == c)
                        bfs.Enqueue((p.x + dx, p.y + dy));
                regions[^1].items.Add((p.x, p.y, borders));
            }
        }
        var faces = new List<int>();
        for (var i = 0; i < regions.Count; i++)
        {
            var count = 0;
            foreach (var p in regions[i].items)
            {
                var borders = p.b;
                if (borders.left && !regions[i].items.Exists(q => q.x == p.x && q.y == p.y + 1 && q.b.left))
                    count++;
                if (borders.right && !regions[i].items.Exists(q => q.x == p.x && q.y == p.y + 1 && q.b.right))
                    count++;
                if (borders.up && !regions[i].items.Exists(q => q.y == p.y && q.x == p.x - 1 && q.b.up))
                    count++;
                if (borders.down && !regions[i].items.Exists(q => q.y == p.y && q.x == p.x + 1 && q.b.down))
                    count++;
            }
            faces.Add(count);

        }
        return regions.Select((r, i) => r.items.Count * faces[i]).Sum();
    }
}