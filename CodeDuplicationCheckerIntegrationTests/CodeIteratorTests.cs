using CountMatrixCloneDetection;
using Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CodeDuplicationChecker.IntegrationTests
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
            Assert.AreEqual(6, results.Count);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[0].Filename);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[1].Filename);
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
            Assert.AreEqual(6, results.Count);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[0].Filename);
            Assert.AreEqual("RiddledWithDuplicates.cs", results[1].Filename);
        }

        [TestMethod]
        public void Run_Self_Test()
        {
            // Arrange
            var currentPath = "../../../";

            // Act
            var cmcdResults = CodeIterator.Run(currentPath, comparer);

            // Assert
            Assert.IsTrue(cmcdResults.Any());
        }

        [TestMethod]
        public void Run_DemoTests_Test()
        {
            // Arrange
            var currentPath = "../../../SampleCode/DemoTests/";

            // Act
            var cmcdResults = CodeIterator.Run(currentPath, comparer);

            // Assert
            Assert.IsTrue(cmcdResults.Any());
            Assert.AreEqual(153, cmcdResults.Count());
            Assert.AreEqual("DuplicateMethods.cs", cmcdResults[0].MethodA.FileName);
        }

        [TestMethod]
        public void Run_UnitTests_Test()
        {
            // Arrange
            var currentPath = "../../../SampleCode/UnitTests/";

            // Act
            var cmcdResults = CodeIterator.Run(currentPath, comparer);

            // Assert
            Assert.IsTrue(cmcdResults.Any());
            Assert.AreEqual(6, cmcdResults.Count());
            Assert.AreEqual("RiddledWithDuplicates.cs", cmcdResults[0].MethodA.FileName);
        }
    }
}