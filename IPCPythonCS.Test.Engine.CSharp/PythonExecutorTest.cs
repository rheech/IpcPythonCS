using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpcPythonCS.Engine.CSharp;

namespace IPCPythonCS.Test.Engine.CSharp
{
    [TestClass]
    public class PythonExecutorTest
    {
        [TestMethod]
        public void RunPythonTest()
        {
            PythonExecutor executor;
            executor = new PythonExecutor();

            System.IO.File.WriteAllText("test.py", "print(\"hello\")");

            executor.RunScript("test.py");
            executor.Close();
        }

        [TestMethod]
        public void RunPythonTestAndGetReturn()
        {
            PythonExecutor executor;
            string rtn;
            executor = new PythonExecutor();

            System.IO.File.WriteAllText("test.py", "print(\"hello\")");

            rtn = executor.RunScriptReturn("test.py");
            executor.Close();

            Assert.AreEqual("hello", rtn);
        }
    }
}
