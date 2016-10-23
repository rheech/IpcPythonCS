using IpcPythonCS.Engine.CSharp;
using IpcPythonCS.Engine.CSharp.Communication;
using IpcPythonCS.Engine.CSharp.Communication.Pipe;
using IpcPythonCS.Engine.CSharp.Example;
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
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnAddition_Click(object sender, EventArgs e)
        {
            PipeClient client = new PipeClient();
            PyCalculator calculator;
            PythonExecutor python;
            Stopwatch sw;

            sw = new Stopwatch();
            sw.Start();

            python = new PythonExecutor();
            python.RunScript("Python\\main.py");

            client.Connect("calculator");

            calculator = new PyCalculator(client);

            lblResult.Text = calculator.Addition((int)numFirst.Value, (int)numSecond.Value).ToString();
            sw.Stop();

            lblFuncCallResult.Text = String.Format("Processing time: {0} ms", sw.ElapsedMilliseconds);
            client.Close();
        }

        private void btnSubtraction_Click(object sender, EventArgs e)
        {
            PipeClient client = new PipeClient();
            PyCalculator calculator;
            PythonExecutor python;
            Stopwatch sw;

            sw = new Stopwatch();
            sw.Start();

            python = new PythonExecutor();
            python.RunScript("Python\\main.py");

            client.Connect("calculator");

            calculator = new PyCalculator(client);

            lblResult.Text = calculator.Subtraction((int)numFirst.Value, (int)numSecond.Value).ToString();
            sw.Stop();

            lblFuncCallResult.Text = String.Format("Processing time: {0} ms", sw.ElapsedMilliseconds);

            client.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
