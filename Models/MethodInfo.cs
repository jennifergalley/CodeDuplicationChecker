using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace Models
{
    /// <summary>
    /// class to represent CMDC method info
    /// </summary>
    public class MethodInfo
    {
        public MethodInfo(Method method)
        {
            FileName = method.FileName;
            FilePath = Path.GetFullPath(method.FilePath);
            MethodText = method.MethodNode.GetText().ToString();
            EndLineNumber = method.MethodNode.FullSpan.End;
            StartLineNumber = method.MethodNode.FullSpan.Start;
            MethodName = (method.MethodNode as MethodDeclarationSyntax)?.Identifier.ValueText;
        }

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
        /// Gets or sets the method name
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the start line number
        /// </summary>
        public int StartLineNumber { get; set; }

        /// <summary>
        /// Gets or sets the end line number
        /// </summary>
        public int EndLineNumber { get; set; }
    }
}
