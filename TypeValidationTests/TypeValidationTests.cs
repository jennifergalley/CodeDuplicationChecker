using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountMatrixCloneDetection;

namespace TypeValidationTests
{
    [TestClass]
    public class TypeValidationTests
    {
        [TestMethod]
        public void ValidateType1Tests()
        {
            var currentPath = "../../../SampleCode/TypeValidationTests/Type1Tests/";

            // Act
            var cmcdResults = CMCD.Run(currentPath);

            Assert.IsTrue(cmcdResults.Count == 6);

            foreach (var result in cmcdResults)
            {
                Assert.IsTrue(result.Score == 0, string.Format("Test failed for {0}, {1}. Expected Score = 0, Actual score = {2}",
                    result.MethodA, result.MethodB, result.Score));
            }
        }

        [TestMethod]
        public void ValidateType2Tests()
        {
            var currentPath = "../../../SampleCode/TypeValidationTests/Type2Tests/";

            // Act
            var cmcdResults = CMCD.Run(currentPath);

            Assert.IsTrue(cmcdResults.Count == 3);

            foreach (var result in cmcdResults)
            {
                Assert.IsTrue(result.Score == 0, string.Format("Test failed for {0}, {1}. Expected Score = 0, Actual score = {2}",
                    result.MethodA, result.MethodB, result.Score));
            }
        }

        [TestMethod]
        public void ValidateType3Tests()
        {
            var currentPath = "../../../SampleCode/TypeValidationTests/Type3Tests/";

            // Act
            var cmcdResults = CMCD.Run(currentPath);

            Assert.IsTrue(cmcdResults.Count == 10);

            foreach (var result in cmcdResults)
            {
                if (string.CompareOrdinal(result.MethodA.MethodName, "DoubledSumFunctionWithLineSubtractions") == 0
                    || string.CompareOrdinal(result.MethodB.MethodName, "DoubledSumFunctionWithLineSubtractions") == 0
                    || string.CompareOrdinal(result.MethodA.MethodName, "DoubledSumFunctionWithLineAdditions") == 0
                    || string.CompareOrdinal(result.MethodB.MethodName, "DoubledSumFunctionWithLineAdditions") == 0)
                {
                    Assert.IsTrue(result.Score != 0, string.Format("Test failed for {0}, {1}. Expected Score should not be 0, Actual score = 0",
                       result.MethodA, result.MethodB));
                }
                else
                {
                    Assert.IsTrue(result.Score == 0, string.Format("Test failed for {0}, {1}. Expected Score = 0, Actual score = {2}",
                        result.MethodA, result.MethodB, result.Score));
                }
            }
        }
    }
}
