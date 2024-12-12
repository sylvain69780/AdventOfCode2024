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
        for ( var  y = 0;y < city.Length;y++)
            for ( var x = 0; x < city[y].Length;x++)
            {
                score += Detection(antennas, y, x);
            }
        return score;
    }

    private static int Detection(Dictionary<char, List<(int x, int y)>> antennas, int y, int x)
    {
        foreach (var (_, list) in antennas)
        {
            if (list.Count > 1)
            {
                for (var i = 0; i < list.Count - 1; i++)
                    for (var j = i + 1; j < list.Count; j++)
                    {
                        var a = list[i];
                        var b = list[j];
                        var ab = (x: b.x - a.x, y: b.y - a.y);
                        var ap = (x: x - a.x, y: y - a.y);
                        var dot = ab.x * ap.y - ab.y * ap.x;
                        if (dot == 0)
                        {
                            return 1;
                        }
                    }
            }
        }
        return 0;
    }
}
