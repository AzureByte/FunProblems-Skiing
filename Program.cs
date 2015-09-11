using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace malasiya_surfing
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream surf = new FileStream("./map.txt", FileMode.Open);
            StreamReader sr = new StreamReader(surf);

            string line = sr.ReadLine();
            string[] size = line.Split(' ');
            int width = int.Parse(size[0]);
            int height = int.Parse(size[1]);
            int maxLength = 0;
            int maxDepth = 0;
            int[,] board = new int[width, height];
            Block[,] solution = new Block[width, height];
            Tuple<int, int> curSol;
            Stack<Tuple<int, int>> longest = new Stack<Tuple<int, int>>();

            //Initialize the solution array
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    solution[x, y] = new Block();
                }
            }

            //Read the file and populate the board
            for (int y = 0; y < height; y++)
            {
                line = sr.ReadLine();
                string[] row = line.Split(' ');
                for (int x = 0; x < width; x++)
                {
                    board[x, y] = int.Parse(row[x]);
                }
            }
            Console.WriteLine("Read the file");

            //Initialize the solution array with the locations of the next piece of each block.
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    //Check Left
                    if (x > 0 && board[x, y] > board[x - 1, y])
                        solution[x, y].Left = true;

                    //Check Up
                    if (y > 0 && board[x, y] > board[x, y - 1])
                        solution[x, y].Up = true;

                    //Check Right 
                    if (x < width - 1 && board[x, y] > board[x + 1, y])
                        solution[x, y].Right = true;

                    //Check Down
                    if (y < height - 1 && board[x, y] > board[x, y + 1])
                        solution[x, y].Down = true;
                }
            }
            Console.WriteLine("Initialized maze");

            //Find the longest length of each block
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    curSol = SearchAlgo.itsPersonal(ref solution, ref board, new Tuple<int, int>(x, y), 1, 0, width, height);
                    solution[x, y].maxLength = curSol.Item1;
                    solution[x, y].maxDepth = curSol.Item2;

                    if (maxLength <= curSol.Item1)
                    {
                        if (maxLength == curSol.Item1)
                        {
                            if (maxDepth < curSol.Item2)
                            {
                                maxDepth = curSol.Item2;
                            }
                        }
                        else
                        {
                            maxLength = curSol.Item1;
                            maxDepth = curSol.Item2;
                        }
                    }
                }
            }
            Console.WriteLine("Max length");
            Console.WriteLine(maxLength);

            Console.WriteLine("Max depth");
            Console.WriteLine(maxDepth);

            Console.WriteLine("Woof");
            Console.ReadLine();
        }



        class SearchAlgo
        {
            public static Tuple<int,int> itsPersonal(ref Block[,] sol, ref int[,] board, Tuple<int, int> curPos, int curLength, int curDepth, int width, int height)
            {
                Tuple<int, int> curFinding = new Tuple<int,int>(curLength, curDepth);
                Tuple<int, int>[] lengths = new Tuple<int, int>[4];
                //Check Left
                if (sol[curPos.Item1, curPos.Item2].Left)
                {
                    curFinding = itsPersonal(ref sol, ref board, new Tuple<int, int>(curPos.Item1 - 1, curPos.Item2), curLength + 1, curDepth + board[curPos.Item1,curPos.Item2] - board[curPos.Item1 - 1, curPos.Item2], width, height);
                    lengths[0] = new Tuple<int,int>(curFinding.Item1, curFinding.Item2);
                }
                //Check Up
                if (sol[curPos.Item1, curPos.Item2].Up)
                {
                    curFinding = itsPersonal(ref sol, ref board, new Tuple<int, int>(curPos.Item1, curPos.Item2 - 1), curLength + 1, curDepth + board[curPos.Item1,curPos.Item2] - board[curPos.Item1, curPos.Item2 - 1], width, height);
                    lengths[1] = new Tuple<int, int>(curFinding.Item1, curFinding.Item2);
                }

                //Check Right 
                if (sol[curPos.Item1, curPos.Item2].Right)
                {
                    curFinding = itsPersonal(ref sol, ref board, new Tuple<int, int>(curPos.Item1 + 1, curPos.Item2), curLength + 1, curDepth + board[curPos.Item1,curPos.Item2] - board[curPos.Item1 + 1, curPos.Item2], width, height);
                    lengths[2] = new Tuple<int, int>(curFinding.Item1, curFinding.Item2);
                }

                //Check Down
                if (sol[curPos.Item1, curPos.Item2].Down)
                {
                    curFinding = itsPersonal(ref sol, ref board, new Tuple<int, int>(curPos.Item1, curPos.Item2 + 1), curLength + 1, curDepth + board[curPos.Item1,curPos.Item2] - board[curPos.Item1, curPos.Item2 + 1], width, height);
                    lengths[3] = new Tuple<int, int>(curFinding.Item1, curFinding.Item2);
                }

                foreach (Tuple<int, int> value in lengths)
                {
                    if (value != null && value.Item1 >= curFinding.Item1)
                    {
                        if (value.Item1 == curFinding.Item1)
                        {
                            if (value.Item2 >= curFinding.Item2)
                            {
                                curFinding = new Tuple<int, int>(value.Item1, value.Item2);
                            }
                        }
                        else
                        {
                            curFinding = new Tuple<int, int>(value.Item1, value.Item2);
                        }
                    }
                }
                               
                return new Tuple<int,int>(curFinding.Item1, curFinding.Item2);
            }
        }
    }
}
