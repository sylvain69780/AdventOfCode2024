using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2024
{
    internal class Grid
    {
        public Grid(string input)
        {
            grid = input.Split('\n');
        }
        string[] grid;

        char ge(int x, int y) => x >= 0 && y >= 0 && x < grid[0].Length && y < grid.Length ? (grid[y])[x] : '.';

        public int Scan()
        {
            int score = 0;
            for (int y = 0; y < grid.Length; y++)
                for (int x = 0; x < grid[0].Length; x++)
                {
                    if (ge(x, y) != 'X')
                        continue;
                    for (int dy = -1; dy < 2; dy++)
                        for (int dx = -1; dx < 2; dx++)
                            for (int i = 1; i < 4; i++)
                            {
                                var c = ge(x + i * dx, y + i * dy);
                                if (c != "XMAS"[i])
                                    break;
                                if (i == 3 && c == 'S')
                                    score++;
                            }
                }
            return score;
        }

        public int LookFor(string s, int x, int y)
        {
            int score = 0;
            if (s[0] == ge(x, y))
            {
                if (s.Length == 1)
                    return 1;
                for (int dx = -1; dx < 2; dx++)
                    for (int dy = -1; dy < 2; dy++)
                    {
                        if (dx == 0 && dy == 0)
                            continue;
                        score += LookFor(s[1..], x + dx, y + dy);
                    }
            }
            return score;
        }

        public int ScanBAD()
        {
            int score = 0;
            for (int y = 0; y < grid.Length; y++)
                for (int x = 0; x < grid[0].Length; x++)
                {
                    score += LookFor("XMAS", x, y);
                }
            return score;
        }
    }
}
