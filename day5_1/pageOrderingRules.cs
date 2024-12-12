// See https://aka.ms/new-console-template for more information

using System.Linq;

internal class pageOrderingRules
{
    private List<(int p1, int p2)> pageOrdering;

    private readonly int[][] updates;

    public pageOrderingRules(string test)
    {
        var parts = test.Replace("\r", string.Empty).Split("\n\n");
        pageOrdering = parts[0].Split('\n').Select(x => x.Split('|')).Select(x => (int.Parse(x[0]), int.Parse(x[1]))).ToList();
        updates = parts[1].Split('\n').Select(x => x.Split(',').Select(x => int.Parse(x)).ToArray()).ToArray();
    }

    internal int Score()
    {
        int score = 0;
        var hash = pageOrdering.Select(x => x.p1).Concat(pageOrdering.Select(x => x.p2)).ToHashSet();
        foreach (var item in updates)
        {
            var ok = true;
            var pages = item.Where(x => hash.Contains(x)).ToArray();
            for (var i = 0; i < pages.Length - 1 && ok; i++)
                for (var j = i + 1; j < pages.Length && ok; j++)
                {
                    var p1 = pages[i];
                    var p2 = pages[j];
                    if (pageOrdering.Contains((p2, p1)))
                        ok = false;
                }
            if (ok)
                score += item[item.Length / 2];
        }
        return score;
    }
}
