
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

    void Visu(HashSet<(int x, int y)> visited)
    {
        Console.WriteLine();
        foreach (var item in map.Select((row,idx) => (row,y:idx)))
            Console.WriteLine(new string(item.row.Select((c,x) => visited.Contains((x,item.y)) ? 'X': c).ToArray()));
    }
    internal long Run()
    {
        var score = 0L;
        var stack = new Stack<(int x, int y,int steps,int turns,int rot,HashSet<(int x,int y)> visited)>();
        stack.Push((start.x, start.y,0, 0, 0,new HashSet<(int x, int y)>()));
//        var solutions = new List<(int x, int y, int steps, int turns, int rot, HashSet<(int x, int y)> visited)>();
        var minscore = long.MaxValue;
        while (stack.Count > 0)
        {
            // 165763 high
            // 163759 high
            // 165763
            // 87415
            // 87407
            var pos = stack.Pop();
            if (pos.turns * 1000 + pos.steps > minscore)
                continue;
            pos.visited.Add((pos.x, pos.y));
            var forks = 0;
            var list = new List<(int x, int y, int steps, int turns, int rot, HashSet<(int x, int y)> visited)>();
            for (int i = 0; i < 4; i++)
            {
                var dir = pos.rot;
                var newturn = pos.turns;
                if (i == 1)
                    dir = (dir + 1) % 4;
                if (i == 2)
                    dir = (dir + 3) % 4;
                if (i == 0)
                    dir = (dir + 2) % 4;
                if (i == 1 || i == 2)
                    newturn += 1;
                if (i == 0)
                    newturn += 2;

                var disp = displ[dir];
                var next = (x:pos.x + disp.dx,y: pos.y + disp.dy);
                var c = map[next.y][next.x];
                if (c == '#' || pos.visited.Contains(next))
                    continue;
                var newp = (next.x, next.y, pos.steps + 1,newturn, dir, forks == 0 ? pos.visited : pos.visited.ToHashSet());
                forks++;
                if (c == 'E')
                {
                    //solutions.Add(newp);
                    if (pos.steps + 1000* pos.turns < minscore)
                    {
                        minscore = pos.steps + 1000 * pos.turns;
                        Visu(pos.visited);
                        Console.WriteLine($"{stack.Count} {minscore}");
                        // Console.ReadKey();

                    }

                }
                else
                    list.Add(newp);
                //if (minscore< long.MaxValue)
                //{
                //    var newStack = new Stack<(int x, int y, int steps, int turns, int rot, HashSet<(int x, int y)> visited)>();
                //    foreach(var item in stack.OrderByDescending(i => i.turns*1000 + i.steps))
                //        newStack.Push(item);
                //    stack = newStack;
                //}
                // var newStack = new Stack<(int x, int y, int steps, int turns, int rot, HashSet<(int x, int y)> visited)>();
                //var col = minscore < long.MaxValue ? 
                //    stack.OrderByDescending(i => i.turns * 1000 + i.steps).ThenByDescending(i => Math.Abs(i.x - end.x) + Math.Abs(i.y - end.y))
                //    : stack.OrderByDescending(i => Math.Abs(i.x - end.x) + Math.Abs(i.y - end.y));
                //var col = stack.OrderByDescending(i => Math.Abs(i.x - end.x) + Math.Abs(i.y - end.y));
                //foreach (var item in col)
                //    newStack.Push(item);
                //stack = newStack;
            }
            foreach (var item in list.Where(i => i.turns * 1000 + i.steps < minscore).OrderByDescending(i => Math.Abs(i.x - end.x) + Math.Abs(i.y - end.y)))
                stack.Push(item);
            //if (stack.Count > 10000)
            //{
            //    var newQueue = new Queue<(int x, int y, int steps, int turns, int rot, HashSet<(int x, int y)> visited)>();
            //    foreach (var item in queue.OrderBy(a => a.steps + a.turns * 1000).Take(1000))
            //    { newQueue.Enqueue(item); }
            //    queue = newQueue;
            //}
            //var newStack = new Stack<(int x, int y, int steps, int turns, int rot, HashSet<(int x, int y)> visited)>();
            //var col = minscore < long.MaxValue ?
            //    stack.Where(i => i.turns * 1000 + i.steps < minscore).OrderByDescending(i => Math.Abs(i.x - end.x) + Math.Abs(i.y - end.y)).ThenByDescending(i => i.turns * 1000 + i.steps)
            //    : stack.OrderByDescending(i => Math.Abs(i.x - end.x) + Math.Abs(i.y - end.y));
            //foreach (var item in col)
            //    newStack.Push(item);
            //stack = newStack;
        }
        //        score = solutions.Select(p => p.steps + p.turns * 1000).Min();
        score = minscore;
        return score;
    }
}