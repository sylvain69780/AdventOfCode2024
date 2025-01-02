

using System.Diagnostics;

internal class Solution
{
    private long[] secrets;

    public Solution(string test)
    {
        secrets = test.Replace("\r", "").Split("\n").Select(long.Parse).ToArray();
    }

    internal long Solve()
    {
        var score = 0L;

        var count = 2000 ;
        var data = new List<List<long>>
        {
            secrets.Select(x => x % 10).ToList()
        };
        for (var i = 0; i < count * 3; i++)
        {
            var newSecrets = (i % 3) switch
            {
                0 => secrets.Select(secret => ((secret * 64) ^ secret) % 16777216).ToArray(),
                1 => secrets.Select(secret => ((secret / 32) ^ secret) % 16777216).ToArray(),
                2 => secrets.Select(secret => ((secret * 2048) ^ secret) % 16777216).ToArray(),
                _ => throw new InvalidOperationException()
            };
            secrets = newSecrets;
            if (i % 3 == 2)
            {
                data.Add(secrets.Select(x => x % 10).ToList());
            }
        }
        // 4 valeurs de -9 à +9 chiffres = 18 * 18 * 18 * 19 = 104976 possibilités
        // recherche optimale en cherchant en partant des optimaux de chaque liste ?
        // var sequ = Enumerable.Range(-9,19)
        //     .SelectMany(a => Enumerable.Range(-9,19).Select(b => (a,b)))
        //     .SelectMany(x => Enumerable.Range(-9,19).Select(c => (x.a,x.b,c)))
        //     .SelectMany(x => Enumerable.Range(-9,19).Select(d => (x.a,x.b,x.c,d)));
        var prices = new List<(long value, (long a, long b, long c, long d) sequ)>();
        for (var seed = 0; seed < data[0].Count; seed++)
        {
            var hashset = new HashSet<(long a, long b, long c, long d)>();
            for (var step = 4; step < data.Count; step++)
            {
                var sequ = (
                    a: data[step - 3][seed] - data[step - 4][seed],
                    b: data[step - 2][seed] - data[step - 3][seed],
                    c: data[step - 1][seed] - data[step - 2][seed],
                    d: data[step][seed] - data[step - 1][seed]);
                var value = data[step][seed];
                if (hashset.Contains(sequ))
                    continue;
                hashset.Add(sequ);
                prices.Add((value, sequ));
            }

        }
        var best = prices.GroupBy(x => x.sequ)
            .Select(x => (value: x.Sum(y => y.value),sequ:x.Key))
            .OrderByDescending(x => x.value)
            .First();
        score = best.value;
        return score;
    }
}
