using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDuplicationChecker.UnitTests
{
    [TestClass()]
    public class CodeIteratorTests
    {
        [TestMethod()]
        public void CheckForDuplicates_Dir_Test()
        {
            // Arrange
            var filename = "../../../SampleCode/UnitTests/";

            // Act
            var results = CodeIterator.CheckForDuplicates(filename);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[0].Filename);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[1].Filename);
        }

        [TestMethod()]
        public void CheckForDuplicates_File_Test()
        {
            // Arrange
            var filename = "../../../SampleCode/UnitTests/RiddledWithDuplicates.cs";

            // Act
            var results = CodeIterator.CheckForDuplicates(filename);

            // Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[0].Filename);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[1].Filename);
        }
    }
}