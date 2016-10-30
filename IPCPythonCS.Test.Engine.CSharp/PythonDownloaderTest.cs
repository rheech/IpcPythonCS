using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpcPythonCS.Engine.Tool;

namespace IPCPythonCS.Test.Engine.CSharp
{
    [TestClass]
    public class PythonDownloaderTest
    {
        /// <summary>
        /// Test downloading a zip file, unzip, and open
        /// </summary>
        [TestMethod]
        public void DownloadTest()
        {
            PythonDownloader downloder = new PythonDownloader();
            string txt;

            downloder.DownloadPython("https://www.cheonghyun.com/download/test.zip");

            txt = System.IO.File.ReadAllText("test.txt");

            Assert.AreEqual("test", txt);
        }
    }
}
