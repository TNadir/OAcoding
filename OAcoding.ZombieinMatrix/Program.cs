using System;
using System.Collections.Generic;

namespace OAcoding.ZombieinMatrix
{
    //zombieees
    class MainClass
    {
        public static void Main(string[] args)
        {
            /*
                   [[0, 1, 1, 0, 1],
                    [0, 1, 0, 1, 0],
                    [0, 0, 0, 0, 1],
                    [0, 1, 0, 0, 0]]
             */
            var grid = new List<List<int>>();
            grid.Add(new List<int> { 0, 1, 1, 0, 1 });
            grid.Add(new List<int> { 0, 1, 0, 1, 0 });
            grid.Add(new List<int> { 0, 0, 0, 0, 1 });
            grid.Add(new List<int> { 0, 1, 0, 0, 0 });
            grid.Add(new List<int> { 0, 1, 0, 0, 0 });
            var zm = new ZombiesInMatrix();

            Console.WriteLine(zm.MinHours(grid));

            Console.ReadKey();
        }

        public class ZombiesInMatrix
        {
            class Point
            {
                public int r;
                public int c;
                public Point(int r, int c)
                {
                    this.r = r;
                    this.c = c;
                }
            }
            private static readonly int ZOMBIE = 1;
            private static int[][] DIRS ={
             new int[]{ 0, 1 },
             new int[]{ 1, 0 },
             new int[]{ 0, -1 },
             new int[]{ -1, 0 }};

            public int MinHours(List<List<int>> grid)
            {

                if (grid == null)
                {
                    return 0;
                }

                int people = 0;
                Queue<Point> zombies = new Queue<Point>();

                for (int r = 0; r < grid.Count; r++)
                {
                    for (int c = 0; c < grid[0].Count; c++)
                    {
                        if (grid[r][c] == ZOMBIE)
                        {
                            zombies.Enqueue(new Point(r, c));
                            continue;
                        }
                        people++;
                    }
                }
                if (people == 0) return 0;
                /*
                   [[0, 1, 1, 0, 1],
                    [0, 1, 0, 1, 0],
                    [0, 0, 0, 0, 1],
                    [0, 1, 0, 0, 0]]

                          ||
                          \/ 

                   [[1, 1, 1, 1, 1],
                    [1, 1, 1, 1, 1],
                    [0, 1, 0, 1, 1],
                    [1, 1, 1, 0, 1]]
                 */

                for (int hours = 1; zombies.Count > 0; hours++)
                {
                    for (int zs = zombies.Count; zs > 0; zs--)
                    {
                        Point p = zombies.Dequeue();

                        foreach (var direct in DIRS)
                        {
                            int r = p.r + direct[0];
                            int c = p.c + direct[1];

                            if (IsHuman(grid, r, c))
                            {
                                people--;
                                if (people == 0) return hours;
                                grid[r][c] = ZOMBIE;
                                zombies.Enqueue(new Point(r, c));
                            }
                        }
                    }
                }

                return 0;

            }

            private static bool IsHuman(List<List<int>> grid, int r, int c)
            {
                return r >= 0 && r < grid.Count && c >= 0 && c < grid[0].Count && grid[r][c] != ZOMBIE;
            }
        }


    }



}
