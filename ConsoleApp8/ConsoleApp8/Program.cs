namespace ConsoleApp8
{
    //Defines the playing board 
    public class Board
    {
        public const int Rows = 6;
        public const int Columns = 7;
        private string[,] Array;

        public const string EmptyDisk = "\u25CC   ";
        public const string BlueDisk = "\U0001F535  ";
        public const string RedDisk = "\U0001F534  ";

        //Board constructor that creates the matrix of the board
        public Board()
        {
            Array = new string [Rows, Columns];
            for (var row = 0; row < Rows; row++) 
            { 
                for (var col = 0; col < Columns; col++)
                {
                    Array[row, col] = EmptyDisk;
                } 
            }

        }

        //Visualizes the board in the console
        public void Print()
        {
            Console.Clear();
            Console.WriteLine("{0}{0}Welcome to Take Four!  {1}{1}", Board.BlueDisk, Board.RedDisk);
            Console.WriteLine();

            for (var col = 1; col <= Columns; col++)
            {
                Console.Write("{0}   ", col);
            }
            Console.WriteLine();
            
            for (var row = 0; row < Rows; row++) 
            { 
                for (var col = 0; col < Columns; col++)
                {
                    if (col == Columns-1) {
                        Console.Write("{0}\n", Array[row, col]);
                    } else {
                        Console.Write(Array[row, col]);
                    }
                } 
            }
            for (var col = 1; col <= Columns; col++)
            {
                Console.Write("{0}   ", col);
            }
            Console.WriteLine();
        }
        // Returns if there is a winner
        public void Win(string disk)
        {
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    Array[row, col] = disk;
                    Print();
                    Thread.Sleep(50);
                }
            }
        }
        // Animation of the disk falling
        private void Dropping(int row, int col, string disk)
        {
            for (var r = 0; r < row; r++)
            {
                Array[r, col] = disk;
                Print();
                Thread.Sleep(100);
                Array[r, col] = EmptyDisk;
                Print();
            }
        }
        // Place the disk in an available cell
        public void DropDisk(int col, string disk)
        {
            for (var row = Rows - 1; row >= 0; row--)
            {
                if (Array[row, col - 1] != EmptyDisk) continue;

                Dropping(row, col - 1, disk);

                Array[row, col - 1] = disk;
                return;
            }
        }
        // Checks if is possible to drop the disk in the column
        public bool IsColumnAvailable(int col)
        {
            return Array[0, col-1] == EmptyDisk;
        }

        // Checking the whole Array to make sure that it's not empty
        public bool IsFull() 
        {
            foreach (var cell in Array)
            {
                if (cell == EmptyDisk)
                {
                    return false;
                }
            }

            return true;
        }
        //Checks possible matches (Vertical/horizontal/Diagonal)
        public bool CheckWinner()
        {
            // Check horizontal lines
            for (var row = 0; row < Rows; row++)
            {
                var cell = Array[row, 0];
                var count = 1;

                for (var col = 1; col < Columns; col++)
                {
                    if (Array[row, col] == cell)
                    {
                        count += 1;
                    }
                    else
                    {
                        cell = Array[row, col];
                        count = 1;
                    }

                    if (count != 4 || cell == EmptyDisk) continue;
                    return true;
                }
            }

            // Check vertical lines
            for (var col = 0; col < Columns; col++)
            {
                var cell = Array[0, col];
                var count = 1;

                for (var row = 1; row < Rows; row++)
                {
                    if (Array[row, col] == cell)
                    {
                        count += 1;
                    }
                    else
                    {
                        cell = Array[row, col];
                        count = 1;
                    }

                    if (count != 4 || cell == EmptyDisk) continue;
                    return true;
                }
            }

            // Check diagonal lines
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Columns; col++)
                {
                    var currentCell = Array[row, col];
                    if (currentCell != EmptyDisk)
                    {
                        // Search right diagonals.
                        var count = 1;
                        var search = true;

                        while (search)
                        {
                            try
                            {
                                var nextCell = Array[row + count, col + count];
                                if (nextCell == currentCell)
                                {
                                    count += 1;
                                }
                                else
                                {
                                    search = false;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                search = false;
                            }

                            if (count != 4) continue;
                            Console.WriteLine("Winner is diagonal right {0}", currentCell);
                            return true;
                        }

                        // Search left diagonals.
                        count = 1;
                        search = true;
                        while (search)
                        {
                            try
                            {
                                var nextCell = Array[row + count, col - count];
                                if (nextCell == currentCell)
                                {
                                    count += 1;
                                }
                                else
                                {
                                    search = false;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                search = false;
                            }

                            if (count != 4) continue;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }

    //Parent class Player
    public abstract class Player
    {
        public string Disk { set; get; }

        public Player(string disk)
        {
            Disk = disk;
        }

        public abstract void MakeTurn(Board board);
    }
    // It is a derived from super class player
    public class Human : Player
    {
        public Human(string disk) : base(disk)
        {
        }
     // Allows user to play while there is a column available
        public override void MakeTurn(Board board)
        {
            while (true)
            {
                Console.Write("\n{0} Please enter column number (1-{1}): ", Disk.Trim(), Board.Columns);
                try
                {
                    int col = Int32.Parse(Console.ReadLine());
                    if (col is > 0 and <= Board.Columns)
                    {
                        if (board.IsColumnAvailable(col))
                        {
                            board.DropDisk(col, Disk);
                            return;
                        }
                    }

                    board.Print();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Column is full or does not exist!");
                    Console.ResetColor();
                }
                catch (FormatException)
                {
                    board.Print();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Only numbers allowed!");
                    Console.ResetColor();
                }
            }
        }
    }

// Derived class from super class player
    public class Bot : Player
    {
        public Bot(string disk) : base(disk)
        {
        }

        public override void MakeTurn(Board board)
        {
            while (true)
            {
                var col = new Random().Next(1, Board.Columns + 1);
                if (board.IsColumnAvailable(col))
                {
                    board.DropDisk(col, Disk);
                    return;
                }
            }
        }
    }
    
  // Controls game, allows user to pick 1 or 2 players, creating new objects from human or bot class
  public class Game
    {
        public Player[] InitPlayers()
        {
            Console.Clear();
            Console.WriteLine("{0}{0}Welcome to Take Four!  {1}{1}", Board.BlueDisk, Board.RedDisk);

            while (true)
            {
                Console.Write("Enter number of players (1 or 2): ");
                
                switch (Console.ReadLine())
                {
                    case "1":
                    {
                        Player[] players = { new Human(Board.BlueDisk), new Bot(Board.RedDisk) };
                        return players;
                    }
                    case "2":
                    {
                        Player[] players = { new Human(Board.BlueDisk), new Human(Board.RedDisk) };
                        return players;
                    }
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Wrong input!");
                        Console.ResetColor();
                        break;
                }
            }
        }
    //Checks the winner until there is a win or the board is full
        public void Play(Board board, Player[] players)
        {
            var activePlayer = players[0];
            
            board.Print();
            
            do
            {
                if (board.IsFull())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nSeems like a draw!");
                    Console.ResetColor();
                    return;
                }
                
                // Reassign active player
                activePlayer = activePlayer == players[0] ? players[1] : players[0];
                
                activePlayer.MakeTurn(board);
                board.Print();
                
            } while (!board.CheckWinner());
            
            Thread.Sleep(1000);
            board.Win(activePlayer.Disk);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nPlayer {0} wins!", activePlayer.Disk.Trim());
            Console.ResetColor();
            
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            do
            {
                var game = new Game();
                var players = game.InitPlayers();

                game.Play(new Board(), players);

                Console.Write("\nEnter \"y\" if you want to play more: ");
            } while (Console.ReadLine() == "y");
        }
    }
}
