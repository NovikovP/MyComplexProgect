using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeleteDirectory;

namespace UnitTestDeleteDirectory
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("C:\\Temp\\FolderToDelete", Program.deletePath);

        }
    }
}
