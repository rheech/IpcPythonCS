import win32file

from IpcPythonCS.Communication.ICommunicator import ICommunicator


class PipeClient(ICommunicator):
    _pipeName = None
    _fileHandle = None

    def __init__(self):
        return

    def Connect(self, pipeName):
        self._fileHandle = win32file.CreateFile(r"\\.\pipe\{0}".format(pipeName),
                                          win32file.GENERIC_READ | win32file.GENERIC_WRITE, 0, None,
                                          win32file.OPEN_EXISTING, 0, None)
        return

    def Write(self, message):
        win32file.WriteFile(self._fileHandle, message.encode('utf-8'))
        win32file.FlushFileBuffers(self._fileHandle)
        return

    def Read(self):
        data = win32file.ReadFile(self._fileHandle, 65536)
        return data[1]

    def Close(self):
        return