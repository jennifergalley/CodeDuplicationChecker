using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NaiveStringComparer.UnitTests
{
    [TestClass()]
    public class NaiveStringComparerTests
    {
        NaiveStringComparer comparer = new NaiveStringComparer();

        [TestMethod()]
        public void GetSimilarityScore_SameTest()
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
        public void GetSimilarityScore_FirstEmptyTest()
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
        public void GetSimilarityScore_SecondEmptyTest()
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
        public void GetSimilarityScore_FirstNullTest()
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
        public void GetSimilarityScore_SecondNullTest()
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