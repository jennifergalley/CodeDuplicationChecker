using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CountMatrixCloneDetection.IntegrationTests
{
    [TestClass]
    public class CMCDTests
    {
        [TestMethod]
        public void Run_Self_Test()
        {
            // Arrange
            var currentPath = "../../../";

            // Act
            var cmcdResults = CMCD.Run(currentPath);

            // Assert
            Assert.IsTrue(cmcdResults.Any());
        }

        [TestMethod]
        public void Run_DemoTests_Test()
        {
            // Arrange
            var currentPath = "../../../SampleCode/DemoTests/";

            // Act
            var cmcdResults = CMCD.Run(currentPath);

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
            var cmcdResults = CMCD.Run(currentPath);

            // Assert
            Assert.IsTrue(cmcdResults.Any());
            Assert.AreEqual(3, cmcdResults.Count());
            Assert.AreEqual("RiddledWithDuplicates.cs", cmcdResults[0].MethodA.FileName);
            Assert.AreEqual("StupidFunction", cmcdResults[0].MethodA.MethodName);
            Assert.AreEqual("StupidFunctionDuplicate", cmcdResults[0].MethodB.MethodName);
            Assert.AreEqual("StupidFunction", cmcdResults[1].MethodA.MethodName);
            Assert.AreEqual("Log", cmcdResults[1].MethodB.MethodName);
            Assert.AreEqual("StupidFunctionDuplicate", cmcdResults[2].MethodA.MethodName);
            Assert.AreEqual("Log", cmcdResults[2].MethodB.MethodName);
        }
    }
}
