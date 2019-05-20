using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDuplicationChecker.Tests
{
    [TestClass()]
    public class CodeIteratorTests
    {
        [TestMethod()]
        public void CheckForDuplicates_Dir_Test()
        {
            // Arrange
            var filename = "../../SampleCode";

            // Act
            var results = CodeIterator.CheckForDuplicates(filename);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreNotEqual(0, results.Count);
        }

        [TestMethod()]
        public void CheckForDuplicates_File_Test()
        {
            // Arrange
            var filename = "../../SampleCode/RiddledWithDuplicates.cs";

            // Act
            var results = CodeIterator.CheckForDuplicates(filename);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreNotEqual(0, results.Count);
        }

        [TestMethod()]
        public void CheckDirForDuplicatesTest()
        {
            // Arrange
            var filepath = "../../SampleCode";
            var verbosity = true;

            // Act
            var results = CodeIterator.CheckDirForDuplicates(filepath, verbosity);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreNotEqual(0, results.Count);
        }

        [TestMethod()]
        public void CheckFileForDuplicatesTest()
        {
            // Arrange
            var filename = "../../SampleCode/RiddledWithDuplicates.cs";
            var verbosity = true;

            // Act
            var results = CodeIterator.CheckFileForDuplicates(filename, verbosity);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreNotEqual(0, results.Count);
        }
    }
}