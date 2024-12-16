
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

internal class Solution
{
    private string[] map;
    private (int x, int y, char c) start;
    private (int x, int y, char c) end;

    readonly string directions = ">^<v";
    private readonly (int dx, int dy)[] displ = [(1, 0), (0, -1), (-1, 0), (0, 1)];

    public Solution(string test)
    {
        map = test.Replace("\r", string.Empty).Split('\n');
        start = map.SelectMany((row, y) => row.Select((c, x) => (x, y, c))).Where(r => r.c == 'S').Single();
        end = map.SelectMany((row, y) => row.Select((c, x) => (x, y, c))).Where(r => r.c == 'E').Single();
    }

    void Visu(HashSet<(int x, int y)> visited)
    {
        string NL = Environment.NewLine; // shortcut
        string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
        string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
        string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
        string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
        string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
        string MAGENTA = Console.IsOutputRedirected ? "" : "\x1b[95m";
        string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
        string GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
        string BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
        string NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
        string UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
        string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";
        string REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
        string NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

        Console.WriteLine();
        foreach (var item in map.Select((row, idx) => (row, y: idx)))
        {
            var sb = new StringBuilder();
            for (var x = 0; x < item.row.Length;x++)
            {
                if ( visited.Contains((x, item.y)))
                    sb.Append($"{RED}O{NORMAL}");
                else
                    sb.Append(item.row[x]); 
            }
            Console.WriteLine(sb.ToString());
        }
    }
    internal long Run()
    {
        var score = 0L;
        var queue = new Queue<(int x, int y, int cost, int dir)>();
        queue.Enqueue((start.x, start.y, 0, 0));
        var graph = new Dictionary<(int x, int y, int dir), long>();
        while (queue.Count > 0)
        {
            var pos = queue.Dequeue();
            if (graph.TryGetValue((pos.x, pos.y, pos.dir), out var node))
            {
                if (pos.cost < node)
                    graph[(pos.x, pos.y, pos.dir)] = pos.cost;
                else
                    continue;
            }
            else
                graph.Add((pos.x, pos.y, pos.dir), pos.cost);
            if ((pos.x, pos.y) == (end.x, end.y))
                continue;
            for (int i = -1; i < 2; i++)
            {
                var newdir = (pos.dir + i + 4) % 4;
                var disp = displ[newdir];
                var next = (x: pos.x + disp.dx, y: pos.y + disp.dy, dir: newdir);
                var c = map[next.y][next.x];
                if (c == '#')
                    continue;
                var newp = (next.x, next.y, cost: pos.cost + 1 + (i == 0 ? 0 : 1000), next.dir);
                queue.Enqueue(newp);
            }
            // Visu(graph.Keys.Select(p => (p.x,p.y)).ToHashSet());
        }

        var sol = graph.Keys.Where(p => (p.x, p.y) == (end.x, end.y)).Select(p => graph[p]).Min();

        var starts = graph.Keys.Where(p => (p.x, p.y) == (end.x, end.y) && graph[p] == sol);
        var backtrack = new Queue<(int x, int y, int dir)>();
        var path = new List<(int x, int y)>() { (end.x, end.y) };
        foreach (var item in starts)
        {
            backtrack.Enqueue(item);
        }
        while (backtrack.Count > 0)
        {
            var pos = backtrack.Dequeue();
            if ((pos.x, pos.y) == (start.x, start.y))
                continue;
            for (int i = -1; i < 2; i++)
            {
                var newdir = (pos.dir + i + 4) % 4;
                var disp = displ[pos.dir];
                var next = (x: pos.x - disp.dx, y: pos.y - disp.dy, newdir);
                if (graph.TryGetValue(next, out var cost))
                {
                    if (graph[pos] - 1 - (i==0 ? 0 : 1000) == cost)
                    {
                        path.Add((next.x, next.y));
                        backtrack.Enqueue(next);
                    }
                }
            }
//            Visu([.. path]);
//            Console.ReadKey();
        }
        Visu(path.ToHashSet());
        score = path.Distinct().Count();
        return score;
    }
}