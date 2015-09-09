using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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
			int answer = 0;
            int[,] board = new int[width, height];
            Block[,] solution = new Block[width, height];
            Queue<Tuple<int,int>> longest = new Queue<Tuple<int,int>>();

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

            //Start the solution by initializing the locations of the next piece of each block.
            for (int y = 0; y < height; y++)
            {
				for (int x = 0; x < width; x++)
                {
                    //Check Left
                    if (x > 0 && board[x, y] > board[x - 1, y])
                    {
                        solution[x, y].Left.Enqueue(new Tuple<int, int>(x - 1, y));
                    }

                    //Check Up
                    if (y > 0 && board[x, y] > board[x, y - 1])
                    {
                        solution[x, y].Up.Enqueue(new Tuple<int, int>(x, y - 1));
                    }

                    //Check Right 
                    if (x < width - 1 && board[x, y] > board[x + 1, y])
                    {
                        solution[x, y].Right.Enqueue(new Tuple<int, int>(x + 1, y));
                    }

                    //Check Down
                    if (y < height - 1 && board[x, y] > board[x, y + 1])
                    {
                        solution[x, y].Down.Enqueue(new Tuple<int, int>(x, y + 1));
                    }
                }
            }

            //Find the longest length

			Console.WriteLine ("Initialized maze");
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					solution[x, y].maxLength = SearchAlgo.itsPersonal (ref solution, ref board, new Tuple<int, int> (x, y), 0, width, height);
				}
			}
			Console.WriteLine ("Found max lengths");
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					if (answer < solution [x, y].maxLength) {
						answer = solution [x, y].maxLength;
					}
				}
			}
				
			Console.WriteLine (answer);
			Console.WriteLine ("Woof");
        }



        class SearchAlgo
        {
            public static int itsPersonal(ref Block[,] sol, ref int[,] board, Tuple<int,int> curPos, int curLength, int width, int height)
            {
				int[] lengths = new int[4];
                //Check Left
                if (sol[curPos.Item1, curPos.Item2].Left.Count > 0)
                {
					lengths[0] = itsPersonal(ref sol, ref board, new Tuple<int,int>(curPos.Item1-1,curPos.Item2), curLength + 1, width, height);
                }
                //Check Up
                if (sol[curPos.Item1, curPos.Item2].Up.Count > 0)
                {
					lengths[1] = itsPersonal(ref sol, ref board, new Tuple<int, int>(curPos.Item1, curPos.Item2-1), curLength + 1, width, height);
                }

                //Check Right 
                if (sol[curPos.Item1, curPos.Item2].Right.Count > 0)
                {
					lengths[2] = itsPersonal(ref sol, ref board, new Tuple<int, int>(curPos.Item1 + 1, curPos.Item2), curLength + 1, width, height);
                }

                //Check Down
                if (sol[curPos.Item1, curPos.Item2].Down.Count > 0)
                {
					lengths[3] = itsPersonal(ref sol, ref board, new Tuple<int, int>(curPos.Item1, curPos.Item2 + 1), curLength + 1, width, height);
                }
                

				return Math.Max(curLength, lengths.Max());
            }

        }

    }
}
