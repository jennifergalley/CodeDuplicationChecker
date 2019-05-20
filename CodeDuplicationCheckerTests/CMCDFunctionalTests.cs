using System;
using System.Linq;
using CountMatrixCloneDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDuplicationChecker.Tests
{
    [TestClass]
    public class CMCDFunctionalTests
    {
        [TestMethod]
        public void RunCMCDOnself()
        {
            var currentPath = @"..\\..\\..\\";
            var cmcdResults = CMCD.Run(currentPath);
            Assert.IsTrue(cmcdResults.Any());
        }
    }
}
