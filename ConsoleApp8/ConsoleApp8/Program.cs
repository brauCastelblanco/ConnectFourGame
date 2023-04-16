
namespace ConsoleApp8
{


    public class Board
    {
        
        public string [,] Array { get; set; }
        private int Rows = 7;
        private int Columns = 6;
        private string EmptyDisk = "#";

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


        public void AddDisk(int colNumber,
            string player) ///// adding x or y if space is empty players x or y will take the space
        {
            for (int row = 5; row >= 0; row--)
            {
                if (Array[row, colNumber - 1] == "#")
                {
                    Array[row, colNumber - 1] = player;
                    return;
                }
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
       
    

}

        internal class Program
        {
            public static void Main(string[] args)
            {
                
                 Board board = new Board(); // so far we can make a new board and add xs and ys
                
                board.AddDisk(4, "x");
                board.AddDisk(3, "y");
                board.AddDisk(2, "y");
                board.AddDisk(4, "x");
                board.Print();

            }
        }
    }
