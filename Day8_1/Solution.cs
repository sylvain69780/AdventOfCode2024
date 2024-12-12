// See https://aka.ms/new-console-template for more information

internal class Solution
{
    private string[] city;

    public Solution(string test)
    {
        city = test.Replace("\r", string.Empty).Split('\n');
    }

    internal int Run()
    {
        int score = 0;
        var antennas = new Dictionary<char, List<(int x, int y)>>();
        for (int y = 0; y < city.Length; y++)
            for (int x = 0; x < city[y].Length; x++)
            {
                var c = city[y][x];
                if (c != '.')
                {
                    if (antennas.TryGetValue(c, out var list))
                    {
                        list.Add((x, y));
                    }
                    else
                    {
                        antennas.Add(c, [(x, y)]);
                    }
                }
            }
        var explored = new HashSet<(int x, int y)>();
        foreach (var key in antennas.Keys)
        {
            var list = antennas[key];
            if (list.Count > 1)
            {
                for ( var i = 0; i < list.Count-1;i++) 
                    for (var j = i+1; j < list.Count;j++)
                    {
                        var a = list[i];
                        var b = list[j];
                        var d = (x:b.x - a.x,y:b.y - a.y);
                        var p1 = (x:b.x + d.x,y: b.y + d.y);
                        var p2 = (x:a.x - d.x, y:a.y - d.y);
                        foreach ((int x, int y) p in new (int x, int y)[] { p1, p2 })
                            if (p.x >= 0 && p.y >= 0 && p.x < city[0].Length && p.y < city.Length)
                            {
                                if ( ! explored.Contains(p) )
                                {
                                    score++;
                                    explored.Add(p); 
                                }

                            }

                    }
            }
        }
        return score;
    }
}
