using Minesweeper;

long width = 10;
long height = 10;
long count = 10;
bool hasLost = false;
bool hasWon = false;

bool[,] guesses = new bool[10,10];
Board board = new Board(width, height, count);
Player player = new Player();
// board.DebugDraw();

while (!hasLost && !hasWon)
{
    board.Draw(guesses);
    Guess guess = Player.GetGuess();

    guesses = board.ResolveGuesses(guesses, guess);

    if (board.CheckMine(guess))
    {
        // Player lost
        hasLost = true;
    }


    Console.Clear();
}