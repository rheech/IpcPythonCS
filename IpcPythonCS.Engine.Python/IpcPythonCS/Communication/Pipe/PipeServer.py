import win32file
import win32pipe
import win32api

from IpcPythonCS.Communication.ICommunicator import ICommunicator


# http://stackoverflow.com/questions/13319679/createnamedpipe-in-python
class PipeServer(ICommunicator):
    _pipeName = None
    _pipeHandle = None

    def __init__(self):
        return

    def WaitForConnection(self, pipeName):
        self._pipeName = pipeName
        self._pipeHandle = win32pipe.CreateNamedPipe(r"\\.\pipe\{0}".format(pipeName), win32pipe.PIPE_ACCESS_DUPLEX,
                                                     win32pipe.PIPE_TYPE_MESSAGE | win32pipe.PIPE_WAIT,
                                                     1, 65536, 65536, 300, None)
        win32pipe.ConnectNamedPipe(self._pipeHandle, None)

    def Write(self, message):
        win32file.WriteFile(self._pipeHandle, message.encode('utf-8'))
        win32file.FlushFileBuffers(self._pipeHandle)
        return

    def Read(self):
        data = win32file.ReadFile(self._pipeHandle, 65536)
        return data[1]

    def Close(self):
        win32api.CloseHandle(self._pipeHandle)
        return