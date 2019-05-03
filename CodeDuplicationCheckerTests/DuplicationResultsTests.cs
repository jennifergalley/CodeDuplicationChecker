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
    public class DuplicationResultsTests
    {
        DuplicationResults duplicationResults;

        [TestInitialize]
        public void TestInitialize()
        {
            duplicationResults = new DuplicationResults();
        }

        [TestMethod()]
        public void GenerateResultsFileTest()
        {
            duplicationResults.GenerateResultsFile(false);
        }
    }
}