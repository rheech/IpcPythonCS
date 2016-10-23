using IpcPythonCS.Engine.CSharp.Communication;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IpcPythonCS.Engine.CSharp.RPC
{
    public class RPCWrapper
    {
        private ICommunicator _communicator;
        StringBuilder _sb;

        protected RPCWrapper(ICommunicator communicator)
        {
            _sb = new StringBuilder();
            _communicator = communicator;
        }

        /*public T CallFunction<T>(params object[] args)
        {
            object rtn;

            rtn = CallFunction(args);

            return (T)rtn;
        }*/

        protected T CallPythonFunction<T>(params object[] args)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();

            string methodName;
            ParameterInfo[] parameters;
            string functionDesc, functionRtn;

            methodName = stackFrames[1].GetMethod().Name;
            parameters = stackFrames[1].GetMethod().GetParameters();

            functionDesc = FunctionToXML(methodName, args);

            string s, rtn;
            s = this.ConvertFunctionToXML(methodName, args);

            _communicator.Write(s);

            rtn = _communicator.Read();

            return ConvertXMLToReturnValue<T>(rtn);
        }

        protected string ConvertFunctionToXML(string methodName, params object[] args)
        {
            string functionDesc;

            functionDesc = FunctionToXML(methodName, args);

            // Send function description and wait for return value
            //_pipe.Write(functionDesc);
            //functionRtn = _pipe.Read();

            return functionDesc;
        }

        protected T ConvertXMLToReturnValue<T>(string xml)
        {
            XmlDocument doc;
            XmlNodeList nodes;
            XmlNode node;
            string text;

            doc = new XmlDocument();
            doc.LoadXml(xml);

            nodes = doc.DocumentElement.SelectNodes("/");
            node = nodes[0];

            text = node.InnerText;

            return ConvertToGenericType<T>(text);
        }

        protected static T ConvertToGenericType<T>(string value)
        {
            object rtnVal;

            rtnVal = null;

            if (typeof(T) == typeof(int))
            {
                rtnVal = Convert.ToInt32(value);
            }
            else if (typeof(T) == typeof(double))
            {
                rtnVal = Convert.ToDouble(value);
            }
            else
            {
                throw new Exception("Undefined type.");
            }

            return (T)rtnVal;
        }

        private static string FunctionToXML(string functionName, params object[] args)
        {
            StringBuilder sb;

            sb = new StringBuilder();

            sb.AppendFormat("<function name=\"{0}\">{1}</function>", functionName, ParametersToXML(args));

            return sb.ToString();
        }

        private static string ParametersToXML(params object[] args)
        {
            StringBuilder sb;

            sb = new StringBuilder();

            foreach (object o in args)
            {
                sb.AppendFormat("<arg type=\"{0}\">{1}</arg>", o.GetType(), Convert.ChangeType(o, o.GetType()));
            }

            return sb.ToString();
        }
    }
}
