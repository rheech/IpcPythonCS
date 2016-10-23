using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using IpcPythonCS.Engine.CSharp.Communication;

namespace IpcPythonCS.Engine
{
    public class PythonExecutor
    {
        const string PYTHON_EXE_PATH = @"C:\Program Files\Python35\Python.exe";
        
        public PythonExecutor()
        {
        }

        private void RunPyton(string arg)
        {
            ProcessStartInfo startInfo;
            Process p;

            // Hide console window
            startInfo = new ProcessStartInfo(PYTHON_EXE_PATH, arg);
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            p = Process.Start(startInfo);
        }

        public void RunScript(string script, params string[] args)
        {
            RunPyton(String.Format("\"{0}\" \"{1}\"", script, args));
        }
    }
}