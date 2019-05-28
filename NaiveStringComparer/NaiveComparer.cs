using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NaiveStringComparer
{
    public class NaiveStringComparerHelper
    {
        static List<string> KnownTypes = new List<string> { "int", "string", "char", "double", "float" };
        static Dictionary<string, string> MatchDictionary = new Dictionary<string, string>();
        static Dictionary<string, int> MatchCount = new Dictionary<string, int>();

        public static string GetFormattedString(string method)
        {
            List<string> testLines = method.Replace("\r\n", string.Empty).Split(';').ToList();
            foreach (var line in testLines)
            {                
                Pass0_ParseLineInitVariables(" " + line + ";");
            }

            return Pass1_ParseLinesReplaceVariables(method);

            ////foreach (var match in MatchDictionary)
            ////{
            ////    Console.WriteLine(match.Key + ", " + match.Value + "Count: " + MatchCount[match.Value]);
            ////}
        }

        static void Pass0_ParseLineInitVariables(string line)
        {
            foreach (var type in KnownTypes)
            {
                Regex InitMatch = new Regex("[ ]+(" + type + ")[ ]+[\\w] +[\\w\\d] *[ ] *[=]");
                if (InitMatch.IsMatch(line))
                {
                    Console.WriteLine("Init Match with:  " + line);
                    var match = InitMatch.Match(line);
                    var index = match.ToString().IndexOf(type) + type.Length;
                    int varLength = match.ToString().IndexOf("=") - index;
                    MatchCount.Add("temp" + MatchDictionary.Count, 0);
                    MatchDictionary.Add(line.Substring(index, varLength).Trim(), "temp" + MatchDictionary.Count);
                    return;
                }

                Regex DefineMatch = new Regex("[ ]+(" + type + ")[ ]+[\\w] +[\\w\\d] *[ ] *[,;]");
                if (DefineMatch.IsMatch(line))
                {
                    Console.WriteLine("Define Match with: " + line);
                    var match = DefineMatch.Match(line);
                    var index = match.ToString().IndexOf(type) + type.Length;
                    int varLength = match.ToString().IndexOf(";") - index;
                    MatchCount.Add("temp" + MatchDictionary.Count, 0);

                    MatchDictionary.Add(match.ToString().Substring(index,
                    varLength).Trim(), "temp" + MatchDictionary.Count);
                    break;
                }
            }
        }

        static string Pass1_ParseLinesReplaceVariables(string str)
        {
            string replacementString = str;
            foreach (var variableMapping in MatchDictionary)
            {
                Regex r = new Regex("[ \t]+(" + variableMapping.Key + ")[ \t]+");
                int count = r.Matches(replacementString).Count;
                replacementString = r.Replace(replacementString, " " + variableMapping.Value + " ");
                MatchCount[variableMapping.Value] += count;

                Regex r1 = new Regex("[ \t]+(" + variableMapping.Key + ")[,]+");
                count = r1.Matches(replacementString).Count;

                replacementString = r1.Replace(replacementString, " " + variableMapping.Value + ",");
                MatchCount[variableMapping.Value] += count;

                Regex r2 = new Regex("[ \t]+(" + variableMapping.Key + ")[;]+");
                count = r2.Matches(replacementString).Count;

                replacementString = r2.Replace(replacementString, " " + variableMapping.Value + ";");
                MatchCount[variableMapping.Value] += count;
            }

            return replacementString;
        }

        ////static string TestParseText()
        ////{
        ////    string str = "int initvalue = 10;\r\n initvalue = initvalue + 20;\r\n int junior; ";
        ////    string str1 = "int j;";
        ////    return str;
        ////}

    }
}