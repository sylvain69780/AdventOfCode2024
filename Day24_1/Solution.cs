

using System.Data;

internal class Solution
{
    private Dictionary<string, bool> wires;
    private Dictionary<string, (string op1, string op2, string op)> gates;

    public Solution(string test)
    {
        var parts = test.Replace("\r",string.Empty).Split("\n\n");
        wires = parts[0].Split("\n").Select(x => x.Split(": ")).Select(x => (wire: x[0], value: x[1] == "1" ? true:false)).ToDictionary(x => x.wire, x => x.value);
        gates = parts[1].Split("\n").Select(x => x.Split(" -> "))
            .Select(x => (rule: x[0].Split(' '), wire: x[1]))
            .Select(x => (op1: x.rule[0],op: x.rule[1],op2:x.rule[2],x.wire))
            .ToDictionary(x => x.wire,x => (x.op1,x.op2,x.op));
    }

    internal string Solve()
    {
        var modified = true;
        while (modified)
        {
            modified = false;
            foreach(var gate in gates)
            {
                if ( !wires.ContainsKey(gate.Key ) && wires.ContainsKey(gate.Value.op1) && wires.ContainsKey(gate.Value.op2))
                {
                    wires.Add(gate.Key, gate.Value.op switch
                    {
                        "AND" => wires[gate.Value.op1] & wires[gate.Value.op2],
                        "OR" => wires[gate.Value.op1] | wires[gate.Value.op2],
                        "XOR" => wires[gate.Value.op1] ^ wires[gate.Value.op2],
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
        return result.ToString();
    }
}
