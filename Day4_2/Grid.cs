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
            for (int x = 0; x < grid[0].Length; x++)
                for (int y = 0; y < grid.Length; y++)
                {
                    var c = ge(x, y);
                    if (c != 'A')
                        continue;
                    var c1  = ge(x-1, y-1);
                    if (!"MS".Contains(c1))
                        continue;
                    var c2 = ge(x+1, y+1);
                    if (!"MS".Contains(c2))
                        continue;
                    var c3 = ge(x-1, y+1);
                    if (!"MS".Contains(c3))
                        continue;
                    var c4 = ge(x+1, y-1);
                    if (!"MS".Contains(c4))
                        continue;
                    if (c1 != c2 && c3 != c4)
                        score++;
                }
            return score;
        }
    }
}
