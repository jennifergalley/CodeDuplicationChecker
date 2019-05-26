using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDuplicationChecker.IntegrationTests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void Main_EmptyArgs_Test()
        {
            // Arrange
            string[] args = new string[] { };

            // Act
            var result = Program.Main(args);

            // Assert
            Assert.AreEqual(-1, result);
            Assert.IsTrue(Logger.log.Contains("Oops! Something went wrong while parsing the command-line arguments. Try using -h or --help for help."),
                $"Log was {Logger.log}");
        }

        [TestMethod()]
        public void Main_Verbose_Test()
        {
            // Arrange
            string[] args = new string[] { "-v" };

            // Act
            var result = Program.Main(args);

            // Assert
            Assert.AreEqual(-1, result);
            Assert.IsTrue(Logger.log.Contains("Oops! Something went wrong while parsing the command-line arguments. Try using -h or --help for help."),
                $"Log was {Logger.log}");
            Assert.IsTrue(Logger.log.Contains("Error message: Either a filename or a directory must be provided."),
                $"Log was {Logger.log}");
        }

        [TestMethod()]
        public void Main_BothArgs_Test()
        {
            // Arrange
            string[] args = new string[] { "-v", "-f", "../../../SampleCode/UnitTests" };

            // Act
            var result = Program.Main(args);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod()]
        public void Main_Help_Test()
        {
            // Arrange
            string[] args = new string[] { "-h" };

            // Act
            var result = Program.Main(args);

            // Assert
            Assert.AreEqual(1, result);
            Assert.IsTrue(Logger.log.Contains("Help dialog:"));
        }

        [TestMethod()]
        public void Main_Help2_Test()
        {
            // Arrange
            string[] args = new string[] { "--help" };

            // Act
            var result = Program.Main(args);

            // Assert
            Assert.AreEqual(1, result);
            Assert.IsTrue(Logger.log.Contains("Help dialog:"));
        }
    }
}