using System.IO;

namespace CountMatrixCloneDetection
{
    /// <summary>
    /// class to represent CMDC method info
    /// </summary>
    public class CMCDMethodInfo
    {
        public CMCDMethodInfo(CMCDMethod method)
        {
            FileName = method.FileName;
            FilePath = Path.GetFullPath(method.FilePath);
            MethodText = method.MethodNode.GetText().ToString();
            EndLineNumber = method.MethodNode.FullSpan.End;
            StartLineNumber = method.MethodNode.FullSpan.Start;
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
        /// Gets or sets the start line number
        /// </summary>
        public int StartLineNumber { get; set; }

        /// <summary>
        /// Gets or sets the end line number
        /// </summary>
        public int EndLineNumber { get; set; }
    }
}
