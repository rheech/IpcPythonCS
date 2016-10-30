using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using IpcPythonCS.Engine.CSharp.Communication;
using System.IO;
using System.Threading;
using IpcPythonCS.Engine.ExceptionHandler;

namespace IpcPythonCS.Engine.CSharp
{
    /// <summary>
    /// Execute Python interpreter
    /// </summary>
    public class PythonExecutor
    {
        public const string DEFAULT_PYTHON_PATH = @"C:\Program Files\Python35\Python.exe";
        public const string DEFAULT_SCRIPT_PATH = @"..\..\..\IpcPythonCS.Engine.Python";
        private FileInfo _pythonInterpreter;
        private DirectoryInfo _scriptPath;
        private Thread _thread = null;
        private StringBuilder _sbOutput;

        public delegate void PythonAppMessageEvent(string output);
        public event PythonAppMessageEvent OnPythonClosed;
        public event PythonAppMessageEvent OnPythonError;

        public PythonExecutor()
        {
            _pythonInterpreter = new FileInfo(DEFAULT_PYTHON_PATH);
            _scriptPath = new DirectoryInfo(DEFAULT_SCRIPT_PATH);
        }

        public PythonExecutor(string pythonPath, string scriptPath)
        {
            _pythonInterpreter = new FileInfo(pythonPath);
            _scriptPath = new DirectoryInfo(scriptPath);
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
            StreamReader sr, stderr;
            string output;

            // Hide console window
            startInfo = new ProcessStartInfo(_pythonInterpreter.FullName, arg)
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            _sbOutput = new StringBuilder();
            output = "";

            proc = Process.Start(startInfo);

            sr = proc.StandardOutput;
            stderr = proc.StandardError;
            
            do
            {
                output = sr.ReadLine();
                _sbOutput.AppendFormat("{0}\r\n", output);
            } while (sr.Peek() != -1);

            //output = sr.ReadToEnd();

            proc.WaitForExit();
            proc.Close();

            if (stderr.Peek() != -1)
            {
                PythonErrorMessage(stderr.ReadToEnd());
            }

            if (OnPythonClosed != null)
            {
                OnPythonClosed(_sbOutput.ToString());
            }

            output = _sbOutput.ToString();

            if (output.Length > 2)
            {
                output = output.Substring(0, output.Length - 2);
            }

            return output;
        }

        public void PythonErrorMessage(string errorMessage)
        {
            throw new PythonExecutionException(errorMessage);

            if (OnPythonError != null)
            {
                OnPythonError(errorMessage);
            }
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
            FileInfo file = new FileInfo(String.Format("{0}\\{1}", _scriptPath.FullName, script));

            if (!file.Exists)
            {
                throw new FileNotFoundException(String.Format("{0} does not exist.", file.FullName));
            }
            
            return _runPythonInternal(String.Format("\"{0}\" {1}", file.FullName, CreateParameter(args)).Trim());
        }

        /// <summary>
        /// Run Python script in different thread
        /// </summary>
        /// <param name="script">Script file</param>
        /// <param name="args">Console output</param>
        public void RunScript(string script, params string[] args)
        {
            FileInfo file = new FileInfo(String.Format("{0}\\{1}", _scriptPath.FullName, script));

            if (!file.Exists)
            {
                throw new FileNotFoundException(String.Format("{0} does not exist.", file.FullName));
            }
            
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