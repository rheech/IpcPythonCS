using IpcPythonCS.Engine.CSharp.Communication;
using IpcPythonCS.Engine.CSharp.RPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpcPythonCS.Engine.CSharp.Example
{
    public class PyCalculator : RPCWrapper
    {        
        public PyCalculator(ICommunicator communicator)
            : base(communicator)
        {
            
        }

        public int Addition(int a, int b)
        {
            return CallPythonFunction<int>(a, b);
        }

        public int Subtraction(int a, int b)
        {
            return CallPythonFunction<int>(a, b);
        }
    }
}
