using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

namespace MazeSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            for (int i = 0; i < 3; i++)
            {
                directory = Directory.GetParent(directory).FullName;
            }
            string sourceFile = directory + @"\RPAMaze.txt";

            StreamReader sr = File.OpenText(sourceFile);

            //reading height and width
            string singleLine = sr.ReadLine();
            string[] heightAndWidth = singleLine.Split(' ');
            int height = Int32.Parse(heightAndWidth[0]);
            int width = Int32.Parse(heightAndWidth[1]);
            Maze maze = new Maze(width, height);

            //parsing file line by line
            for (int row = 0; (singleLine = sr.ReadLine()) != null; row++)
            {
                int col = 0;
                foreach (char c in singleLine) {

                    if (c != ' ')
                    {
                        maze.array[row, col++] = c - '0';
                        
                        if (c == '2')
                        {
                            maze.SetCurrent(new Point(row, col - 1));
                            //Console.WriteLine("Start position is (" + row + "," + (col - 1) + ")");
                        }
                    }
                }
            }

            Console.Write("Hello. Would you like to select a custom starting position? Type \"yes\" or \"no\": ");
            string customStartAnswer = Console.ReadLine();
            if (customStartAnswer.ToUpper() == "YES")
            {
                Console.WriteLine("Please select a starting position (and remember - index starts at 0): ");
                int newStartCol, newStartRow;
                do
                {
                    Console.Write("Row: ");
                    Int32.TryParse(Console.ReadLine(), out newStartRow);
                    Console.Write("Column: ");
                    Int32.TryParse(Console.ReadLine(), out newStartCol);
                } while (maze.SetNewCurrent(new Point(newStartRow, newStartCol)) != 0);
            }

            Console.WriteLine("Your starting position is (" + maze.current.X + "," + maze.current.Y + ")");

            maze.Print();
            maze.Solve();
            Console.WriteLine("The path was saved to Result.txt. Thanks for playing :)");

            string result = directory + @"\Log.txt";

            //creating Result.txt file
            string mazePrint = "";
            for (int r = 0; r < width; r++)
            {
                for (int c = 0; c < height; c++)
                {
                    if (maze.array[r, c] == 3)
                    {
                        mazePrint += "* ";
                    } else
                    {
                        mazePrint += maze.array[r, c] + " ";
                    }
                }

                mazePrint += Environment.NewLine;
            }

            File.WriteAllText(result, "The path is marked with *" + Environment.NewLine);
            File.AppendAllText(result, mazePrint);
        }

    }
}
