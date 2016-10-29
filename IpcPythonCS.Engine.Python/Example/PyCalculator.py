from IpcPythonCS.RPC.RPCWrapper import RPCWrapper
from IpcPythonCS.Communication.ICommunicator import ICommunicator


class PyCalculator(RPCWrapper):
    _communicator = ICommunicator

    def __init__(self, communicator):
        self._communicator = communicator
        return

    def Addition(self, a, b):
        return a + b

    def Subtraction(self, a, b):
        return a - b
