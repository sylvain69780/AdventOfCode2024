// See https://aka.ms/new-console-template for more information

using System.Threading;

internal class Solution
{
    private string[] grid;
    private const string directions = "^>v<";
    private static readonly (int x, int y)[] velocities = [(0, -1), (1, 0), (0, 1), (-1, 0)];
    public Solution(string test) => grid = [.. test.Replace("\r", string.Empty).Split('\n')];

    private char sample((int x, int y) p) => p.x >= 0 && p.y >= 0 && p.x < grid[0].Length && p.y < grid.Length ? grid[p.y][p.x] : '*';
    internal int Run()
    {
        var score = 0;
        for ( var y = 0; y < grid.Length; y++ ) 
        for (var x = 0; x < grid[0].Length; x++)
            {
                if ( sample((x,y)) == '.')
                    score += RunSimu((x,y));
            }
        return score;
    }
    internal int RunSimu((int x,int y) obstruction)
    {
        var visited = new HashSet<(int x, int y)>();
        var visited2 = new HashSet<(int x, int y, int dir)>();
        var pos = grid.Select((s, i) => (x: s.IndexOf('^'), y: i)).Where(r => r.x > 1).Single();
        var dir = 0;
        var isLoop = false;
        while (true)
        {
            visited.Add(pos);
            var loopDetect = (pos.x, pos.y, dir);
            if (visited2.Contains(loopDetect))
            {
                isLoop = true;  
                break;
            }

            visited2.Add(loopDetect);
            while (sample((pos.x + velocities[dir].x, pos.y + velocities[dir].y)) == '#' || (pos.x + velocities[dir].x, pos.y + velocities[dir].y) == obstruction)
                dir = (dir + 1) % 4;
            var next = (pos.x + velocities[dir].x, pos.y + velocities[dir].y);
            if (sample(next) == '*')
                break;
            pos = next;
        }
        return isLoop ? 1 :0 ;
    }
}

