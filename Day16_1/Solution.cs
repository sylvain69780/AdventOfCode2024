
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;

internal class Solution
{
    private string[] map;
    private (int x, int y, char c) start;
    private (int x, int y, char c) end;

    readonly string directions = ">^<v";
    private readonly (int dx, int dy)[] displ = [(1, 0), (0, -1), (-1, 0), (0, 1)]; 

    public Solution(string test)
    {
        map = test.Replace("\r",string.Empty).Split('\n');
        start = map.SelectMany((row, y) => row.Select((c, x) => (x, y, c))).Where(r => r.c == 'S').Single();
        end = map.SelectMany((row, y) => row.Select((c, x) => (x, y, c))).Where(r => r.c == 'E').Single();
    }

    void Visu(Dictionary<(int x, int y), long> visited)
    {
        Console.WriteLine();
        foreach (var item in map.Select((row,idx) => (row,y:idx)))
            Console.WriteLine(new string(item.row.Select((c,x) => visited.ContainsKey((x,item.y)) ? 'X': c).ToArray()));
    }
    internal long Run()
    {
        var score = 0L;
        var queue = new Queue<(int x, int y,int cost,int dir)>();
        queue.Enqueue((start.x, start.y,0, 0));
        var minscore = long.MaxValue;
        var explored = new Dictionary<(int x, int y), long>();
        while (queue.Count > 0)
        {
            // 87407
            var pos = queue.Dequeue();
            if (pos.cost > minscore)
                continue;
            if (explored.TryGetValue((pos.x, pos.y), out var value))
            {
                if (pos.cost > value)
                    continue;
            } else
            {
                explored.Add((pos.x, pos.y),pos.cost);
            }
            var list = new List<(int x, int y, int cost, int dir)>();
            for (int i = 0; i < 3; i++)
            {
                var newdir = (pos.dir + i - 1 + 4) % 4;
                var disp = displ[newdir];
                var next = (x:pos.x + disp.dx,y: pos.y + disp.dy);
                var c = map[next.y][next.x];
                if (c == '#')
                    continue;
                var newp = (next.x, next.y, cost:pos.cost +1 + (i == 1 ? 0 : 1000),dir:newdir);
                if (c == 'E')
                {
                    if (newp.cost < minscore)
                    {
                        minscore = newp.cost;
                        Visu(explored);
                        Console.WriteLine($"{queue.Count} {minscore}");
                        // Console.ReadKey();
                    }
                }
                else
                    list.Add(newp);
            }
            foreach (var item in list.OrderByDescending(i => Math.Abs(i.x - end.x) + Math.Abs(i.y - end.y)))
                queue.Enqueue(item);
        }
        score = minscore;
        return score;
    }
}