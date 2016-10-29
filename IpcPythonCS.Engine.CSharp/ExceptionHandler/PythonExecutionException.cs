using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpcPythonCS.Engine.ExceptionHandler
{
    public class PythonExecutionException : ApplicationException
    {
        public PythonExecutionException(string message) : base(message)
        {

        }
    }
}
