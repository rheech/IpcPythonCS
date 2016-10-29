using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using IpcPythonCS.Engine.CSharp.Communication;
using System.IO;

namespace IpcPythonCS.Engine.CSharp
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
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            p = Process.Start(startInfo);
        }

        private static string CreateParameter(params string[] args)
        {
            StringBuilder sb;

            sb = new StringBuilder();

            foreach (string s in args)
            {
                sb.AppendFormat("\"{0}\" ", s);
            }

            return sb.ToString().Trim();
        }

        public void RunScript(string script, params string[] args)
        {
            FileInfo file = new FileInfo(script);
            
            RunPyton(String.Format("\"{0}\" {1}", file.FullName, CreateParameter(args)).Trim());
        }
    }
}