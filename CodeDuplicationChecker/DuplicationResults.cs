using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeDuplicationChecker
{
    /// <summary>
    /// A collection of duplicate code within a file / codebase, 
    /// and the ultimate result of this program.
    /// There may be many instances of duplicate code, or none.
    /// </summary>
    public class DuplicationResults
    {
        /// <summary>
        /// A list of blocks of duplicate code
        /// </summary>
        public List<DuplicateCode> CodeDuplicates;

        /// <summary>
        /// The directory in which to save the results file
        /// </summary>
        public readonly string Filepath = "../../../Results/";

        /// <summary>
        /// The name of the results file itself
        /// </summary>
        public readonly string Filename = "results.html";

        /// <summary>
        /// Generates an HTML (or other) results file for viewing the list of CodeDuplicates.
        /// </summary>
        /// <param name="verbose">the verbosity of the logging output. True = verbose, False = normal</param>
        /// <returns>True on success, false on failure</returns>
        public bool GenerateResultsFile(bool verbose = false)
        {
            if (verbose) Console.WriteLine("Beginning execution of GenerateResultsFile");

            //
            // TO BE WRITTEN
            //
            // Reads the CodeDuplicates object
            // Creates an HTML visualization
            // Returns false if it fails for some reason


            // Create the "Results" folder if it does not exist
            Console.WriteLine("Creating Results directory");
            Directory.CreateDirectory(Filepath);

            // Write the file
            Console.WriteLine("Writing the results file");
            using (FileStream fs = new FileStream(Filepath + Filename, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    w.WriteLine("<H1>NEW</H1>");
                }
            }

            if (verbose) Console.WriteLine("Finishing execution of GenerateResultsFile");
            return true;
        }
    }

    /// <summary>
    /// A collection of Instances of duplicate code. All of these
    /// instances will have code that is very similar to each other
    /// but they may not be exactly the same.
    /// </summary>
    public class DuplicateCode
    {
        /// <summary>
        /// A list of instances of the same code throughout a file or files
        /// </summary>
        public List<DuplicateInstance> Instances;
    }

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
    }
}