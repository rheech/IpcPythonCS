from Communication.ICommunicator import ICommunicator
from RPC.RPCWrapper import RPCWrapper

class PyCalculator(RPCWrapper):
    _communicator = ICommunicator

    def __init__(self, communicator):
        self._communicator = communicator
        return

    def Addition(self, a, b):
        return a + b

    def Subtraction(self, a, b):
        return a - b
