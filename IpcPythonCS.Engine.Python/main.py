from Example.PyCalculator import PyCalculator
from IpcPythonCS.Communication.Pipe.PipeServer import PipeServer

server = PipeServer()
server.WaitForConnection("calculator")
calc = PyCalculator(server)

#print("hello")

try:
    while(True):
        calc.ProcessFunctionCall()

except:
    print("Connection ended.")