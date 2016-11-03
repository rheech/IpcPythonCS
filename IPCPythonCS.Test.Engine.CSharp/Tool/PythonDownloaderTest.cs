using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpcPythonCS.Engine.Tool;

namespace IPCPythonCS.Test.Engine.CSharp.Tool
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

            downloder.DownloadAndUnzip("https://www.cheonghyun.com/download/test.zip");

            txt = System.IO.File.ReadAllText("test\\test.txt");

            Assert.AreEqual("test", txt, "Unable to download and extract the specified file from url.");
        }
    }
}
