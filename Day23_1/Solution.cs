


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
        var triplets = new List<HashSet<string>>();
        foreach (var (node,nodes) in graph)
        {
            var list = nodes.ToList();
            for ( var i = 0 ; i < list.Count-1 ; i++ )
            {
                for ( var j = i+1 ; j < list.Count ; j++ )
                {
                    if ( graph.TryGetValue(list[i],out var hash) && hash.Contains(list[j]) )
                    {
                        triplets.Add(new HashSet<string>{node,list[i],list[j]});
                    }
                }
            }
        }
        var score = triplets
            .Where(x => x.Any(y => y[0] == 't'))
            .Select(x => string.Join('-',x.Order())).Distinct().Count();
        return score.ToString();
    }
}