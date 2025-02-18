

using System.Data;

internal class Solution
{
    private Dictionary<string, bool> wires;
    private Dictionary<string, (string op1, string op2, string op)> gates;

    public Solution(string test)
    {
        var parts = test.Replace("\r", string.Empty).Split("\n\n");
        wires = parts[0].Split("\n").Select(x => x.Split(": ")).Select(x => (wire: x[0], value: x[1] == "1" ? true : false)).ToDictionary(x => x.wire, x => x.value);
        gates = parts[1].Split("\n").Select(x => x.Split(" -> "))
            .Select(x => (rule: x[0].Split(' '), wire: x[1]))
            .Select(x => (op1: x.rule[0], op: x.rule[1], op2: x.rule[2], x.wire))
            .ToDictionary(x => x.wire, x => (x.op1, x.op2, x.op));
    }

    internal string Solve()
    {
        var swap = new Dictionary<string, string>();
        Encode(0, 3);
        long result = Compute(swap);
        return result.ToString();
    }

    private void Encode(int v1, int v2)
    {
        var bits = wires.Count / 2;
        for (int i = 0; i < bits; i++)
        {
            wires[$"x{i:D2}"] = ((v1>>i) & 1) == 1;
            wires[$"y{i:D2}"] = ((v2 >> i) & 1) == 1;
        }
    }

    private long Compute(Dictionary<string, string> swap)
    {
        var modified = true;
        while (modified)
        {
            modified = false;
            foreach (var gate in gates)
            {
                var output = gate.Key;
                if (swap.TryGetValue(output, out var output2))
                    output = output2;
                if (!wires.ContainsKey(output) && wires.TryGetValue(gate.Value.op1, out var op1) && wires.TryGetValue(gate.Value.op2, out var op2))
                {
                    wires.Add(output, gate.Value.op switch
                    {
                        "AND" => op1 & op2,
                        "OR" => op1 | op2,
                        "XOR" => op1 ^ op2,
                        _ => throw new InvalidExpressionException()
                    });
                    modified = true;
                }
            }
        }
        var result = 0L;
        foreach (var wire in wires.Keys.Where(x => x[0] == 'z'))
        {
            if (wires[wire])
            {
                result += 1L << int.Parse(wire[1..]);
            }
        }

        return result;
    }
}
