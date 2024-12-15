
internal class Solution
{
    private char[][] map;
    private string moves;
    private (char c, int x, int y) start;

    public Solution(string test)
    {
        var parts = test.Replace("\r", string.Empty).Split("\n\n");
        map = parts[0].Split("\n").Select(s => s.ToCharArray()).ToArray();
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
            if (Sample(next) == 'O')
            {
                var stack = new Stack<(int x, int y)>();
                while (Sample(next) == 'O')
                {
                    stack.Push(next);
                    next = (next.x + disp.dx, next.y + disp.dy);
                }
                if (Sample(next) == '.')
                {
                    while (stack.Count > 0)
                    {
                        map[next.y][next.x] = 'O';
                        next = stack.Pop();
                    }
                    map[next.y][next.x] = '.';
                    robot = next;
                }
            }
        }
        Visu(robot);
        var score = map.SelectMany((row, y) => row.Select((c, x) => (c, x, y)))
            .Where(p => p.c == 'O')
            .Select(p => p.x + 100 * p.y).Sum();
        return score;
    }
}

