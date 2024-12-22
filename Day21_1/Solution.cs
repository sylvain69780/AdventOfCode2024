

using System.Runtime.InteropServices;

internal class Solution
{
    private string[] codes;
    private (char c, int x, int y)[] numKeypad;
    private (char c, int x, int y)[] dirKeypad;

    List<string> Moves(char start, char end,(char c, int x, int y)[] keypad)   
{
    var res = new List<string>();
    var startp = keypad.First(p => p.c == start);
    var endp = keypad.First(p => p.c == end);
    var stack = new Stack<(int x, int y,List<char> path)>();
    stack.Push((startp.x, startp.y, new List<char>()));
    while (stack.Count > 0)
    {
        var (x, y,path) = stack.Pop();
        if ( x == endp.x && y == endp.y)
        {
            res.Add(new string(path.ToArray()));
            continue;
        }
        if (!keypad.Any(p => p.x == x && p.y == y))
            continue;
        if ( x < endp.x)
            stack.Push((x + 1, y,path.Append('>').ToList()));
        if ( x > endp.x)
            stack.Push((x - 1, y,path.Append('<').ToList()));
        if ( y < endp.y)        
            stack.Push((x, y + 1,path.Append('v').ToList()));
        if ( y > endp.y)    
            stack.Push((x, y - 1,path.Append('^').ToList()));
        }
    return res;
}

    public Solution(string test)
    {
        codes = test.Replace("\r\n", "\n").Split('\n');

        numKeypad = (new string[] { "789", "456", "123", " 0A" })
        .SelectMany((row,y) => row.Select((c,x) => (c, x, y)))
        .Where(p => p.c != ' ')
        .ToArray();        
        dirKeypad = new string[] {" ^A","<v>"}
        .SelectMany((row,y) => row.Select((c,x) => (c, x, y)))
        .Where(p => p.c != ' ')
        .ToArray();        
        //new List<(char node, char dest, int cost)>()

    }


    IEnumerable<string> KeypadRobotMoves(string code)
    {
        var stack = new Stack<(char pos,int level,string result)>();
        stack.Push(('A',0,string.Empty));
        while (stack.Count > 0)
        {
            var (pos,level,result) = stack.Pop();
            if (level == code.Length)
            {
                yield return result;
                continue;
            }
            var c = code[level];
                foreach( var move in Moves(pos, c, numKeypad))
                    stack.Push((c,level + 1,result + move + 'A'));
        }
    }

    IEnumerable<string> DirKeypadRobotMoves(string code)
    {
        var stack = new Stack<(char pos,int level,string result)>();
        stack.Push(('A',0,string.Empty));
        while (stack.Count > 0)
        {
            var (pos,level,result) = stack.Pop();
            if (level == code.Length)
            {
                yield return result;
                continue;
            }
            var c = code[level];
                foreach( var move in Moves(pos, c, dirKeypad))
                    stack.Push((c,level + 1,result + move + 'A'));
        }
    }
    internal string Run()
    {
        var score = 0;
        foreach( var code in codes)
        {
            var moves = KeypadRobotMoves(code)
                .SelectMany(code => DirKeypadRobotMoves(code))
                .SelectMany(code => DirKeypadRobotMoves(code));
                var best = moves.OrderBy(code => code.Length).First();
                Console.WriteLine(best);
                score += best.Length * int.Parse(code[..^1]);
        }

        return score.ToString();
    }
}
