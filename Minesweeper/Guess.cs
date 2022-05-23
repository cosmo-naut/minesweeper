using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public struct Guess
    {
        public long X;
        public long Y;

        public Guess(string guessString)
        {
            string[] sub = guessString.Split(",");
            X = long.Parse(sub[0]);
            Y = long.Parse(sub[1]);
        }

        public Guess(long x, long y)
        {
            X = x;
            Y = y;
        }
    }
}
