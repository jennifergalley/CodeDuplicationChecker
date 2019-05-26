using CountMatrixCloneDetection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CountMatrixCloneDetection.IntegrationTests
{
    [TestClass]
    public class CMCDTests
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
