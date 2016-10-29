from abc import abstractmethod

class ICommunicator:
    @abstractmethod
    def Write(self, message):
        pass

    @abstractmethod
    def Read(self):
        pass

    @abstractmethod
    def Close(self):
        pass