
namespace ConsoleApp8
{


    public class Board
    {
        public const int Rows = 6;
        public const int Columns = 7;
        private string[,] Array;

        public const string EmptyDisk = "\u25CC   ";
        public const string BlueDisk = "\U0001F535  ";
        public const string RedDisk = "\U0001F534  ";
        
        
        public Board()
        {
            Array = new string [6, 7];

            for (int i = 0; i < Array.GetLength(0); i++) ////filling the Array with #s
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] = "#";
                }

            }
        }

        
        public void Print() /////Displaying Array with all the x and ys
        {
            for (int i = 1; i <= 7; i++)
            {
                Console.Write(i + "    ");
            }

            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    if (j == 6)
                    {
                        Console.Write(Array[i, j]);
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write(Array[i, j] + "    ");
                    }
                }
            }
        }

        
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
        public void DropDisk(int col, string disk)
        {
            for (var row = Rows - 1; row >= 0; row--)
            {
                if (Array[row, col - 1] != EmptyDisk) continue;
                // Animation
                Dropping(row, col-1, disk);
                
                Array[row, col - 1] = disk;
                return;
            }
        }
        
       public  bool IsColumnAvailable(int colNumber)
        {
            return Array[0, colNumber] == "#";
        }
       
       public bool IsFull() ////// checking the whole Array to make sure that it's not empty
        {
            foreach (var cell in Array)
            {
                if (cell == "#")
                {
                    return false;
                }
            }

            return true;
        }
       
       public bool CheckWinner()
        {
            // Check horizontal lines
            for (var row = 0;  row < Rows; row++)
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
            for (var col = 0;  col < Columns; col++)
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
public abstract class Player
{
    public string Disk { set; get; }

    public Player(string disk)
    {
        Disk = disk;
    }
    public abstract void MakeTurn(Board board);
}

public class Human : Player
{
    public Human(string disk): base(disk) {}

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


public class Bot: Player
{
    public Bot(string disk): base(disk) {}

    public override void MakeTurn(Board board)
    {
        while (true)
        {
            var col = new Random().Next(1, Board.Columns+1);
            if (board.IsColumnAvailable(col))
            {
                board.DropDisk(col, Disk);
                return;
            }
        }
    }
}

        internal class Program
        {
            public static void Main(string[] args)
            {
                
                 Board board = new Board();
                 

            }
        }
    }
