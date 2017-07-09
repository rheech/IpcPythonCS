from Example.PyCalculator import PyCalculator
from IpcPythonCS.Communication.Pipe.PipeServer import PipeServer

server = PipeServer()
server.WaitForConnection("calculator")
calc = PyCalculator(server)

#print("hello")

## Infinite execution ##
while (True):
    try:
        calc.ProcessFunctionCall()
    except:
        server.Close()
        server.WaitForConnection("calculator")
        calc = PyCalculator(server)

'''
## One time execution ##
try:
    while(True):
        calc.ProcessFunctionCall()

except:
    print("Connection ended.")
'''