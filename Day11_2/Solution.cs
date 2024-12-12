
internal class Solution
{
    private string[] stones;

    public Solution(string test)
    {
        stones = test.Replace("\r", string.Empty).Split(" ");
    }

    internal long Run()
    {
        var sl = stones.ToList();
        var dico = sl.ToDictionary(x => x, x => 1L);
        for (var i = 0; i < 75; i++)
        {
            var newDico = new Dictionary<string, long>();
            newDico.Add("1", 0L);
            foreach (var s in dico.Keys)
            {
                if (s == "0")
                    newDico["1"] += dico[s];
                else if ( s.ToString().Length % 2 == 0)
                {
                    var m = s.ToString().Length / 2;
                    var s1 = long.Parse(s[0..m]).ToString();
                    var s2 = long.Parse(s[m..]).ToString();
                    UpdateDico(newDico, s1, dico[s]);
                    UpdateDico(newDico, s2, dico[s]);
                }
                else
                {
                    var s2 = (decimal.Parse(s) * 2024).ToString();
                    UpdateDico(newDico, s2, dico[s]);
                }
            }
            dico = newDico;
        }

        return dico.Values.Sum();
    }

    private static void UpdateDico(Dictionary<string, long> dico, string s, long count)
    {
        if (dico.TryGetValue(s, out var prev))
            dico[s] = prev + count;
        else
            dico.Add(s, count);
    }
}