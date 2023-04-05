// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;

namespace Activity_04
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] column1 = new char[6] {'#','#','#','#','#','#'};
            char[] column2 = new char[6] {'#','#','#','#','#','#'};
            char[] column3 = new char[6] {'#','#','#','#','#','#'};
            char[] column4 = new char[6] {'#','#','#','#','#','#'};
            char[] column5 = new char[6] {'#','#','#','#','#','#'};
            char[] column6 = new char[6] {'#','#','#','#','#','#'};
            char[] column7 = new char[6] {'#','#','#','#','#','#'};

            char[][] jArray = new char[6][];
            jArray[0] = new char[7]{'#','#','#','#','#','#','#'};
            jArray[1] = new char[7]{'#','#','#','#','#','#','#'};
            jArray[2] = new char[7]{'#','#','#','#','#','#','#'};
            jArray[3] = new char[7]{'#','#','#','#','#','#','#'};
            jArray[4] = new char[7]{'#','#','#','#','#','#','#'};
            jArray[5] = new char[7]{'#','#','#','#','#','#','#'};

            foreach (var item in jArray)
            {
                foreach (var element in item)
                {
                    Console.Write(element + " ");
                    
                }
                Console.WriteLine();
            }
            //Played by player 1
            int Counting = 0;
            int Pressed = 1;
            jArray[5-Counting][Pressed-1] = 'x';
            
            Console.WriteLine("**************");
            foreach (var item in jArray)
            {
                foreach (var element in item)
                {
                    Console.Write(element + " ");
                    
                }
                Console.WriteLine();
            }

            Counting += 1;
            Pressed = 2;
            jArray[5-Counting][Pressed-1] = 'o';
            Console.WriteLine("**************");
            foreach (var item in jArray)
            {
                foreach (var element in item)
                {
                    Console.Write(element + " ");
                    
                }
                Console.WriteLine();
            }
        }
    }
}