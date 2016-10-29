using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpcPythonCS.Engine.CSharp.Example;
using IpcPythonCS.Engine.CSharp.Communication.Pipe;
using IpcPythonCS.Engine.CSharp;

namespace IPCPythonCS.Test.Engine.CSharp
{
    [TestClass]
    public class PyCalculatorTest
    {
        [TestMethod]
        public void CalculationTest()
        {
            PyCalculator calculator;
            PythonExecutor python;
            PipeClient client;

            python = new PythonExecutor();
            python.RunScript("Python\\main.py");

            client = new PipeClient();
            client.Connect("calculator");

            calculator = new PyCalculator(client);

            Assert.AreEqual(1 + 2, calculator.Addition(1, 2));
            Assert.AreEqual(1 - 2, calculator.Subtraction(1, 2));
        }
    }
}
