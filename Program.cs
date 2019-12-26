using System;
using System.Collections;
using System.Collections.Generic;

namespace OAcoding
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /*

              [[2,1,1],
               [1,1,0],
               [0,1,1]]

             */
            var grid = new int[][] {
                new int[]{ 0,2 }

            };
            Console.WriteLine(OrangesRotting(grid));
            Console.ReadKey();
        }

        public enum Fresh
        {
            noFreash = 0,
            fresh = 1,
            rotten = 2
        }

        public class Point
        {

            public int r, c;
            public Point(int r, int c)
            {
                this.r = r;
                this.c = c;
            }
        }
        public static int OrangesRotting(int[][] grid)
        {
            /*
            [[2,1,1],
             [0,1,1],
             [1,0,1]]

            */

            if (grid == null) return -1;

            int countFreshs = 0, countNoFreshs = 0;
            int row = grid.Length, col = grid[0].Length;
            int countCells = row * col;
            Queue<Point> rottens = new Queue<Point>();

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < col; c++)
                {
                    if (grid[r][c] == (int)Fresh.rotten)
                    {
                        rottens.Enqueue(new Point(r, c));
                        continue;
                    }

                    if (grid[r][c] == (int)Fresh.noFreash)
                    {
                        countNoFreshs++;
                        continue;
                    }
                    if (grid[r][c] == (int)Fresh.fresh)
                    {
                        countFreshs++;
                    }
                }
            }

            if (countFreshs == 0)
            {
                return 0;
            }

            int[][] DIRS = { new int[] {0, 1 },
                             new int[] {1, 0 },
                             new int[] {0,-1 },
                             new int[] {-1,0 }};

            for (int min = 1; rottens.Count > 0; min++)
            {
                for (int i = rottens.Count; i > 0; i--)
                {
                    Point p = rottens.Dequeue();
                    foreach (var item in DIRS)
                    {
                        int r = p.r + item[0];
                        int c = p.c + item[1];
                        var isfresh = IsFresh(grid, r, c);
                        if (isfresh)
                        {
                            countFreshs--;
                            if (countFreshs == 0) return min;
                            grid[r][c] = (int)Fresh.rotten;
                            rottens.Enqueue(new Point(r, c));
                        }
                    }
                }
            }
            return -1;
        }

        private static bool IsFresh(int[][] grid, int r, int c)
        {

            return r >= 0 && r < grid.Length && c >= 0 &&
                c < grid[0].Length && grid[r][c] == (int)Fresh.fresh;
        }
    }
}
