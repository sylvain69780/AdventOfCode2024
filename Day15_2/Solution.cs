
internal class Solution
{
    private char[][] map;
    private string moves;
    private (char c, int x, int y) start;

    public Solution(string test)
    {
        var parts = test.Replace("\r", string.Empty).Split("\n\n");
        map = parts[0].Split("\n").Select(s => s.SelectMany(c => c switch
        {
            '#' => "##",
            'O' => "[]",
            '.' => "..",
            '@' => "@.",
            _ => throw new NotImplementedException()
        }).ToArray()).ToArray();
        moves = parts[1].Replace("\n", string.Empty);
        start = map.SelectMany((row, y) => row.Select((c, x) => (c, x, y))).Single(p => p.c == '@');
        map[start.y][start.x] = '.';
    }

    char Sample((int x, int y) p)
    {
        return map[p.y][p.x];
    }

    private const string directions = "^v<>";

    private readonly (int dx, int dy)[] displacements = [(0, -1), (0, 1), (-1, 0), (1, 0)];

    private void Visu((int x, int y) robot)
    {
              return;
        Console.WriteLine();
        map[robot.y][robot.x] = '@';
        foreach (var item in map)
            Console.WriteLine(new string(item));
        map[robot.y][robot.x] = '.';
        Console.ReadKey();
    }

    internal long Run()
    {
        var robot = (x: start.x, y: start.y);
        for (var counter = 0; counter < moves.Length; counter++)
        {
            Visu(robot);
            var move = moves[counter];
            var disp = displacements[directions.IndexOf(move)];
            var next = (x: robot.x + disp.dx, y: robot.y + disp.dy);
            if (Sample(next) == '#')
                continue;
            if (Sample(next) == '.')
            {
                robot = next;
                continue;
            }
            if (move == '>' && Sample(next) == '[')
            {
                var stack = new Stack<(int x, int y)>();
                while (Sample(next) == '[')
                {
                    stack.Push(next);
                    next = (next.x + 2, next.y);
                }
                if (Sample(next) == '.')
                {
                    while (stack.Count > 0)
                    {
                        map[next.y][next.x] = ']';
                        map[next.y][next.x - 1] = '[';
                        next = stack.Pop();
                    }
                    map[next.y][next.x] = '.';
                    robot = next;
                }
            }
            if (move == '<' && Sample(next) == ']')
            {
                var stack = new Stack<(int x, int y)>();
                while (Sample(next) == ']')
                {
                    stack.Push(next);
                    next = (next.x - 2, next.y);
                }
                if (Sample(next) == '.')
                {
                    while (stack.Count > 0)
                    {
                        map[next.y][next.x] = '[';
                        map[next.y][next.x + 1] = ']';
                        next = stack.Pop();
                    }
                    map[next.y][next.x] = '.';
                    robot = next;
                }
            }
            if (move == '^' || move == 'v')
            {
                var dir = move == '^' ? -1 : 1;
                if (Sample(next) == ']')
                    next = (next.x - 1, next.y);
                var stack = new Stack<(int x, int y)>();
                var queue = new Queue<(int x, int y)>();
                queue.Enqueue(next);
                var blocked = false;
                while (queue.Count > 0)
                {
                    var pos = queue.Dequeue();
                    if (map[pos.y+dir][pos.x] == '#' || map[pos.y + dir][pos.x + 1] == '#')
                    {
                        blocked = true;
                        break;
                    }
                    if (map[pos.y + dir][pos.x] == '.' && map[pos.y + dir][pos.x + 1] == '.')
                        stack.Push(pos);
                    else
                    {
                        stack.Push(pos);
                        if (map[pos.y + dir][pos.x] == '[')
                            queue.Enqueue((pos.x, pos.y + dir));
                        else
                        {
                            if (map[pos.y + dir][pos.x] == ']')
                                queue.Enqueue((pos.x - 1, pos.y + dir));
                            if (map[pos.y + dir][pos.x + 1] == '[')
                                queue.Enqueue((pos.x + 1, pos.y + dir));
                        }
                    }

                }
                if (!blocked)
                {
                    while (stack.Count > 0)
                    {
                        var pos = stack.Pop();
                        map[pos.y + dir][pos.x] = '[';
                        map[pos.y + dir][pos.x + 1] = ']';
                        map[pos.y][pos.x] = '.';
                        map[pos.y][pos.x + 1] = '.';
                    }
                    robot = (robot.x,robot.y + dir);
                }
            }

        }
        Visu(robot);
        var score = map.SelectMany((row, y) => row.Select((c, x) => (c, x, y)))
            .Where(p => p.c == '[')
            .Select(p => p.x + 100 * p.y).Sum();
        return score;
    }
}

