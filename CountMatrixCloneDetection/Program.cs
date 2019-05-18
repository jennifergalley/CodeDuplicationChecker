using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Dedup;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CountMatrixCloneDetection
{
    class Program
    {
        public static void Main(string[] args)
        {
            var cmcdResults = CMCD.Run(@"..\\..\\..\\");
            return;
        }
    }
}
