using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeDuplicationChecker
{
    /// <summary>
    /// A collection of duplicate code within a file / codebase, 
    /// and the ultimate result of this program.
    /// There may be many instances of duplicate code, or none.
    /// </summary>
    public static class VisualizeDiffs
    {
        /// <summary>
        /// The directory in which to save the results file
        /// </summary>
        private static string Filepath = "../../../Results/";

        /// <summary>
        /// The name of the results file itself
        /// </summary>
        private static string Filename = "results.html";

        /// <summary>
        /// Generates an HTML (or other) results file for viewing the list of CodeDuplicates.
        /// </summary>
        /// <param name="codeDuplicates">the list of blocks of duplicate code</param>
        /// <param name="verbose">the verbosity of the logging output. True = verbose, False = normal</param>
        /// <returns>True on success, false on failure</returns>
        public static bool GenerateResultsFile(List<DuplicateInstance> codeDuplicates, bool verbose = false)
        {
            if (verbose) Console.WriteLine("Beginning execution of GenerateResultsFile");

            // Create the "Results" folder if it does not exist
            Console.WriteLine("Creating Results directory");
            Directory.CreateDirectory(Filepath);

            // Write the file
            Console.WriteLine("Writing the results file");
            using (FileStream fs = new FileStream(Filepath + Filename, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    GenerateCodeHTML(codeDuplicates);

                    foreach (var clone in codeDuplicates)
                    {
                        w.WriteLine($"<h1>File: {clone.Filename}</h1>");
                        w.WriteLine($"<h2>Start: {clone.StartLine}</h2>");
                        w.WriteLine($"<h2>End: {clone.EndLine}</h2>");
                        w.WriteLine($"<pre>{clone.CodeHtml}</pre>");
                    }

                    w.WriteLine($"<hr>");

                    // Write the CSS!
                    w.WriteLine(@"
                    <style>
                        span.diff {
                            background-color: red;
                        }
                        /*span.same {
                            background-color: greenyellow;
                        }*/
                    </style>");
                }

                if (verbose) Console.WriteLine("Finishing execution of GenerateResultsFile");
                return true;
            }
        }

        /// <summary>
        /// Splits a string on newlines and returns a list of lines as strings
        /// </summary>
        /// <param name="code">the code to split</param>
        /// <returns>a list of lines as strings</returns>
        public static List<string> SplitCodeNewlines(string code)
        {
            return code.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ).ToList();
        }

        /// <summary>
        /// Generates the html for diff of code instances
        /// </summary>
        /// <param name="codeDuplicates">the instances of duplicate code</param>
        public static void GenerateCodeHTML(List<DuplicateInstance> codeDuplicates)
        {
            // Find the set of differences between the code snippets
            var diffs = new List<string>();
            var compare1 = SplitCodeNewlines(codeDuplicates[0].Code);
            for (int i = 1; i < codeDuplicates.Count; i++)
            {
                var compare2 = SplitCodeNewlines(codeDuplicates[i].Code);
                diffs = diffs.Union(compare1.Except(compare2).Union(compare2.Except(compare1))).ToList();
            }

            // For each block of code
            for (int i = 0; i < codeDuplicates.Count; i++)
            {
                var splitByLine = SplitCodeNewlines(codeDuplicates[i].Code.Trim());
                var html = new StringBuilder();

                // Highlight the lines with a diff using the split by newline
                bool foundDiff;
                foreach (var line in splitByLine)
                {
                    foundDiff = false;

                    foreach (var diff in diffs)
                    {
                        if (line.Contains(diff))
                        {
                            html.AppendLine(string.Concat("<span class='diff'>", System.Security.SecurityElement.Escape(line).TrimEnd(), "</span>"));
                            foundDiff = true;
                            break;
                        }
                    }

                    if (!foundDiff)
                    {
                        html.AppendLine(string.Concat("<span class='same'>", System.Security.SecurityElement.Escape(line).TrimEnd(), "</span>"));
                    }
                }

                codeDuplicates[i].CodeHtml = html.ToString();
            }
        }
    }
}