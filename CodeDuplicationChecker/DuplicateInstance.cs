namespace CodeDuplicationChecker
{
    /// <summary>
    /// An instance of duplicate code in a codebase.
    /// This includes the filename, starting and ending lines of the block
    /// of duplicate code, as well as the duplicate code itself.
    /// </summary>
    public class DuplicateInstance
    {
        /// <summary>
        /// The filename of the duplicate code
        /// </summary>
        public string Filename;

        /// <summary>
        /// The starting line of the block of duplicate code
        /// </summary>
        public int StartLine;

        /// <summary>
        /// The ending line of the block of duplicate code
        /// </summary>
        public int EndLine;

        /// <summary>
        /// The block of code which is duplicated / very similar
        /// to that of code in other places
        /// </summary>
        public string Code;

        /// <summary>
        /// The html version of the code to display (e.g. with highlights)
        /// </summary>
        public string CodeHtml;

        internal DuplicateInstance Copy()
        {
            return new DuplicateInstance()
            {
                Filename = Filename,
                StartLine = StartLine,
                EndLine = EndLine,
                Code = Code,
                CodeHtml = CodeHtml,
            };
        }
    }
}
