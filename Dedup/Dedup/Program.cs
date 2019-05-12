using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SyntaxKind = Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace Dedup
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var programText = File.ReadAllText(@"C:\cs590\Dedup\Dedup\DupClass.cs");
            var tree = CSharpSyntaxTree.ParseText(programText);
            var root = tree.GetCompilationUnitRoot();

            var child = root.ChildNodes();
            var methods = new List<SyntaxNode>();

            foreach (var syntaxNode in child)
            {
                var kind = syntaxNode.Kind();
                if (kind != SyntaxKind.NamespaceDeclaration && kind != SyntaxKind.ClassDeclaration &&
                    kind != SyntaxKind.MethodDeclaration)
                {
                    continue;
                }

                if (kind == SyntaxKind.MethodDeclaration)
                {
                    methods.Add(syntaxNode);
                }

                if (kind == SyntaxKind.ClassDeclaration)
                {
                    methods.AddRange(SyntaxTreeParser.GetMethodsFromClassNode(syntaxNode));
                }

                if (kind == SyntaxKind.NamespaceDeclaration)
                {
                    methods.AddRange(SyntaxTreeParser.GetMethodsFromNamespace(syntaxNode));
                }
            }

            var v1 = SyntaxTreeParser.GetVariablesCount(methods[0]);
            var v2 = SyntaxTreeParser.GetVariablesCount(methods[1]);

            double[,] u = GetMatrix(v1);
            double[,] v = GetMatrix(v2);
                       

            var maxLen = u.GetLength(0);
            if (u.GetLength(0) < v.GetLength(0))
            {
                u = ZeroPadMatrix(u, v.GetLength(0) - u.GetLength(0));
                maxLen = v.GetLength(0);
            }
            else if (u.GetLength(0) > v.GetLength(0))
            {
                v = ZeroPadMatrix(v, u.GetLength(0) - v.GetLength(0));
            }
                        
            //for (int i = 0; i < u.GetLength(0); i++)
            //{
            //    for (int j = 0; j < u.GetLength(1); j++)
            //    {
            //        Console.Write(u[i, j] +" ");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("v");
            //for (int i = 0; i < u.GetLength(0); i++)
            //{
            //    for (int j = 0; j < u.GetLength(1); j++)
            //    {
            //        Console.Write(v[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}

            var d1 = EuclideanDistance(u, v);
            var mapping = Bipartite.GetBipartiteMatrix(u, v);
            var d2 = EuclideanDistance(u, v, mapping);

            Console.WriteLine(d1);
            Console.WriteLine(d2); 
        }

        private static double CalculateSimilarityDistance(double[,] a, double[,] b)
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

        private static double EuclideanDistance(double[,] a, double[,] b)
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

        private static double EuclideanDistance(double[,] a, double[,] b, int[] mapping)
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
        
        private static double[,] GetMatrix(IReadOnlyList<VariableName> variables)
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

        private static double[,] ZeroPadMatrix(double[,] original, int numberOfRows)
        {
            if (original == null || numberOfRows <= 0)
            {
                throw new ArgumentException("original matrix is null or numberOfRows is less or equal to zero.");
            }

            var result = new double[original.GetLength(0) + numberOfRows, original.GetLength(1)];
            Array.Copy(original, 0, result, 0, original.Length);
            return result;
        }
    }
}
