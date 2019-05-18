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
    public class DuplicationResults
    {
        /// <summary>
        /// A list of blocks of duplicate code
        /// </summary>
        public List<DuplicateCode> CodeDuplicates;

        /// <summary>
        /// The directory in which to save the results file
        /// </summary>
        private readonly string Filepath = "../../../Results/";

        /// <summary>
        /// The name of the results file itself
        /// </summary>
        private readonly string Filename = "results.html";

        /// <summary>
        /// Generates an HTML (or other) results file for viewing the list of CodeDuplicates.
        /// </summary>
        /// <param name="verbose">the verbosity of the logging output. True = verbose, False = normal</param>
        /// <returns>True on success, false on failure</returns>
        public bool GenerateResultsFile(bool verbose = false)
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
                    foreach (var dup in CodeDuplicates)
                    {
                        dup.GenerateCodeHTML();

                        foreach (var clone in dup.Instances)
                        {
                            w.WriteLine($"<h1>File: {clone.Filename}</h1>");
                            w.WriteLine($"<h2>Start: {clone.StartLine}</h2>");
                            w.WriteLine($"<h2>End: {clone.EndLine}</h2>");
                            w.WriteLine($"<pre>{clone.CodeHtml}</pre>");
                        }

                        w.WriteLine($"<hr>");
                    }

                    // Write the CSS!
                    w.WriteLine(@"
                    <style>
                        span.diff {
                            background-color: yellow;
                        }
                        /*span.same {
                            background-color: greenyellow;
                        }*/
                    </style>");
                }
            }

            if (verbose) Console.WriteLine("Finishing execution of GenerateResultsFile");
            return true;
        }


        //internal static List<string> SplitCodePunctuation(string code)
        //{
        //    string pattern = @"^(\s+|\d+|\w+|[^\d\s\w])+$";

        //    Regex regex = new Regex(pattern);
        //    var results = new List<string>();
        //    if (regex.IsMatch(code))
        //    {
        //        Match match = regex.Match(code);

        //        foreach (Capture capture in match.Groups[1].Captures)
        //        {
        //            if (!string.IsNullOrWhiteSpace(capture.Value))
        //            {
        //                results.Add(capture.Value);
        //            }
        //        }
        //    }

        //    return results;
        //}

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

        /// <summary>
        /// A "master" copy of the cloned code, comprised of the most common denominators.
        /// </summary>
        private string master = string.Empty;

        internal DuplicateCode Copy()
        {
            var copy = new DuplicateCode()
            {
                Instances = new List<DuplicateInstance>()
            };

            foreach (var instance in Instances)
            {
                copy.Instances.Add(instance.Copy());
            }

            return copy;
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
        /// 
        /// </summary>
        /// <param name="dup"></param>
        public void GenerateCodeHTML()
        {
            // Find the set of differences between the code snippets
            var diffs = new List<string>();
            var compare1 = SplitCodeNewlines(Instances[0].Code);
            for (int i = 1; i < Instances.Count; i++)
            {
                var compare2 = SplitCodeNewlines(Instances[i].Code);
                diffs = diffs.Union(compare1.Except(compare2).Union(compare2.Except(compare1))).ToList();
            }

            // For each block of code
            for (int i = 0; i < Instances.Count; i++)
            {
                var splitByLine = SplitCodeNewlines(Instances[i].Code.Trim());
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

                Instances[i].CodeHtml = html.ToString();
            }
        }

        /// <summary>
        /// Gets or creates a master copy of the duplicated code
        /// </summary>
        /// <returns>the master copy of the duplicated code</returns>
        public string Master
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(master))
                {
                    return master;
                }

                // For each instance of duplicated code, split the code into lines
                // and assign each line a corresponding vote
                var lineMap = BuildLineMap();

                string winningLine;
                var masterCode = new StringBuilder();
                for (int i = 0; i < lineMap.Count; i++)
                {
                    // Get the winning line, or see if we should stop
                    if (GetWinningLine(lineMap[i], out winningLine))
                    {
                        // If we have already finished, stop
                        break;
                    }

                    // Else, add the winning line to master
                    masterCode.AppendLine(winningLine);
                }

                master = masterCode.ToString().TrimEnd();
                return master;
            }
        }

        /// <summary>
        /// Build a mapping from index to line of code and number of votes per line
        /// </summary>
        /// <returns>a mapping from index to line of code and number of votes per line</returns>
        private Dictionary<int, Dictionary<string, int>> BuildLineMap()
        {
            var lineMap = new Dictionary<int, Dictionary<string, int>>();

            foreach (var instance in Instances)
            {
                var lines = SplitCodeNewlines(instance.Code);

                int j = 0;
                for (int i = 0; i < lines.Count; i++)
                {
                    var line = lines[i];

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    if (lineMap.TryGetValue(j, out var lineVotes))
                    {
                        if (lineVotes.ContainsKey(line))
                        {
                            lineVotes[line] += 1;
                        }
                        else
                        {
                            lineVotes.Add(line, 1);
                        }

                        lineMap[j] = lineVotes;
                    }
                    else
                    {
                        lineMap.Add(j, new Dictionary<string, int>() { { line, 1 } });
                    }

                    j++;
                }
            }

            return lineMap;
        }

        /// <summary>
        /// Do magic
        /// </summary>
        /// <param name="lineVotes"></param>
        /// <param name="winningLine"></param>
        /// <returns></returns>
        private bool GetWinningLine(Dictionary<string, int> lineVotes, out string winningLine)
        {
            int max = 0;
            int sum = 0;
            winningLine = string.Empty;

            // Get the line with the maximum votes
            foreach (var pair in lineVotes)
            {
                var line = pair.Key;
                var votes = pair.Value;
                sum += votes;

                if (votes > max)
                {
                    max = votes;
                    winningLine = line;
                }
            }

            // Determine stopping condition - big open question
            if (sum <= (Instances.Count / 2))
            {
                return true;
            }

            return false;
        }
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