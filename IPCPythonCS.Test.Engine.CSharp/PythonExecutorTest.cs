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
        }
    }
}
