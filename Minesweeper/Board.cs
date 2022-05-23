using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Board
    {
        public static Random r = new Random();
        public enum TileType { Empty, Number, Mine = 10};
        private long _width;
        private long _height;
        private long _mineCount;

        

        private long[,] _tiles;

        public Board(long width = 10, long height = 10, long mineCount = 10)
        {
            _width = width;
            _height = height;
            if (mineCount > width * height)
                mineCount = width * height;
            _tiles = Generate(width, height, mineCount);
        }

        public static long[,] Generate(long width, long height, long count)
        {
            long[,] array = new long[width, height];

            // Generate mines
            array = GenerateMines(array, width, height, count);

            // Calculate numbers
            array = GenerateNumbers(array, width, height);

            return array;
        }

        public static long[,] GenerateMines(long[,] array, long width, long height, long count)
        {
            for (int i = 0; i < count;)
            {
                long x = r.Next(0, (int)width);
                long y = r.Next(0, (int)height);

                if (array[x, y] != 0)
                    continue;


                array[x, y] = (long)TileType.Mine;
                i++;
            }

            return array;
        }

        public static long[,] GenerateNumbers(long[,] array, long width, long height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (array[x, y] != (long)TileType.Mine)
                        continue;

                    for (int xn = -1; xn < 2; xn++)
                    {
                        for (int yn = -1; yn < 2; yn++)
                        {
                            if (xn == 0 && yn == 0)
                                continue;

                            int X = x + xn;
                            int Y = y + yn;

                            if (X >= 0 && X < width && Y >= 0 && Y < height)
                            {
                                if (array[X,Y] != (long)TileType.Mine)
                                {
                                    array[X, Y]++;
                                }
                            }
                        }
                    }
                    // x-1, y-1
                    // x, y-1
                    // x+1, y-1
                    // x-1, y
                    // 
                    // x+1, y
                    // x-1, y+1
                    // x, y+1
                    // x+1, y+1
                }
            }

            return array;
        }

        public string GetTileChar(long n)
        {
            if (n == 0)
                return ".";
            if (n == 10)
                return "*";

            if (n > 0 && n < 10)
                return n.ToString();

            throw new ArgumentOutOfRangeException();
        }

        public void Draw(bool[,] guesses)
        {
            Console.Write("  ");
            for (int i = 0; i < _width; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < _width; i++)
            {
                Console.Write("|");
            }
            Console.WriteLine();
            for (int y = 0; y < _height; y++)
            {
                string line = y + "-";
                for (int x = 0; x < _width; x++)
                {
                    line += guesses[x, y]? GetTileChar(_tiles[x, y]): "#";
                }
                Console.WriteLine(line);
            }
        }

        public bool CheckMine(long x, long y)
        {
            return (TileType)_tiles[x, y] == TileType.Mine;
        }

        public bool CheckMine(Guess guess)
        {
            return CheckMine(guess.X, guess.Y);
        }

        public bool CheckEmpty(long x, long y)
        {
            return (TileType)_tiles[x, y] == TileType.Empty;
        }

        public bool CheckEmpty(Guess guess)
        {
            return CheckEmpty(guess.X, guess.Y);
        }
        internal bool[,] ResolveGuesses(bool[,] guesses, Guess guess)
        {
            Queue<Guess> uncheckedGuesses = new Queue<Guess>();
            uncheckedGuesses.Enqueue(guess);
            bool[,] checkedGuesses = new bool[_width, _height];

            while (uncheckedGuesses.Count > 0)
            {
                Guess g = uncheckedGuesses.Dequeue();
                guesses[g.X, g.Y] = true;

                if (!checkedGuesses[g.X, g.Y] && CheckEmpty(g))
                {
                    checkedGuesses[g.X, g.Y] = true;
                    for (int xn = -1; xn < 2; xn++)
                    {
                        for (int yn = -1; yn < 2; yn++)
                        {
                            if (xn == 0 && yn == 0)
                                continue;

                            long X = g.X + xn;
                            long Y = g.Y + yn;

                            if (X >= 0 && X < _width && Y >= 0 && Y < _height)
                            {
                                uncheckedGuesses.Enqueue(new Guess(X, Y));
                            }
                        }
                    }
                }
            }

            return guesses;
        }


        public void DebugDraw()
        {
            for (int y = 0; y < _height; y++)
            {
                string line = "";
                for (int x = 0; x < _width; x++)
                {
                    line += GetTileChar(_tiles[x, y]);
                }
                Console.WriteLine(line);
            }
        }

        public void DebugColourDraw()
        {
            for (int y = 0; y < _height; y++)
            {
                string line = "";
                for (int x = 0; x < _width; x++)
                {

                    Console.ForegroundColor = GetTextColour(_tiles[x, y]);
                    Console.Write(GetTileChar(_tiles[x, y]));
                }
                Console.WriteLine();
            }
        }

        public ConsoleColor GetTextColour(long n)
        {
            switch (n)
            {
                case 0: return ConsoleColor.White;
                case 1: return ConsoleColor.Blue;
                case 2: return ConsoleColor.DarkCyan;
                case 3: return ConsoleColor.Cyan;
                case 4: return ConsoleColor.Green;
                case 5: return ConsoleColor.Yellow;
                case 6: return ConsoleColor.Magenta;
                case 7: return ConsoleColor.DarkMagenta;
                case 8: return ConsoleColor.Red;
                case 10: return ConsoleColor.DarkRed;
                default: return ConsoleColor.White;
            }
        }
    }
}
