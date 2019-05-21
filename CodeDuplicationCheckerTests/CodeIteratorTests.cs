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
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[0].Filename);
            Assert.AreEqual(13, results[0].StartLine);
            Assert.AreEqual(59, results[0].EndLine);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[1].Filename);
            Assert.AreEqual(61, results[1].StartLine);
            Assert.AreEqual(109, results[1].EndLine);
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
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[0].Filename);
            Assert.AreEqual(13, results[0].StartLine);
            Assert.AreEqual(59, results[0].EndLine);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[1].Filename);
            Assert.AreEqual(61, results[1].StartLine);
            Assert.AreEqual(109, results[1].EndLine);
        }
    }
}