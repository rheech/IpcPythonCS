from Communication.Pipe.PipeClient import PipeClient
from Communication.Pipe.PipeServer import PipeServer
from Example.PyCalculator import PyCalculator

server = PipeServer()
server.WaitForConnection("calculator")
calc = PyCalculator(server)

#print("hello")

try:
    while(True):
        calc.ProcessFunctionCall()

except:
    print("Connection ended.")