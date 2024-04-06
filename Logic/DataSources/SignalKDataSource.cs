using Logic.MessageHandlers;
using Logic.Wrappers;

namespace Logic.DataSources;

public class SignalKDataSource : ISignalKDataSource
{
    readonly string _streamingUrl;
    readonly IClientWebSocketWrapper _webSocket;
    readonly ISKLogInHandler _logInHandler;

    internal SignalKDataSource(string streamingUrl, string userName, string password)
    {
        _streamingUrl = streamingUrl;
        _webSocket = new ClientWebSocketWrapper();
        var guidWrapper = new GuidWrapper();
        _logInHandler = new SKLogInHandler(userName, password, _webSocket, guidWrapper);
    }

    internal SignalKDataSource(string streamingUrl, IClientWebSocketWrapper webSocket, ISKLogInHandler logInHandler)
    {
        _streamingUrl = streamingUrl;
        _webSocket = webSocket;
        _logInHandler = logInHandler;
    }

    internal async Task Initialise()
    {
        await _webSocket.ConnectAsync(_streamingUrl);
        await _webSocket.ReceiveMessage();
        await _logInHandler.LogIn();
    }

    public SignalKValue CreateValue<T>(string name)
    {
        throw new NotImplementedException();
    }

    ISignalKValue ISignalKDataSource.CreateValue<T>(string name)
    {
        throw new NotImplementedException();
    }
}
