using System;

namespace CountMatrixCloneDetection
{
    public class Bipartite
    {
        /// <summary>
        /// This method will use Hungarian Algorithm to determined the best match of every row
        /// from first matrix to second matrix provided that the total euclidean distance is minimum.
        /// </summary>
        /// <param name="u">matrix of variable counts</param>
        /// <param name="v">matrix of variable counts</param>
        /// <returns></returns>
        public static int[] GetBipartiteMatrix(double[,] u, double[,] v)
        {
            var p = CreateBipartiteMatrix(u, v);
            return HungarianAlgorithm.HungarianAlgorithm.FindAssignments(p);
        }

        /// <summary>
        /// Create Bipartite matrix from two disjoint matrix. The algorithm will calculate euclidean distance
        /// from one row to each row of the second matrix and use that as weight of an edge.At the end there is edge
        /// connected each row from one matrix to other matrix with associated weight.
        /// </summary>
        /// <param name="u">matrix of variable counts</param>
        /// <param name="v">matrix of variable counts</param>
        /// <returns></returns>
        internal static int[,] CreateBipartiteMatrix(double[,] u, double[,] v)
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

                    //Cast to int. Instead of modifying the Hungarian algorithm to double/float and do comparison with epsilon
                    //I choose to cast to int, since we are dealing with count and decimal values will not significantly affect
                    //the outcome.
                    adjacent[i, j] = (int)Math.Sqrt(temp);
                }
            }

            return adjacent;
        }
    }
}
