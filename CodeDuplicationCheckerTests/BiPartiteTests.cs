using System;
using Dedup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDuplicationChecker.Tests
{
    [TestClass]
    public class BipartiteTests
    {
        private double[,] u = { { 0, 0, 0 }, { 0, 0, 0 } };
        private double[,] v = { { 0, 0, 0 }, { 0, 0, 0 } };

        [TestMethod]
        public void GetBipartiteMatrixTest()
        {
            // Act
            var result = Bipartite.GetBipartiteMatrix(u, v);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(1, result[1]);
        }

        [TestMethod]
        public void CreateBipartiteMatrixTest()
        {
            // Act
            var result = Bipartite.CreateBipartiteMatrix(u, v);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(0, result[0, 0]);
            Assert.AreEqual(0, result[1, 0]);
            Assert.AreEqual(0, result[0, 1]);
            Assert.AreEqual(0, result[1, 1]);
        }
    }
}
