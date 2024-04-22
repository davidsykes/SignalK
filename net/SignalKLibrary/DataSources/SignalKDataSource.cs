using SignalKLibrary.MessageHandlers;
using SignalKLibrary.Wrappers;

namespace SignalKLibrary.DataSources;

internal class SignalKDataSource : ISignalKDataSource
{
    readonly string _streamingUrl;
    readonly IClientWebSocketWrapper _webSocket;
    readonly ISKLogInHandler _logInHandler;

    internal SignalKDataSource(string streamingUrl, string userName, string password, ISignalKMessageLogger? messageLogger)
    {
        _streamingUrl = streamingUrl;
        _webSocket = new ClientWebSocketWrapper(messageLogger);
        var guidWrapper = new GuidWrapper();
        _logInHandler = new SKLogInHandler(userName, password, _webSocket, guidWrapper);
    }

    internal SignalKDataSource(string streamingUrl, IClientWebSocketWrapper webSocket, ISKLogInHandler logInHandler)
    {
        _streamingUrl = streamingUrl;
        _webSocket = webSocket;
        _logInHandler = logInHandler;
    }

    async Task ISignalKDataSource.Initialise()
    {
        await _webSocket.ConnectAsync(_streamingUrl);
        await _webSocket.ReceiveMessage();
        await _logInHandler.LogIn();
    }

    ISignalKSettableValue ISignalKDataSource.CreateValue<T>(string name)
    {
        return new SignalKSettableValue(name, _webSocket);
    }

    void ISignalKDataSource.Close()
    {
        _webSocket.Close();
    }
}
