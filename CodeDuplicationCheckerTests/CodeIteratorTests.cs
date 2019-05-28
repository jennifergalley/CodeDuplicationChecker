using CountMatrixCloneDetection;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDuplicationChecker.UnitTests
{
    [TestClass()]
    public class CodeIteratorTests
    {
        ICodeComparer comparer = new CMCD();

        [TestMethod()]
        public void CheckForDuplicates_Dir_Test()
        {
            // Arrange
            var filename = "../../../SampleCode/UnitTests/";

            // Act
            var results = CodeIterator.CheckForDuplicates(filename, comparer);

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod()]
        public void CheckForDuplicates_File_Test()
        {
            // Arrange
            var filename = "../../../SampleCode/UnitTests/RiddledWithDuplicates.cs";

            // Act
            var results = CodeIterator.CheckForDuplicates(filename, comparer);

            // Assert
            Assert.IsNotNull(results);
        }

        [TestMethod]
        public void GetMethodsTest()
        {
            // Arrange
            var filepath = "../../../SampleCode/UnitTests/RiddledWithDuplicates.cs";

            // Act
            var result = CodeIterator.GetMethods(filepath);

            // Assert
            Assert.AreEqual(3, result.Count);
        }
    }
}