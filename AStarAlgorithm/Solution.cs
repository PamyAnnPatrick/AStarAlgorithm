using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarAlgorithm
{
    public class Solution
    {
        public int ShortestPathBinaryMatrix(int[][] grid)
        {
            int n = grid.Length;

            //Return -1 , if start or end is 1.
            if (grid[0][0] == 1 || grid[n - 1][n - 1] == 1)
                return -1;

            PriorityQueue<(int r, int c), double> openQueue = new();
            //To search with o(1) and avoid duplicate entry 
            //in priority queue
            HashSet<(int r, int c)> openHash = new();
            HashSet<(int r, int c)> closeHash = new();
            //Can ignore this dictionary to find the solution, this
            //is just to trace down the path
            Dictionary<(int r, int c), (int r, int c)> parentDict = new();

            //Add root to queue
            grid[0][0] = 1;
            openQueue.Enqueue((0, 0), GetFX((0, 0), grid, n));
            openHash.Add((0, 0));

            while (openQueue.Count > 0)
            {
                List<(int r, int c)> currents = new();
                double priority, nextPriority;
                do
                {
                    (int r, int c) cur = new();
                    openQueue.TryPeek(out cur, out priority);
                    currents.Add(cur);
                    openQueue.Dequeue();
                    openQueue.TryPeek(out cur, out nextPriority);
                } while (priority == nextPriority);

                //To process node of same priority
                foreach (var current in currents)
                {
                    //To handle duplicate entries in priority queue.
                    if (!openHash.Contains(current))
                        continue;

                    openHash.Remove(current);

                    //Already reached goal.
                    if (current.r == n - 1 && current.c == n - 1)
                        return (grid[n - 1][n - 1] > 0) ? grid[n - 1][n - 1] : -1;


                    List<(int r, int c)> children = GetChildren(grid, current);

                    int g_current = GetGX(current, grid);

                    foreach (var c in children)
                    {
                        int child_current_cost = g_current + 1;

                        if (openHash.Contains(c))
                        {
                            int g_child = GetGX(c, grid);
                            if (g_child <= child_current_cost)
                                continue;
                        }
                        else if (closeHash.Contains(c))
                        {
                            int g_child = GetGX(c, grid);
                            if (g_child <= child_current_cost)
                                continue;

                            //Move Node from closed to open
                            closeHash.Remove(c);
                        }

                        // Remove and add new parent.
                        if (parentDict.ContainsKey(c))
                            parentDict.Remove(c);

                        parentDict.Add(c, current);
                        grid[c.r][c.c] = child_current_cost;

                        openHash.Add(c);
                        openQueue.Enqueue(c, GetFX(c, grid, n));
                    }
                    closeHash.Add(current);
                }
            }
            return (grid[n - 1][n - 1] > 0) ? grid[n - 1][n - 1] : -1;
        }


        int GetGX((int r, int c) cell, int[][] grid)
        {
            return grid[cell.r][cell.c];
        }

        double GetHX((int r, int c) cell, int n)
        {
            double dx = Math.Abs(cell.r - n - 1);
            double dy = Math.Abs(cell.c - n - 1);
            return Math.Max(dx, dy);
        }
        double GetFX((int r, int c) cell, int[][] grid, int n)
        {
            return GetGX(cell, grid) + GetHX(cell, n);
        }
        List<(int r, int c)> GetChildren(int[][] grid, (int r, int c) current)
        {
            int n = grid.Length;
            List<(int r, int c)> children = new();

            //All possible children.
            children.Add(((current.r - 1), (current.c - 1)));
            children.Add(((current.r - 1), (current.c)));
            children.Add(((current.r - 1), (current.c + 1)));
            children.Add(((current.r), (current.c + 1)));
            children.Add(((current.r), (current.c - 1)));
            children.Add(((current.r + 1), (current.c - 1)));
            children.Add(((current.r + 1), (current.c)));
            children.Add(((current.r + 1), (current.c + 1)));

            children.RemoveAll(c => c.c < 0 || c.c >= n || c.r < 0
                                        || c.r >= n);
            children.RemoveAll(c => grid[c.r][c.c] == 1);

            return children;
        }
    }
}
