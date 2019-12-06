using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace MazeSolver
{
    public class Maze
    {
        int height { get; set; }
        int width {get; set;}
        public int[,] array;
        public Point current { get; set; }
        public Point start { get; set; }
        public List<Point> visited = new List<Point>();
        public Stack<Point> path = new Stack<Point>();

        public Maze(int width, int height)
        {
            array = new int[width, height];
            this.width = width;
            this.height = height;
        }

        public void SetCurrent(Point value)
        {
            visited.Add(value);
            path.Push(value);
            this.current = value;
            start = value;
        }

        public int SetNewCurrent(Point value)
        {
            if (array[value.X, value.Y] != 0)
            {
                Console.WriteLine("The position you entered is illegal. Try again!");
                return -1;
            }
            array[current.X, current.Y] = 0;
            visited.Remove(current);
            path.Pop();

            array[value.X, value.Y] = 2;
            visited.Add(value);
            path.Push(value);
            this.current = value;
            start = value;
            return 0;
        }

        public void Print()
        {
            for (int r = 0; r < width; r++)
            {
                for (int c = 0; c < height; c++)
                {
                    Console.Write(array[r, c] + "  ");
                }
                Console.WriteLine();
            }

            
            Console.WriteLine();
            Console.WriteLine();
        }

        public int Move(Point dest)
        {
            if (dest.X < 0 | dest.Y < 0 | dest.X > width | dest.Y > height)
            {
                return -1;
            }
            if (array[dest.X, dest.Y] != 1)
            {
                array[current.X, current.Y] = 0;
                array[dest.X, dest.Y] = 2;
                //Console.WriteLine("was " + current.X + ", " + current.Y + " NEW IS " + dest.X + "," + dest.Y);
                current = dest;
                path.Push(current);
                visited.Add(current);
                Print();
                return 0;
            } else
            {
                return 1;
            }
        }

        public void Solve()
        {
            Point dest;

            if (foundExit())
            {
                return;
            }

            //try going left
            if (!visited.Contains(dest = new Point(current.X, current.Y - 1)) && Move(dest) == 0)
            {
                Solve();
            }
            
            if (foundExit())
            {
                return;
            }
            
            //try going right
            if (!visited.Contains(dest = new Point(current.X, current.Y + 1)) && Move(dest) == 0)
            {
                Solve();
            }

            if (foundExit())
            {
                return;
            }

            //try going down
            if (!visited.Contains(dest = new Point(current.X + 1, current.Y)) && Move(dest) == 0)
            {
                Solve();
            }

            if (foundExit())
            {
                return;
            }

            //try going up
            if (!visited.Contains(dest = new Point(current.X - 1, current.Y)) && Move(dest) == 0)
            {
                Solve();
            } 

            if (foundExit())
            {
                return;
            }
            //Point backtrack = path.Pop();
            MoveBack();
            return;
            }

        //backtrack
        public void MoveBack()
        {   
            Point back = path.Pop();
            if (back.X == current.X && back.Y == current.Y)
            {
                back = path.Pop();
            }

            array[current.X, current.Y] = 0;
            array[back.X, back.Y] = 2;
            //Console.WriteLine("BACKTRACK! Was " + current.X + ", " + current.Y + " NEW IS " + back.X + "," + back.Y);
            current = back;
            Print();
            path.Push(current);
        }



        public bool foundExit()
        {
            if (current.X == 0 | current.X == width - 1 |
                current.Y == 0 | current.Y == height - 1)
            {

                Point temp;
                temp = path.Pop();
                //Console.Write("(" + temp.X + "," + temp.Y + ")  ");
                if (path.Count != 0)
                {
                    array[temp.X, temp.Y] = 3;
                } else
                {
                    array[temp.X, temp.Y] = 2;
                }
                return true;
            }

            return false;
        }
    }

}
