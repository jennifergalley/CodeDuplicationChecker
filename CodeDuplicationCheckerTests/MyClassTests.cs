using System;
using CodeDuplicationChecker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeDuplicationChecker.Tests
{
    [TestClass]
    public class MyClassTests
    {
        MyClass myClass;

        [TestInitialize]
        public void Initialize()
        {
            myClass = new MyClass();
        }

        [TestMethod]
        public void HelloWorldTest()
        {
            // Act
            var result = myClass.HelloWorld();

            // Assert
            Assert.AreEqual("Hello world", result);
        }
    }
}
