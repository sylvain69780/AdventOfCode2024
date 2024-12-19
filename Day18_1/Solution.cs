

using System.Diagnostics;
using System.Text;
using System.Xml.Serialization;

internal class Solution
{
    private (long x, long y)[] fallingBlocks;

    public Solution(string test)
    {
        fallingBlocks = test.Replace("\r", string.Empty).Split("\n").Select(x => x.Split(",")).Select(a => (x: long.Parse(a[0]), y: long.Parse(a[1]))).ToArray();
    }

    void Visu(int size, Dictionary<(int x, int y), int> graph, int step)
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
        Console.WriteLine();
        foreach (var y in Enumerable.Range(0, size))
            Console.WriteLine(new string(Enumerable.Range(0, size)
                .SelectMany(
                x =>
                {
                    if (fallingBlocks.Take(step).Contains((x, y))) return $"{CYAN}#{NORMAL}";
                    else if (graph.ContainsKey((x, y))) return "O";
                    else return ".";
                }
                ).ToArray()));
        Console.ReadKey();
    }

    (int dx, int dy)[] directions = [(1, 0), (0, 1), (-1, 0), (0, -1)];

    char Sample((int x, int y) p, int step, int size)
    {
        if (p.x < 0 || p.y < 0 || p.x > size - 1 || p.y > size - 1)
            return '#';
        if (fallingBlocks.Take(step).Contains(p))
            return '#';
        else
            return '.';
    }
    internal string Run(int size)
    {

        var graph = new Dictionary<(int x, int y), int>();

        var start = (0, 0);
        var end = (size - 1, size - 1);

        var queue = new Queue<((int x, int y) p, int step)>();
        queue.Enqueue((start, 0));
        var score = 0;
        while (queue.Count > 0)
        {
            var (p, step) = queue.Dequeue();
            if (graph.TryGetValue((p.x, p.y), out var value))
            {
                if (value > step)
                    graph[(p.x, p.y)] = step;
                else
                    continue;
            }
            else
                graph.Add((p.x, p.y), step);
            if (p == end)
                break;
            foreach (var (dx, dy) in directions)
            {
                var np = (p.x + dx, p.y + dy);
                var tile = Sample(np, step + 1, size);
                if (tile == '.')
                {
                    queue.Enqueue((np, step + 1));
                }
            }
            Visu(size, graph,step);
        }
        score = graph.Keys.Where(n => n.x == size - 1 && n.y == size - 1).Select(n => graph[n]).Single();
        return score.ToString();
    }
}