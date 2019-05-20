namespace CountMatrixCloneDetection
{
    /// <summary>
    /// class to represent CMDC method info
    /// </summary>
    public class CMCDMethodInfo
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
}
