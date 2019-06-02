﻿using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Models;

namespace CountMatrixCloneDetection
{
    public class CMCD : ICodeComparer
    {
        /// <summary>
        /// If the methods are completely different according to heuristic, this will be the default score.
        /// </summary>
        internal const double CompletelyDifferentDefaultScore = double.MaxValue;

        /// <summary>
        /// If the number of nodes difference is greater than this number on each level of abstract syntax tree, the method
        /// will be evaluated as different.
        /// </summary>
        internal const int MaxAllowedNodePerLevelDifference = 3;

        /// <summary>
        /// If the total number of node in abstract syntax tree is greater than this value, the method
        /// will be evaluated as different.
        /// </summary>
        internal const int MaxAllowedTotalDifference = 10;

        /// <summary>
        /// If the total variable difference between two method is greater than this value, the method
        /// will be evaluated as different.
        /// </summary>
        internal const int MaxAllowedVariableDifference = 7;

        /// <summary>
        /// If the number of variables is less or equal this number, the distance between will not be normalized, instead we
        /// used the absolute difference as score.
        /// </summary>
        internal const int MinimumVariableLengthToNormalize = 3;

        /// <summary>
        /// API to compare two methods
        /// </summary>
        /// <param name="methodA">Method A</param>
        /// <param name="methodB">Methods B</param>
        /// <returns>Comparision report between these two methods</returns>
        public double Compare(Method methodA, Method methodB)
        {
            var methodAVariablesCount = SyntaxTreeParser.GetVariablesCount(methodA.MethodNode, out var methodANodeCountPerLevel);
            var methodBVariablesCount = SyntaxTreeParser.GetVariablesCount(methodB.MethodNode, out var methodBNodeCountPerLevel);

            var shouldRunClonedDetection = methodANodeCountPerLevel.Count > methodBNodeCountPerLevel.Count ?
                ShouldRunCountMatrixClonedDetection(methodANodeCountPerLevel, methodBNodeCountPerLevel) :
                ShouldRunCountMatrixClonedDetection(methodBNodeCountPerLevel, methodANodeCountPerLevel);

            if (!shouldRunClonedDetection)
            {
                return CompletelyDifferentDefaultScore;
            }

            double[,] methodAMatrix = GetMatrix(methodAVariablesCount);
            double[,] methodBMatrix = GetMatrix(methodBVariablesCount);
            var maxLen = methodAMatrix.GetLength(0);

            if (methodAMatrix.GetLength(0) < methodBMatrix.GetLength(0))
            {
                methodAMatrix = ZeroPadMatrix(methodAMatrix, methodBMatrix.GetLength(0) - methodAMatrix.GetLength(0));
                maxLen = methodBMatrix.GetLength(0);
            }
            else if (methodAMatrix.GetLength(0) > methodBMatrix.GetLength(0))
            {
                methodBMatrix = ZeroPadMatrix(methodBMatrix, methodAMatrix.GetLength(0) - methodBMatrix.GetLength(0));
            }

            var d1 = EuclideanDistance(methodAMatrix, methodBMatrix);
            var mapping = Bipartite.GetBipartiteMatrix(methodAMatrix, methodBMatrix);
            var d2 = EuclideanDistance(methodAMatrix, methodBMatrix, mapping);

            if (maxLen > MinimumVariableLengthToNormalize)
            {
                // Normalize Euclidean Distance by the number of variables
                d2 /= maxLen;
            }

            if (d2 >= 0)
            {
                Console.WriteLine(d2);
            }

            return d2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal static double CalculateSimilarityDistance(double[,] a, double[,] b)
        {
            var maxlength = Math.Max(a.GetLength(0), b.GetLength(0));
            const int smallLen = 7;

            if (maxlength > 2 * a.GetLength(0) || maxlength > 2 * b.GetLength(0) || Math.Abs(a.GetLength(0) - b.GetLength(0)) >= 7)
            {
                return 1000;
            }

            if (maxlength <= smallLen)
            {
                return EuclideanDistance(a, b);
            }

            return EuclideanDistance(a, b);
        }

        /// <summary>
        /// Compute the flat Euclidean distance assuming that each rows are
        /// mapped 1-to-1
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal static double EuclideanDistance(double[,] a, double[,] b)
        {
            var smaller = a.GetLength(0) < b.GetLength(0) ? a : b;
            var sum = 0.0;

            for (var i = 0; i < smaller.GetLength(0); i++)
            {
                for (var j = 0; j < smaller.GetLength(1); j++)
                {
                    sum += Math.Pow((a[i, j] - b[i, j]), 2);
                }
            }

            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Compute the Euclidean distance with row mappings
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="mapping"></param>
        /// <returns></returns>
        internal static double EuclideanDistance(double[,] a, double[,] b, int[] mapping)
        {
            var smaller = a.GetLength(0) < b.GetLength(0) ? a : b;
            var sum = 0.0;

            for (var i = 0; i < smaller.GetLength(0); i++)
            {
                for (var j = 0; j < smaller.GetLength(1); j++)
                {
                    sum += Math.Pow((a[i, j] - b[mapping[i], j]), 2);
                }
            }

            return Math.Sqrt(sum);
        }

        /// <summary>
        /// Convert list variables into 2d array
        /// </summary>
        /// <param name="variables"></param>
        /// <returns></returns>
        internal static double[,] GetMatrix(IReadOnlyList<VariableName> variables)
        {
            if (variables == null || variables.Count <= 0)
            {
                throw new ArgumentException("variable is null or empty");
            }

            var array = new double[variables.Count, variables[0].ToArray().Length];

            for (int i = 0; i < variables.Count; i++)
            {
                var variableArray = variables[i].ToArray();
                for (int j = 0; j < variableArray.Length; j++)
                {
                    array[i, j] = variableArray[j];
                }
            }

            return array;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="numberOfRows"></param>
        /// <returns></returns>
        internal static double[,] ZeroPadMatrix(double[,] original, int numberOfRows)
        {
            if (original == null || numberOfRows <= 0)
            {
                throw new ArgumentException("original matrix is null or numberOfRows is less or equal to zero.");
            }

            var result = new double[original.GetLength(0) + numberOfRows, original.GetLength(1)];
            Array.Copy(original, 0, result, 0, original.Length);
            return result;
        }

        /// <summary>
        /// This method runs heuristics and determine if is necessary to run clone detection algorithm
        /// or evaluate them methods as different.
        /// </summary>
        /// <param name="largerNodeCountPerLevel"></param>
        /// <param name="smallerNodeCountPerLevel"></param>
        /// <returns></returns>
        internal static bool ShouldRunCountMatrixClonedDetection(IReadOnlyDictionary<int, int> largerNodeCountPerLevel, IReadOnlyDictionary<int, int> smallerNodeCountPerLevel)
        {
            if (largerNodeCountPerLevel.Count >= 2 * smallerNodeCountPerLevel.Count ||
                Math.Abs(largerNodeCountPerLevel.Count - smallerNodeCountPerLevel.Count) >= MaxAllowedVariableDifference)
            {
                return false;
            }

            if (smallerNodeCountPerLevel.Any(nodeCount => Math.Abs(nodeCount.Value - largerNodeCountPerLevel[nodeCount.Key]) >
                                                          MaxAllowedNodePerLevelDifference))
            {
                return false;
            }

            if (Math.Abs(largerNodeCountPerLevel.Sum(e => e.Value) - smallerNodeCountPerLevel.Sum(e => e.Value)) > MaxAllowedTotalDifference)
            {
                return false;
            }

            return true;
        }
    }
}
