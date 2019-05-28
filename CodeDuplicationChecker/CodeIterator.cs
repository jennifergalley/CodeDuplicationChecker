using Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Interfaces;

namespace CodeDuplicationChecker
{
    public class CodeIterator
    {
        /// <summary>
        /// Takes either a file directory or a filename and optional variables and initiates a scan for duplicate code.
        /// </summary>
        /// <param name="filepath">the name of the directory to scan</param>
        /// <param name="verbose">(optinoal) the verbosity of the output. True = verbose, False = normal</param>
        public static List<DuplicateInstance> CheckForDuplicates(string filepath, ICodeComparer comparer, bool verbose = false)
        {
            if (verbose) Logger.Log("Beginning execution of CheckForDuplicates");

            if (string.IsNullOrWhiteSpace(filepath))
                throw new ArgumentNullException("Either a filename or a directory must be provided.");

            var results = new List<DuplicateInstance>();
            var cmcdResults = Run(filepath, comparer);

            foreach (var result in cmcdResults)
            {
                // Decide if we rule this a duplicate
                if (result.Score < 50)
                {
                    // Method A
                    results.Add(new DuplicateInstance(result.MethodA));

                    // Method B
                    results.Add(new DuplicateInstance(result.MethodB));
                }
            }

            if (verbose) Logger.Log("Finishing execution of CheckForDuplicates");

            return results;
        }

        /// <summary>
        /// Method to run the CMCD algorithm
        /// </summary>
        public static List<DuplicateResult> Run(string directoryPath, ICodeComparer comparer)
        {
            var comparisonResults = new List<DuplicateResult>();

            try
            {
                var allMethods = new List<Method>();
                var attr = File.GetAttributes(directoryPath);

                var files = attr.HasFlag(FileAttributes.Directory) ? Directory.GetFiles(directoryPath, "*.cs", SearchOption.AllDirectories).ToList() : new List<string>() { directoryPath };

                foreach (var file in files)
                {
                    var methods = GetMethods(file).Select(c =>
                        new Method()
                        {
                            FilePath = Path.GetFullPath(file),
                            FileName = Path.GetFileName(file),
                            MethodNode = c
                        });
                    allMethods.AddRange(methods);
                }

                for (int i = 0; i < allMethods.Count; i++)
                {
                    for (int j = i + 1; j < allMethods.Count; j++)
                    {
                        try
                        {
                            var methodA = allMethods[i];
                            var methodB = allMethods[j];
                            var compareResult = comparer.Compare(methodA, methodB);
                            comparisonResults.Add(new DuplicateResult()
                            {
                                MethodA = new MethodInfo(methodA),
                                MethodB = new MethodInfo(methodB),
                                Score = compareResult
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return comparisonResults;
        }

        /// <summary>
        /// Get all the methods in a .cs file
        /// </summary>
        /// <param name="filePath">The file path</param>
        /// <returns>List of methods</returns>
        internal static List<SyntaxNode> GetMethods(string filePath)
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
                    switch (syntaxNode.Kind())
                    {
                        case SyntaxKind.MethodDeclaration:
                            methods.Add(syntaxNode);
                            break;
                        case SyntaxKind.ClassDeclaration:
                            methods.AddRange(GetMethodsFromClassNode(syntaxNode));
                            break;
                        case SyntaxKind.NamespaceDeclaration:
                            methods.AddRange(GetMethodsFromNamespace(syntaxNode));
                            break;
                        default:
                            continue;
                    }
                }
            }

            return methods;
        }


        /// <summary>
        /// This method will return list of methods node defined in class by traversing the tree
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <returns></returns>
        public static IEnumerable<SyntaxNode> GetMethodsFromClassNode(SyntaxNode syntaxNode)
        {
            var methods = new List<SyntaxNode>();
            foreach (var node in syntaxNode.ChildNodes())
            {
                if (node.Kind() == SyntaxKind.MethodDeclaration)
                {
                    methods.Add(node);
                }
            }
            return methods;
        }

        /// <summary>
        /// This method will return list of methods node defined in namespace by traversing the tree
        /// </summary>
        /// <param name="syntaxNode"></param>
        /// <returns></returns>
        public static IEnumerable<SyntaxNode> GetMethodsFromNamespace(SyntaxNode syntaxNode)
        {
            var methods = new List<SyntaxNode>();
            foreach (var node in syntaxNode.ChildNodes())
            {
                if (node.Kind() == SyntaxKind.ClassDeclaration)
                {
                    methods.AddRange(GetMethodsFromClassNode(node));
                }
            }
            return methods;
        }
    }
}
