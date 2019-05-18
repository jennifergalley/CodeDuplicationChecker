using System;

namespace Dedup
{
    public class Bipartite
    {
        public static int[] GetBipartiteMatrix(double[,] u, double[,] v)
        {
            var p = CreateBipartiteMatrix(u, v);
            return HungarianAlgorithm.HungarianAlgorithm.FindAssignments(p);
        }
        
        private static int[,] CreateBipartiteMatrix(double[,] u, double[,] v)
        {
            var rowLen = u.GetLength(0);
            var colLength = u.GetLength(1);
            var adjacent = new int[rowLen, rowLen];

            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < rowLen; j++)
                {
                    var temp = 0.0;
                    for (int k = 0; k < colLength; k++)
                    {
                        temp += Math.Pow((u[i, k] - v[j, k]), 2);
                    }

                    //Cast to int
                    adjacent[i, j] = (int)Math.Sqrt(temp);
                }
            }

            return adjacent;
        }
    }
}
