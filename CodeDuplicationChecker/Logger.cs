using System;

namespace CodeDuplicationChecker
{
    public static class Logger
    {
        static internal string log = string.Empty;

        public static void Log(string line)
        {
            log += line + " ";
            Console.WriteLine(line);
        }

        public static void Clear()
        {
            log = string.Empty;
        }
    }
}
