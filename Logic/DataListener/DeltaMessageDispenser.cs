using Logic.DataListener.Interfaces;
using Logic.Wrappers;

namespace Logic.DataListener
{
    internal class DeltaMessageDispenser : IDeltaMessageDispenser
    {
        readonly string _streamingUrl;
        readonly IClientWebSocketWrapper _webSocket;

        internal DeltaMessageDispenser(string streamingUrl)
        {
            _streamingUrl = streamingUrl;
            _webSocket = new ClientWebSocketWrapper();
        }

        internal DeltaMessageDispenser(string streamingUrl, IClientWebSocketWrapper webSocket)
        {
            _streamingUrl = streamingUrl;
            _webSocket = webSocket;
        }

        public async Task DispenseMessages(IDeltaMessageConverter messageConverter)
        {
            await _webSocket.ConnectAsync(_streamingUrl);
            await _webSocket.ReceiveMessage();

            await DispenseAllMessages(messageConverter);
        }

        public async Task DispenseAllMessages(IDeltaMessageConverter messageConverter)
        {
            while (true)
            {
                var message = await _webSocket.ReceiveMessage();
                messageConverter.ConvertMessage(message);
            }
        }

        public void Close()
        {
            _webSocket.Close();
        }
    }
}
