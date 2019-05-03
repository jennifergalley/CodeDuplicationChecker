using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDuplicationChecker
{
    class Program
    {
        /// <summary>
        /// Parses the command line inputs and starts the check.
        /// Possible options:
        /// -f / --filename <string> : provides the filename to check for duplicates
        /// -p / --filepath <string> : specifies a directory of files to check for duplicates
        /// -r / --results <string> : specifies the directory for saving the results (output) file
        /// -i / --instances <int> : the minimum number of instances of duplication to look for
        /// -v / --verbose : indicates the program should produce verbose output
        /// </summary>
        /// <param name="args">the command-line arguments</param>
        static void Main(string[] args)
        {
            var numArgs = args.Count();
            var filename = string.Empty; // -f / --filename
            var filepath = string.Empty; // -p / --filepath
            var resultsPath = string.Empty; // -r / --results
            var instances = 0; // -i / --instances
            var verbose = false; // -v / --verbose
            var blockOfExecution = string.Empty; // the block of execution which threw an exception, if any

            try
            {
                // Parse the command-line arguments
                blockOfExecution = "parsing the command-line arguments";
                if (numArgs == 0)
                {
                    throw new ArgumentException("Either a filename or a directory must be provided");
                }

                for (int i = 0; i < numArgs; i++)
                {
                    var arg = args[i];
                    switch (arg)
                    {
                        case "-f":
                        case "--filename":
                            i++;
                            filename = args[i];
                            break;
                        case "-p":
                        case "--filepath":
                            i++;
                            filepath = args[i];
                            break;
                        case "-r":
                        case "--results":
                            i++;
                            resultsPath = args[i];
                            break;
                        case "-i":
                        case "--instances":
                            i++;
                            bool success = int.TryParse(args[i], out instances);
                            if (!success)
                            {
                                throw new ArgumentException("Invalid number provided for -i / --instances argument");
                            }
                            break;
                        case "-v":
                        case "--verbose":
                            verbose = true;
                            break;
                        case "-h":
                        case "--help":
                            PrintHelp();
                            return;
                        default:
                            throw new ArgumentException("Invalid arguments provided");
                    }
                }

                // Run the scan
                blockOfExecution = "parsing your file(s)";
                var results = CodeIterator.CheckForDuplicates(filename, filepath, instances, verbose);

                // Generate the results
                blockOfExecution = "generating the results file";
                results.GenerateResultsFile(verbose);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops! Something went wrong while {blockOfExecution}. Try using -h or --help for help.");

                if (verbose)
                {
                    Console.WriteLine("Error message:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Prints the help dialog to the console
        /// </summary>
        private static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Help dialog:");
            Console.WriteLine("Possible options:");
            Console.WriteLine("-f / --filename <string> : provides the filename to check for duplicates");
            Console.WriteLine("-p / --filepath <string> : specifies a directory of files to check for duplicates");
            Console.WriteLine("-i / --instances <int> : the minimum number of instances of duplication to look for");
            Console.WriteLine("-v / --verbose : indicates the program should produce verbose output");
        }
    }
}
