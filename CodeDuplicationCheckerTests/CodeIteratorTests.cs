using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeDuplicationChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDuplicationChecker.Tests
{
    [TestClass()]
    public class CodeIteratorTests
    {
        [TestMethod()]
        public void CheckFileForDuplicatesTest()
        {
            // Arrange
            var filename = "../../SampleCode/RiddledWithDuplicates.cs";

            // Act
            var results = CodeIterator.CheckForDuplicates(filename);

            // Assert
            Assert.IsNotNull(results);
        }
    }
}