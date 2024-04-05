
using Logic.DataSources;
using Logic.Wrappers;

namespace Logic;

public class SignalKDataSource : ISignalKDataSource
{
    IClientWebSocketWrapper _webSocket;

    internal SignalKDataSource(string streamingUrl)
    {
        _webSocket = new ClientWebSocketWrapper(streamingUrl);
    }

    internal void Initialise()
    {
        throw new NotImplementedException();
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
