using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpcPythonCS.Engine.CSharp.Communication
{
    public interface ICommunicator
    {
        void Write(string message);
        string Read();
        void Close();
        bool isConnected();
    }
}
