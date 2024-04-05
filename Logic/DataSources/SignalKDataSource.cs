using Logic.Wrappers;

namespace Logic.DataSources;

public class SignalKDataSource : ISignalKDataSource
{
    string _streamingUrl;
    IClientWebSocketWrapper _webSocket;

    internal SignalKDataSource(string streamingUrl)
    {
        _streamingUrl = streamingUrl;
        _webSocket = new ClientWebSocketWrapper();
    }

    internal SignalKDataSource(string streamingUrl, IClientWebSocketWrapper webSocket)
    {
        _streamingUrl = streamingUrl;
        _webSocket = webSocket;
    }

    internal async Task Initialise()
    {
        await _webSocket.ConnectAsync(_streamingUrl);
        await _webSocket.ReceiveMessage();
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
