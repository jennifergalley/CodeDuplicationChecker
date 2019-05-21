using Microsoft.CodeAnalysis;

namespace CountMatrixCloneDetection
{
    /// <summary>
    /// Class to represent CMDC 
    /// </summary>
    public class CMCDMethod
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
