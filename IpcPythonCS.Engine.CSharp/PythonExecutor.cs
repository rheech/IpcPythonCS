using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using IpcPythonCS.Engine.CSharp.Communication;
using System.IO;
using System.Threading;

namespace IpcPythonCS.Engine.CSharp
{
    /// <summary>
    /// Execute Python interpreter
    /// </summary>
    public class PythonExecutor
    {
        private string _pythonPath = @"C:\Program Files\Python35\Python.exe";
        private Thread _thread = null;
        private StringBuilder _sbOutput;

        public PythonExecutor()
        {
            
        }

        public PythonExecutor(string pythonPath)
        {
            _pythonPath = pythonPath;
        }

        /// <summary>
        /// Execute Python interpreter
        /// </summary>
        /// <param name="arg">Command line argument</param>
        /// <returns>Console output from python</returns>
        private string _runPythonInternal(string arg)
        {
            ProcessStartInfo startInfo;
            Process proc;
            StreamReader sr;
            string output;

            // Hide console window
            startInfo = new ProcessStartInfo(_pythonPath, arg)
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            _sbOutput = new StringBuilder();
            output = "";

            proc = Process.Start(startInfo);

            sr = proc.StandardOutput;

            do
            {
                output = sr.ReadLine();
                _sbOutput.AppendFormat("{0}\r\n", output);
            } while (sr.Peek() != -1);

            //output = sr.ReadToEnd();

            proc.WaitForExit();
            proc.Close();

            return output;
        }

        /// <summary>
        /// Run Python from another thread
        /// </summary>
        /// <param name="arg">Command line argument</param>
        private void RunPyton(string arg)
        {
            _thread = new Thread(() => _runPythonInternal(arg));
            _thread.Start();
        }

        /// <summary>
        /// Convert string[] parameters to a formatted parameter (e.g., "param1" "param2")
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Run Python script, wait for exit, and return the console output
        /// </summary>
        /// <param name="script">Script file</param>
        /// <param name="args">Command line argument</param>
        /// <returns>Console output</returns>
        public string RunScriptReturn(string script, params string[] args)
        {
            FileInfo file = new FileInfo(script);
            
            return _runPythonInternal(String.Format("\"{0}\" {1}", file.FullName, CreateParameter(args)).Trim());
        }

        /// <summary>
        /// Run Python script in different thread
        /// </summary>
        /// <param name="script">Script file</param>
        /// <param name="args">Console output</param>
        public void RunScript(string script, params string[] args)
        {
            FileInfo file = new FileInfo(script);
            
            RunPyton(String.Format("\"{0}\" {1}", file.FullName, CreateParameter(args)).Trim());
        }

        /// <summary>
        /// Close Python interpreter running on the other thread
        /// </summary>
        public void Close()
        {
            if (_thread != null && _thread.ThreadState == System.Threading.ThreadState.Running)
            {
                _thread.Abort();
            }
        }
    }
}