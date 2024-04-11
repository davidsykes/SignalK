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

        async Task IDeltaMessageDispenser.DispenseMessages(IDeltaMessageConverter messageConverter)
        {
            await _webSocket.ConnectAsync(_streamingUrl);
            await _webSocket.ReceiveMessage();

            await DispenseAllMessages(messageConverter);
        }

        internal async Task DispenseAllMessages(IDeltaMessageConverter messageConverter)
        {
            while (true)
            {
                var message = await _webSocket.ReceiveMessage();
                messageConverter.ConvertMessage(message);
            }
        }

        internal void Close()
        {
            _webSocket.Close();
        }
    }
}
