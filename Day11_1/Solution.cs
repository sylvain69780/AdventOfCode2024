
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

        for (var i = 0; i < 25; i++)
        {
            var sl2 = new List<string>();
            foreach (var s in sl)
            {
                if (s == "0")
                    sl2.Add("1");
                else if ( s.ToString().Length % 2 == 0)
                {
                    var m = s.ToString().Length / 2;
                    var s1 = decimal.Parse(s[0..m]).ToString();
                    var s2 = decimal.Parse(s[m..]).ToString();
                    sl2.Add (s1);
                    sl2.Add(s2);
                }
                else
                {
                    sl2.Add((decimal.Parse(s) * 2024).ToString());
                }
            }
            sl = sl2;
        }

        return sl.Count;
    }
}