using Models;
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
        private static readonly string Filepath = "../../../Results/";

        /// <summary>
        /// The name of the results file itself
        /// </summary>
        private static readonly string Filename = "results.html";

        /// <summary>
        /// Generates an HTML (or other) results file for viewing the list of CodeDuplicates.
        /// </summary>
        /// <param name="codeDuplicates">the list of blocks of duplicate code</param>
        /// <param name="verbose">the verbosity of the logging output. True = verbose, False = normal</param>
        /// <returns>True on success, false on failure</returns>
        public static bool TryGenerateResultsFile(List<DuplicateInstance> codeDuplicates, out string resultsfilePath, bool verbose = false)
        {
            if (verbose) Logger.Log("Beginning execution of GenerateResultsFile");

            // Create the "Results" folder if it does not exist
            Logger.Log("Creating Results directory");
            Directory.CreateDirectory(Filepath);

            // Write the file
            Logger.Log("Writing the results file");
            resultsfilePath = Path.GetFullPath(Filepath + Filename);
            using (FileStream fs = new FileStream(resultsfilePath, FileMode.Create))
            {
                using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                {
                    GenerateCodeHTML(codeDuplicates, verbose);

                    foreach (var clone in codeDuplicates)
                    {
                        w.WriteLine("<div class='float'>");
                        w.WriteLine($"<span class='filename'><b>File:</b> {clone.Filename}</span>");
                        w.WriteLine("<br />");
                        w.WriteLine($"<span class='line'><b>Start:</b> {clone.StartLine}</span>");
                        w.WriteLine("<br />");
                        w.WriteLine($"<span class='line'><b>End:</b> {clone.EndLine}</span>");
                        w.WriteLine("<br />");
                        w.WriteLine($"<pre>{clone.CodeHtml}</pre>");
                        w.WriteLine("</div>");
                    }

                    // Write the CSS!
                    w.WriteLine(@"
<style>
    span.diff {
        background-color: yellow;
    }
    span.line 
    {
        font-size: 24px;
    }
    span.filename 
    {
        font-size: 36px;
    }
    div.float
    {
        float: left;
        padding-left: 10px;
    }
</style>");
                }

                if (verbose) Logger.Log("Finishing execution of GenerateResultsFile");
                return true;
            }
        }

        /// <summary>
        /// Splits a string on newlines and returns a list of lines as strings
        /// </summary>
        /// <param name="code">the code to split</param>
        /// <returns>a list of lines as strings</returns>
        internal static List<string> SplitCodeNewlines(string code)
        {
            code = code.Trim();
            return code.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
            ).ToList();
        }

        /// <summary>
        /// Generates the html for diff of code instances
        /// </summary>
        /// <param name="codeDuplicates">the instances of duplicate code</param>
        internal static void GenerateCodeHTML(List<DuplicateInstance> codeDuplicates, bool verbose = false)
        {
            if (verbose) Logger.Log("Beginning execution of GenerateCodeHTML");

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

            if (verbose) Logger.Log("Finishing execution of GenerateCodeHTML");
        }
    }
}