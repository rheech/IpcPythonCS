using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpcPythonCS.Engine.CSharp.Communication.Pipe
{
    public class PipeClient : ICommunicator, IDisposable
    {
        NamedPipeClientStream _pipeClient;
        StreamWriter _sw;
        string _pipeName;

        public PipeClient()
        {
        }

        ~PipeClient()
        {
            Close();
        }

        public void Connect(string pipeName)
        {
            _pipeName = pipeName;
            _pipeClient = new NamedPipeClientStream(".", _pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

            _pipeClient.Connect(2000);

            _sw = new StreamWriter(_pipeClient);
            //_sw.AutoFlush = true;
        }

        public void Write(string message)
        {
            _sw.Write(message);
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

            numBytes = _pipeClient.Read(buffer, offset, count);

            return System.Text.Encoding.UTF8.GetString(buffer);
        }

        public bool isConnected()
        {
            if (_pipeClient != null)
            {
                return (_pipeClient.CanRead && _pipeClient.CanWrite &&
                        _sw.BaseStream != null);
            }

            return false;
        }

        public string PipeName
        {
            get
            {
                return _pipeName;
            }
        }

        public void Close()
        {
            if (_pipeClient != null)
            {
                _pipeClient.Close();
            }
        }

        public void Dispose()
        {
            Close();
        }
    }
}
