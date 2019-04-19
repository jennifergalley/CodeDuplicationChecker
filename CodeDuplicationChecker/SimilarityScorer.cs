using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDuplicationChecker
{
    public class SimilarityScorer
    {
        /// <summary>
        /// Gets the similarity score between two strings, with 0 being not alike at all
        /// and 1 being exactly the same.
        /// </summary>
        /// <param name="compare1">the first string to compare</param>
        /// <param name="compare2">the second string to compare</param>
        /// <returns>a similarity score between 0 and 1</returns>
        public static double GetSimilarityScore(string compare1, string compare2)
        {
            // If either string is null or whitespace, return 0 because we shouldn't find duplicate whitespace
            if (string.IsNullOrWhiteSpace(compare1) || string.IsNullOrWhiteSpace(compare2))
            {
                return 0;
            }

            // If the strings are exactly the same, return 1
            if (compare1.Equals(compare2))
            {
                return 1.0;
            }

            //
            // TO BE WRITTEN
            //

            return 0;
        }
    }
}
