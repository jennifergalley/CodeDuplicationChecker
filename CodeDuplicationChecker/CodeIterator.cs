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
        /// Iterates through a file, looking for duplicate code within the file
        /// </summary>
        /// <param name="filepath">the path of the file</param>
        public static DuplicationResults CheckFileForDuplicates(string filepath)
        {
            var file = File.ReadAllText(filepath);
            var results = new DuplicationResults();

            //
            // TO BE WRITTEN
            //
            // Loop through file contents somehow
            // Build result object
            //

            return results;
        }
    }
}
