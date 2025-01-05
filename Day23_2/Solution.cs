


internal class Solution
{
    private Dictionary<string, HashSet<string>> graph;

    public Solution(string test)
    {
        graph = new Dictionary<string,HashSet<string>>();
        var links = test.Replace("\r",string.Empty).Split("\n").Select(x => x.Split("-")).Select(x => (a:x[0],b: x[1]));
        foreach (var (a, b) in links)
        {
            if ( graph.TryGetValue(a,out var hash) )
                hash.Add(b);
            else
                graph.Add(a,[b]);
            if ( graph.TryGetValue(b,out var hash2) )
                hash2.Add(a);
            else
                graph.Add(b,[a]);
        }
    }

    internal string Solve()
    {
        var sets = graph.Keys.Select(x => new HashSet<string>() {x}).ToList();
        while (true) 
        {
            var newSets = new List<HashSet<string>>();
            foreach(var set in sets)
            {
                // try to add a new element to the set
                foreach (var node in graph.Keys)
                {
                    if (set.All(x => graph[x].Contains(node)))
                    {
                        set.Add(node);
                        if ( ! newSets.Any(x => x.SetEquals(set)) )
                            newSets.Add(set);
                        break;
                    }
                }
            }
            if (newSets.Count == 0)
                break;
            sets = newSets;
        }
        return string.Join(',',sets[0].Order());
    }
}