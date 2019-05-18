using Dedup;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountMatrixCloneDetection
{
    public static class CMCD
    {
        /// <summary>
        /// Method to run the CMCD algorithm
        /// </summary>
        public static List<CMDCDuplicateResult> Run(string path)
        {
            var comparisionResults = new List<CMDCDuplicateResult>();
            try
            {
                var allMethods = new List<CMDCMethod>();
                var files = new List<string>();
                FileAttributes attr = File.GetAttributes(path);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    // it's a directory
                    files = Directory.GetFiles(path, "*.cs", SearchOption.AllDirectories).ToList();

                }
                foreach (var file in files)
                {
                    allMethods.AddRange(GetMethods(file).Select(c => new CMDCMethod() { FilePath = Path.GetFullPath(file),  FileName = Path.GetFileName(file), MethodNode = c }));
                }

                foreach (var methodA in allMethods)
                {
                    foreach (var methodB in allMethods)
                    {
                        try
                        {
                            var compareResult = Compare(methodA.MethodNode, methodB.MethodNode);
                            comparisionResults.Add(new CMDCDuplicateResult()
                            {
                                MethodA = new CMDCMethodInfo() {
                                    FileName = methodA.FileName,
                                    FilePath = Path.GetFullPath(methodA.FilePath),
                                    MethodText = methodA.MethodNode.GetText().ToString(),
                                    EndLineNumber = methodA.MethodNode.FullSpan.End,
                                    StartLineNumber = methodA.MethodNode.FullSpan.Start
                                },
                                MethodB = new CMDCMethodInfo() {
                                    FileName = methodB.FileName,
                                    FilePath = Path.GetFullPath(methodB.FilePath),
                                    MethodText = methodB.MethodNode.GetText().ToString(),
                                    EndLineNumber = methodB.MethodNode.FullSpan.End,
                                    StartLineNumber = methodB.MethodNode.FullSpan.Start
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return comparisionResults;
        }

        /// <summary>
        /// Get all the methods in a .cs file
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <returns>List of methods</returns>
        private static List<SyntaxNode> GetMethods(string filePath)
        {
            var methods = new List<SyntaxNode>();
            if (File.Exists(filePath))
            {
                var programText = File.ReadAllText(filePath);
                var tree = CSharpSyntaxTree.ParseText(programText);
                var root = tree.GetCompilationUnitRoot();

                var child = root.ChildNodes();

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
            }

            return methods;
        }

        /// <summary>
        /// API to compare two methods
        /// </summary>
        /// <param name="methodA">Method A</param>
        /// <param name="methodB">Methods B</param>
        /// <returns>Comparision report between these two methods</returns>
        private static double Compare(SyntaxNode methodA, SyntaxNode methodB)
        {
            var MethodAvariablesCount = SyntaxTreeParser.GetVariablesCount(methodA);
            var methodBVariablesCount = SyntaxTreeParser.GetVariablesCount(methodB);

            double[,] methodAMatrix = GetMatrix(MethodAvariablesCount);
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

            return d2;
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

    /// <summary>
    /// Class to represent a CMDC duplicate result
    /// </summary>
    public class CMDCDuplicateResult
    {
        public CMDCMethodInfo MethodA { get; set; }

        public CMDCMethodInfo MethodB { get; set; }
    }

    /// <summary>
    /// class to represent CMDC method info
    /// </summary>
    public class CMDCMethodInfo
    {
        /// <summary>
        /// Gets or sets the File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the File paths
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the method text
        /// </summary>
        public string MethodText { get; set; }

        /// <summary>
        /// Gets or sets the start line number
        /// </summary>
        public int StartLineNumber { get; set; }

        /// <summary>
        /// Gets or sets the end line number
        /// </summary>
        public int EndLineNumber { get; set; }
    }

    /// <summary>
    /// Class to represent CMDC 
    /// </summary>
    public class CMDCMethod
    {
        /// <summary>
        /// Gets or sets file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets file path
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets method node
        /// </summary>
        public SyntaxNode MethodNode { get; set; }
    }
}
