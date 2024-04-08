using Logic.Wrappers;

namespace Logic.DataListener
{
    internal class SignalKDataListener : ISignalKDataListener
    {
        readonly string _streamingUrl;
        readonly IClientWebSocketWrapper _webSocket;

        internal SignalKDataListener(string streamingUrl)
        {
            _streamingUrl = streamingUrl;
            _webSocket = new ClientWebSocketWrapper();
        }

        internal SignalKDataListener(string streamingUrl, IClientWebSocketWrapper webSocket)
        {
            _streamingUrl = streamingUrl;
            _webSocket = webSocket;
        }

        public async Task Initialise()
        {
            await _webSocket.ConnectAsync(_streamingUrl);
            await _webSocket.ReceiveMessage();
        }

        public async void ProcessMessages(ISignalKMessageProcessor messageProcessor)
        {
            var message = await _webSocket.ReceiveMessage();
            throw new NotImplementedException();
        }

        public void Close()
        {
            _webSocket.Close();
        }
    }
}