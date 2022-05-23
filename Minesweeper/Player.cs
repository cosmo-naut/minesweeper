using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal class Player
    {
        public static Guess GetGuess()
        {
            Console.WriteLine("Please enter your guess in the format (xx,yy)");
            
            string guessString = Console.ReadLine();
            ValidateGuess(guessString);

            return new Guess(guessString);
        }

        private static bool ValidateGuess(string s)
        {
            // does not currently validate
            return true;
        }
    }
}
