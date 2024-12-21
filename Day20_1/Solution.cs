internal class Solution
{
    private string test;
    private string[] map;
    private (int x, int y) start;
    private (int x, int y) end;

    (int dx, int dy)[] directions = [(0, 1), (1, 0), (0, -1), (-1, 0)];

    public Solution(string test)
    {
        map = test.Replace("\r", "").Split('\n');
        start = map.SelectMany((row, y) => row.Select((cell, x) => (cell, x, y))).Where(p => p.cell == 'S').Select(p => (p.x, p.y)).Single();
        end = map.SelectMany((row, y) => row.Select((cell, x) => (cell, x, y))).Where(p => p.cell == 'E').Select(p => (p.x, p.y)).Single();
    }

    char Sample((int x, int y) pos)
    {
        var (x, y) = pos;
        if (y < 0 || y >= map.Length || x < 0 || x >= map[0].Length)
            return '#';
        return map[y][x];
    }
    internal string Run()
    {
        var score = 0;
        var graph = new HashSet<(int x, int y)>() { start };
        var path = new List<(int x, int y)>() { start };
        var pos = start;
        while (pos != end)
        {           
            pos = directions.Select(d => (x:pos.x + d.dx,y: pos.y + d.dy)).Where(p => map[p.y][p.x] != '#').Where(p => !graph.Contains(p)).Single();
            graph.Add(pos);
            path.Add(pos);
        }
        for (int i = 0; i < path.Count; i++)
        {
            Console.WriteLine($"Index: {i}, Element: {path[i]}");
            foreach (var (dx, dy) in directions)
            {
                var next = (x: path[i].x + 2*dx, y: path[i].y + 2*dy);
                if (Sample(next) == '#')
                    continue;
                if (graph.Contains(next))
                    {
                        if ( path.IndexOf(next) - i - 2 >= 100)
                        {
                            score++;
                        }
                    }
            }
        }
        return score.ToString();
    }
}