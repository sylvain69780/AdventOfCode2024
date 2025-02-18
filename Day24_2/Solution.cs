

using System.Data;

internal class Solution
{
    private Dictionary<string, bool> wires;
    private Dictionary<string, (string op1, string op2, string op)> gates;
    private int bits;

    public Solution(string test)
    {
        var parts = test.Replace("\r", string.Empty).Split("\n\n");
        wires = parts[0].Split("\n").Select(x => x.Split(": ")).Select(x => (wire: x[0], value: x[1] == "1" ? true : false)).ToDictionary(x => x.wire, x => x.value);
        gates = parts[1].Split("\n").Select(x => x.Split(" -> "))
            .Select(x => (rule: x[0].Split(' '), wire: x[1]))
            .Select(x => (op1: x.rule[0], op: x.rule[1], op2: x.rule[2], x.wire))
            .ToDictionary(x => x.wire, x => (op1 : x.op1[0] == 'x' ? x.op1 : x.op2, op2: x.op1[0] == 'x' ? x.op2 : x.op1, x.op));
         bits = wires.Count / 2;
    }

    internal string Solve()
    {
        var v = gates["grf"];
        gates["grf"] = gates["wpq"];
        gates["wpq"] = v;

        v = gates["z18"];
        gates["z18"] = gates["fvw"];
        gates["fvw"] = v;

        v = gates["z22"];
        gates["z22"] = gates["mdb"];
        gates["mdb"] = v;

        v = gates["z36"];
        gates["z36"] = gates["nwq"];
        gates["nwq"] = v;

        var result = Compute();
        var z = 0L;
        foreach (var wire in wires.Keys.Where(x => x[0] == 'z'))
        {
            if (wires[wire])
            {
                z += 1L << int.Parse(wire[1..]);
            }
        }
        var x = 0L;
        foreach (var wire in wires.Keys.Where(x => x[0] == 'x'))
        {
            if (wires[wire])
            {
                x += 1L << int.Parse(wire[1..]);
            }
        }
        var y = 0L;
        foreach (var wire in wires.Keys.Where(x => x[0] == 'y'))
        {
            if (wires[wire])
            {
                y += 1L << int.Parse(wire[1..]);
            }
        }
        Console.WriteLine($"x = {x} + y = {y} = z = {z} {x + y == z}");
        Console.WriteLine($"z in binary = {Convert.ToString(z, 2).PadLeft(bits + 1, '0')}");
        Console.WriteLine($"r in binary = {Convert.ToString(x + y, 2).PadLeft(bits + 1, '0')}");
        return string.Join(",", (new List<string>() { "grf", "wpq", "z18", "fvw", "z22", "mdb", "z36", "nwq" }).OrderBy(x => x) );
    }

    private void Encode(int v1, int v2)
    {
        for (int i = 0; i < bits; i++)
        {
            wires[$"x{i:D2}"] = ((v1 >> i) & 1) == 1;
            wires[$"y{i:D2}"] = ((v2 >> i) & 1) == 1;
        }
    }

    private long Compute()
    {
        for (int i = 0; i < bits; i++)
        {
            var key = $"z{i:D2}";
            var value = GetValue(key);
//            wires.Add(key,value);
            Console.WriteLine(new string('-', 30));
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

    private bool GetValue(string key)
    {
        if (wires.TryGetValue(key, out var value))
            return value;
        var gate = gates[key];
        var (o1,o2) = (gate.op1, gate.op2);
        var op1 = GetValue(o1);
        var op2 = GetValue(o2);

        if ( gates.TryGetValue(o1, out var g1) && g1.op1[0] == 'x')
            o1 = o1+"("+g1.op + g1.op1[1..]+")";
        if (gates.TryGetValue(o2, out var g2) && g2.op1[0] == 'x')
            o2 = o2 + "(" + g2.op + g2.op1[1..]+")";

        Console.WriteLine($"{key} = {o1} {gate.op} {o2}");
        value = gate.op switch
        {
            "AND" => op1 & op2,
            "OR" => op1 | op2,
            "XOR" => op1 ^ op2,
            _ => throw new InvalidExpressionException()
        };
        wires.Add(key, value);
        return value;
    }

    private long ComputeOld(Dictionary<string, string> swap)
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
