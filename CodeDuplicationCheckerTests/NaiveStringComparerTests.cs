using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaiveStringComparer.UnitTests
{
    [TestClass()]
    public class NaiveStringComparerTests
    {
        readonly NaiveStringComparer comparer = new NaiveStringComparer();

        [TestMethod()]
        public void GetSimilarityScore_Same_Test()
        {
            // Arrange
            var compare1 = "hello";
            var compare2 = "hello";

            // Act
            var result = comparer.Compare(compare1, compare2);

            // Assert
            Assert.AreEqual(0, result, "Similarity score returned was not correct.");
        }

        [TestMethod()]
        public void GetSimilarityScore_FirstEmpty_Test()
        {
            // Arrange
            var compare1 = "";
            var compare2 = "hello";

            // Act
            var result = comparer.Compare(compare1, compare2);

            // Assert
            Assert.AreEqual(double.MaxValue, result, "Similarity score returned was not correct.");
        }

        [TestMethod()]
        public void GetSimilarityScore_SecondEmpty_Test()
        {
            // Arrange
            var compare1 = "hello";
            var compare2 = "";

            // Act
            var result = comparer.Compare(compare1, compare2);

            // Assert
            Assert.AreEqual(double.MaxValue, result, "Similarity score returned was not correct.");
        }

        [TestMethod()]
        public void GetSimilarityScore_FirstNull_Test()
        {
            // Arrange
            string compare1 = null;
            var compare2 = "hello";

            // Act
            var result = comparer.Compare(compare1, compare2);

            // Assert
            Assert.AreEqual(double.MaxValue, result, "Similarity score returned was not correct.");
        }

        [TestMethod()]
        public void GetSimilarityScore_SecondNull_Test()
        {
            // Arrange
            var compare1 = "hello";
            string compare2 = null;

            // Act
            var result = comparer.Compare(compare1, compare2);

            // Assert
            Assert.AreEqual(double.MaxValue, result, "Similarity score returned was not correct.");
        }
    }
}