using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CountMatrixCloneDetection.UnitTests
{
    [TestClass]
    public class CMCDTests
    {
        private double[,] u = { { 0, 0, 0 }, { 0, 0, 0 } };
        private double[,] v = { { 0, 0, 0 }, { 0, 0, 0 } };

        [TestMethod]
        public void GetMethodsTest()
        {
            // Arrange
            var filepath = "../../../SampleCode/UnitTests/RiddledWithDuplicates.cs";

            // Act
            var result = CMCD.GetMethods(filepath);

            // Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void CalculateSimilarityDistanceTest()
        {
            // Act
            var result = CMCD.CalculateSimilarityDistance(u, v);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void EuclideanDistanceTest()
        {
            // Act
            var result = CMCD.EuclideanDistance(u, v);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ZeroPadMatrixTest()
        {
            // Act
            var result = CMCD.ZeroPadMatrix(u, 2);

            // Assert
            Assert.AreEqual(12, result.Length);
        }
    }
}
