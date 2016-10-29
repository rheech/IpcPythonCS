using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IpcPythonCS.Engine.CSharp.Communication.Pipe;
using System.Threading;

namespace IPCPythonCS.Test.Engine.CSharp
{
    [TestClass]
    public class PipeTest
    {
        public void OpenPipeServer(string pipeName)
        {
            PipeServer pipeServer;
            string value;

            pipeServer = new PipeServer();
            pipeServer.WaitForConnection(pipeName);

            value = pipeServer.Read();
            pipeServer.Write(value);

            pipeServer.Close();
        }

        [TestMethod]
        public void TestPipeIO()
        {
            PipeClient client;
            Thread thread;
            string value;
            
            client = new PipeClient();

            thread = new Thread(() => OpenPipeServer("PipeTest"));
            thread.Start();

            client.Connect("PipeTest");
            
            client.Write("1");
            value = client.Read();

            Assert.AreEqual(1, Int32.Parse(value), "Pipe communication failed.");

            client.Close();
        }
    }
}