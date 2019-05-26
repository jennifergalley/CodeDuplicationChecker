using System;
using System.Linq;

namespace CodeDuplicationChecker
{
    public class Program
    {
        /// <summary>
        /// Parses the command line inputs and starts the check.
        /// Possible options:
        /// -f / --filepath <string> : specifies a directory of files to check for duplicates
        /// -v / --verbose : indicates the program should produce verbose output
        /// -h / --help : prints the help dialog
        /// </summary>
        /// <param name="args">the command-line arguments</param>
        public static int Main(string[] args)
        {
            var numArgs = args.Count();
            var filepath = string.Empty; // -f / --filepath
            var verbose = false; // -v / --verbose
            var blockOfExecution = string.Empty; // the block of execution which threw an exception, if any

            try
            {
                // Parse the command-line arguments
                blockOfExecution = "parsing the command-line arguments";
                if (numArgs == 0)
                {
                    throw new ArgumentException("Either a filename or a directory must be provided.");
                }

                for (int i = 0; i < numArgs; i++)
                {
                    var arg = args[i];
                    switch (arg)
                    {
                        case "-f":
                        case "--filepath":
                            i++;
                            filepath = args[i];
                            break;
                        case "-v":
                        case "--verbose":
                            verbose = true;
                            break;
                        case "-h":
                        case "--help":
                            PrintHelp();
                            return 1;
                        default:
                            throw new ArgumentException("Invalid arguments provided.");
                    }
                }

                if (string.IsNullOrWhiteSpace(filepath))
                {
                    throw new ArgumentException("Either a filename or a directory must be provided.");
                }

                // Run the scan
                blockOfExecution = "parsing your file(s)";
                var results = CodeIterator.CheckForDuplicates(filepath, verbose);

                // Generate the results
                blockOfExecution = "generating the results file";
                VisualizeDiffs.TryGenerateResultsFile(results, out var resultsFilePath, verbose);

                return 1;
            }
            catch (Exception e)
            {
                Logger.Log($"Oops! Something went wrong while {blockOfExecution}. Try using -h or --help for help.");

                if (verbose)
                {
                    Logger.Log("Error message:");
                    Logger.Log(e.Message);
                }

                return -1;
            }
        }

        /// <summary>
        /// Prints the help dialog to the console
        /// </summary>
        private static void PrintHelp()
        {
            Logger.Log(string.Empty);
            Logger.Log("Help dialog:");
            Logger.Log("Possible options:");
            Logger.Log("-f / --filepath <string> [required] : specifies a filename or directory of files to check for duplicates");
            Logger.Log("-v / --verbose : indicates the program should produce verbose output");
            Logger.Log("-h / --help : prints this help dialog");
        }
    }
}
