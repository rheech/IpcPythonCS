using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpcPythonCS.Engine.CSharp.Communication.Pipe
{
    public class PipeServer : ICommunicator
    {
        // http://social.technet.microsoft.com/wiki/contents/articles/18193.named-pipes-io-for-inter-process-communication.aspx
        // https://blogs.msdn.microsoft.com/user_ed/2013/08/03/c-guru-named-pipes-io-for-inter-process-communication/
        NamedPipeServerStream _pipeServer;
        StreamWriter _sw;
        string _pipeName;

        public PipeServer()
        {
        }

        public void WaitForConnection(string pipeName)
        {
            _pipeName = pipeName;
            _pipeServer = new NamedPipeServerStream(_pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            _pipeServer.WaitForConnection();
            _sw = new StreamWriter(_pipeServer);
            //_sw.AutoFlush = true;
        }

        public void Write(string message)
        {
            _sw.WriteLine(message);
            _sw.Flush();
        }

        public string Read()
        {
            byte[] buffer;
            int offset;
            int count;
            int numBytes;

            buffer = new byte[65535];
            offset = 0;
            count = 65535;

            numBytes = _pipeServer.Read(buffer, offset, count);

            return System.Text.Encoding.UTF8.GetString(buffer);
        }

        public string PipeName
        {
            get
            {
                return _pipeName;
            }
        }

        public bool isConnected()
        {
            if (_pipeServer != null)
            {
                return (_pipeServer.CanRead && _pipeServer.CanWrite);
            }

            return false;
        }

        public void Close()
        {
            if (_pipeServer != null)
            {
                _pipeServer.Close();
            }
        }
    }
}
