using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;

namespace CountMatrixCloneDetection
{
    public class ASTMatch
    {

        /// <summary>
        /// API to compare two methods
        /// </summary>
        /// <param name="methodA">Method A</param>
        /// <param name="methodB">Methods B</param>
        /// <returns>Comparision report between these two methods</returns>
        public double Compare(Models.Method methodA, Models.Method methodB)
        {
            return Recurse(methodA.MethodNode, methodB.MethodNode) == false ? 1.0 : 0.0;
        }

        /// <summary>
        /// Recursive procedure to compare 2 nodes based on Syntax Node's kind.
        /// </summary>
        /// <param name="methodA">Method A</param>
        /// <param name="methodB">Methods B</param>
        /// <returns>Comparision report between these two methods</returns>
        private bool Recurse(SyntaxNode node1, SyntaxNode node2)
        {
            if (node1.Kind() != node2.Kind()) return false;
            if (node1.ChildNodes().Count() != node2.ChildNodes().Count())
                return false;
                
            foreach (var tuple in node1.ChildNodes().Zip(node2.ChildNodes(), Tuple.Create)) {
                if (!Recurse(tuple.Item1, tuple.Item2))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
