﻿using CountMatrixCloneDetection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDuplicationChecker
{
    public class CodeIterator
    {
        /// <summary>
        /// Takes either a file directory or a filename and optional variables and initiates a scan for duplicate code.
        /// </summary>
        /// <param name="filename">the name of the file to scan</param>
        /// <param name="dir">the name of the directory to scan</param>
        /// <param name="instances">(optional) the minimum number of duplicate instances to check for</param>
        /// <param name="verbose">(optinoal) the verbosity of the output. True = verbose, False = normal</param>
        public static DuplicationResults CheckForDuplicates(string filename = "", string dir = "", int instances = 0, bool verbose = false)
        {
            if (verbose)
            {
                Console.WriteLine("Beginning execution of CheckForDuplicates");
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(dir))
                {
                    return CheckDirForDuplicates(dir, instances, verbose);
                }
                else if (!string.IsNullOrWhiteSpace(filename))
                {
                    return CheckFileForDuplicates(filename, instances, verbose);
                }
                else
                {
                    throw new ArgumentException("Either a filename or a directory must be provided.");
                }
            }
            finally
            {
                if (verbose)
                {
                    Console.WriteLine("Finishing execution of CheckForDuplicates");
                }
            }
        }

        /// <summary>
        /// Iterates through a file, looking for duplicate code within the file
        /// </summary>
        /// <param name="filename">the name of the file</param>
        /// <param name="instances">the minimum number of duplicate instances to check for</param>
        /// <param name="verbose">the verbosity of the output. True = verbose, False = normal</param>
        private static DuplicationResults CheckFileForDuplicates(string filename, int instances, bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("Beginning execution of CheckFileForDuplicates");
            }
            var results = new DuplicationResults();

            var cmcdResults = CMCD.Run(filename);

            if (verbose)
            {
                Console.WriteLine("Finishing execution of CheckFileForDuplicates");
            }
            return results;
        }

        /// <summary>
        /// Iterates through a directory, looking for duplicate code within the directory
        /// </summary>
        /// <param name="dir">the path of the directory</param>
        /// <param name="instances">the minimum number of duplicate instances to check for</param>
        /// <param name="verbose">the verbosity of the output. True = verbose, False = normal</param>
        private static DuplicationResults CheckDirForDuplicates(string dir, int instances, bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("Beginning execution of CheckDirForDuplicates");
            }

            var results = new DuplicationResults();
            var cmcdResults = CMCD.Run(dir);

            //
            // TO BE WRITTEN
            //
            // Loop through files in dir
            // Call CheckFileForDuplicates
            // Compare files with each other
            //

            if (verbose)
            {
                Console.WriteLine("Finishing execution of CheckDirForDuplicates");
            }
            return results;
        }
    }
}
