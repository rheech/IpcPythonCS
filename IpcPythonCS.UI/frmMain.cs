using IpcPythonCS.Engine.CSharp;
using IpcPythonCS.Engine.CSharp.Communication;
using IpcPythonCS.Engine.CSharp.Communication.Pipe;
using IpcPythonCS.Engine.CSharp.Example;
using IpcPythonCS.Engine.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpcPythonCS.UI
{
    public partial class frmMain : Form
    {
        PythonExecutor python;
        PyCalculator calculator;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            runPython();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            calculator.Communicator.Close();
            python.Close();
        }

        private void runPython()
        {
            PipeClient client;

            python = new PythonExecutor();
            python.RunScript("main.py");

            client = new PipeClient();
            client.Connect("calculator");

            calculator = new PyCalculator(client);
        }

        private void btnAddition_Click(object sender, EventArgs e)
        {
            Stopwatch sw;

            sw = new Stopwatch();
            sw.Start();

            lblResult.Text = calculator.Addition((int)numFirst.Value, (int)numSecond.Value).ToString();
            sw.Stop();

            lblFuncCallResult.Text = String.Format("Processing time: {0} ms", sw.ElapsedMilliseconds);
        }

        private void btnSubtraction_Click(object sender, EventArgs e)
        {
            Stopwatch sw;

            sw = new Stopwatch();
            sw.Start();

            lblResult.Text = calculator.Subtraction((int)numFirst.Value, (int)numSecond.Value).ToString();
            sw.Stop();

            lblFuncCallResult.Text = String.Format("Processing time: {0} ms", sw.ElapsedMilliseconds);
        }
    }
}
