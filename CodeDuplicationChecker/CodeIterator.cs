using CountMatrixCloneDetection;
using System;
using System.Collections.Generic;

namespace CodeDuplicationChecker
{
    public class CodeIterator
    {
        /// <summary>
        /// Takes either a file directory or a filename and optional variables and initiates a scan for duplicate code.
        /// </summary>
        /// <param name="filepath">the name of the directory to scan</param>
        /// <param name="verbose">(optinoal) the verbosity of the output. True = verbose, False = normal</param>
        public static List<DuplicateInstance> CheckForDuplicates(string filepath = "", bool verbose = false)
        {
            if (verbose) Logger.Log("Beginning execution of CheckForDuplicates");

            if (string.IsNullOrWhiteSpace(filepath))
                throw new ArgumentNullException("Either a filename or a directory must be provided.");

            var results = new List<DuplicateInstance>();
            var cmcdResults = CMCD.Run(filepath);

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
    }
}
