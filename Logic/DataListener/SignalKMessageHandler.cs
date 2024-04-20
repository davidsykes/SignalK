using Logic.DataListener.Interfaces;
using Logic.Interfaces;
using Logic.Wrappers;

namespace Logic.DataListener
{
    internal class SignalKMessageHandler : ISignalKMessageHandler
    {
        readonly string _streamingUrl;
        readonly IClientWebSocketWrapper _webSocket;

        internal SignalKMessageHandler(string streamingUrl, IMessageLogger? messageLogger)
        {
            _streamingUrl = streamingUrl;
            _webSocket = new ClientWebSocketWrapper(messageLogger);
        }

        internal SignalKMessageHandler(string streamingUrl, IClientWebSocketWrapper webSocket)
        {
            _streamingUrl = streamingUrl;
            _webSocket = webSocket;
        }

        async Task ISignalKMessageHandler.GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(ISignalKMessageDispenser messageDispenser)
        {
            await _webSocket.ConnectAsync(_streamingUrl);
            await _webSocket.ReceiveMessage();

            await DispenseAllMessages(messageDispenser);
        }

        internal async Task DispenseAllMessages(ISignalKMessageDispenser messageDispenser)
        {
            while (true)
            {
                var message = await _webSocket.ReceiveMessage();
                messageDispenser.ConvertAndDispenseMessage(message);
            }
        }

        internal void Close()
        {
            _webSocket.Close();
        }
    }
}
