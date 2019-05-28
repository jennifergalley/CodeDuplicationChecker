﻿using Interfaces;
using Models;

namespace NaiveStringComparer
{
    public class NaiveStringComparer : ICodeComparer
    {
        /// <summary>
        /// Gets the similarity score between two strings, with double.MaxValue being not alike at all
        /// and 0 being exactly the same.
        /// </summary>
        /// <param name="compare1">the first string to compare</param>
        /// <param name="compare2">the second string to compare</param>
        /// <returns>0 if exactly the same, or double.MaxValue otherwise</returns>
        public double Compare(string compare1, string compare2)
        {
            // If either string is null or whitespace, return double.MaxValue because we shouldn't find duplicate whitespace
            if (string.IsNullOrWhiteSpace(compare1) || string.IsNullOrWhiteSpace(compare2))
            {
                return double.MaxValue;
            }

            // If the strings are exactly the same, return 0
            if (compare1.Equals(compare2))
            {
                return 0;
            }

            return double.MaxValue;
        }

        public double Compare(Method methodA, Method methodB)
        {
            var compare1 = methodA.MethodNode.GetText().ToString();
            var compare2 = methodB.MethodNode.GetText().ToString();

            return Compare(compare1, compare2);
        }
    }
}
