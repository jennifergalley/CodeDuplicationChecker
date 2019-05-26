//
// This file is just a sample for testing
//

using System;

namespace CodeDuplicationCheckerTests.SampleCode
{
    public class RiddledWithDuplicates
    {
        GlobalLogger logger = new GlobalLogger();

        public void StupidFunction()
        {
            var i = DateTime.Now.Minute;
            if (i / 2 == 0)
            {
                var timeOfDay = DateTime.UtcNow.TimeOfDay;
                var timeSpan = timeOfDay.Add(new TimeSpan(7, 7, 7));
                this.logger.Log($"The time of day is {timeOfDay} and when you add 7, 7, 7, it is {timeSpan}");
            }
            else
            {
                var timeOfDay = DateTime.UtcNow.TimeOfDay;
                var timeSpan = timeOfDay.Add(new TimeSpan(7, 7, 7));
                this.logger.Log($"The time of day is {timeOfDay} and when you add 7, 7, 7, it is {timeSpan}");
            }

            // more bad code
            string truth;
            int minute;
            switch (DateTime.Now.Hour)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    truth = "this is a stupid function";
                    minute = DateTime.Now.Minute;
                    this.logger.Log($"Let's log something stupid! Here's some text: {truth}, and here's a number: {minute}");
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    truth = "this is a stupid function";
                    minute = DateTime.Now.Minute;
                    this.logger.Log($"Let's log something stupid! Here's some text: {truth}, and here's a number: {minute}");
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                    truth = "this is a stupid function";
                    minute = DateTime.Now.Minute;
                    this.logger.Log($"Let's log something stupid! Here's some text: {truth}, and here's a number: {minute}");
                    break;
            }
        }

        public void StupidFunctionDuplicate()
        {
            var i = DateTime.Now.Minute;
            if (i / 3 == 0)
            {
                var timeOfDay = DateTime.UtcNow.TimeOfDay;
                var timeSpan = timeOfDay.Add(new TimeSpan(9, 9, 9));
                this.logger.Log($"The time of day is {timeOfDay} and when you add 9, 9, 9, it is {timeSpan}");
            }
            else
            {
                var timeOfDay = DateTime.UtcNow.TimeOfDay;
                var timeSpan = timeOfDay.Add(new TimeSpan(9, 9, 9));
                this.logger.Log($"The time of day is {timeOfDay} and when you add 9, 9, 9, it is {timeSpan}");
            }

            string truth;
            int minute;
            switch (DateTime.Now.Hour)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    // Added comment
                    truth = "this is a stupid function too!";
                    minute = DateTime.Now.Minute;
                    this.logger.Log($"Let's log something stupid! Here's some text: {truth}, and here's a number: {minute}");
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    // Added comment
                    truth = "this is a stupid function too!";
                    minute = DateTime.Now.Minute;
                    this.logger.Log($"Let's log something stupid! Here's some text: {truth}, and here's a number: {minute}");
                    break;
                case 9:
                case 10:
                case 11:
                case 12:
                    // Added comment
                    truth = "this is a stupid function too!";
                    minute = DateTime.Now.Minute;
                    this.logger.Log($"Let's log something stupid! Here's some text: {truth}, and here's a number: {minute}");
                    break;
            }
        }
    }

    internal class GlobalLogger
    {
        public GlobalLogger()
        {
        }

        public void Log(string logMsg)
        {
        }
    }
}
