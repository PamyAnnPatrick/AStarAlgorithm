// See https://aka.ms/new-console-template for more information
using AStarAlgorithm;


int[][] m = new int[9][] {
new int[] {0,0,1,0,0,1,0,1,0},
new int[] {0,0,0,0,0,0,0,0,0 },
new int[] {0,1,1,0,1,1,1,1,1},
new int[] {0,0,0,1,0,0,0,0,0 },
new int[] {1,1,0,0,0,1,0,0,0 },
new int[] {1,0,1,0,0,1,0,0,1 },
new int[] {1,1,1,1,0,0,1,0,0 },
new int[] {1,0,0,1,0,0,1,1,1 },
new int[] {0,0,0,0,0,0,0,0,0 }
};
Solution sol = new();
Console.WriteLine(sol.ShortestPathBinaryMatrix(m));
