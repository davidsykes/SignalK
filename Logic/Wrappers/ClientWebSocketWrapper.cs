using Logic.Interfaces;
using System.Net.WebSockets;
using System.Text;

namespace Logic.Wrappers
{
    internal class ClientWebSocketWrapper : IClientWebSocketWrapper
    {
        private readonly ClientWebSocket _clientWebSocket;
        private readonly IMessageLogger? _messageLogger;

        internal ClientWebSocketWrapper(IMessageLogger? messageLogger)
        {
            _clientWebSocket = new ClientWebSocket();
            _messageLogger = messageLogger;
        }

        async Task IClientWebSocketWrapper.ConnectAsync(string serverUrl)
        {
            LogMessage("OPEN", serverUrl);
            Uri serverUri = new(serverUrl);
            await _clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);
        }

        async Task<string> IClientWebSocketWrapper.ReceiveMessage()
        {
            var message = await ReceiveMessageImp();
            LogMessage("RECV", message);
            return message;
        }

        async Task<string> ReceiveMessageImp()
        {
            ArraySegment<byte> bytesReceived = new(new byte[1024]);
            WebSocketReceiveResult result = await _clientWebSocket.ReceiveAsync(bytesReceived, CancellationToken.None);
            if (bytesReceived.Array as byte[] != null)
            {
                return Encoding.UTF8.GetString(bytes: bytesReceived.Array, 0, result.Count);
            }
            return "";
        }

        Task IClientWebSocketWrapper.SendMessage(string message)
        {
            LogMessage("SEND", message);
            ArraySegment<byte> bytesToSend = new(Encoding.UTF8.GetBytes(message));
            return _clientWebSocket.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        void IClientWebSocketWrapper.Close()
        {
            LogMessage("CLOSE", "");
            _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal Close", CancellationToken.None);
        }

        void LogMessage(string type, string message)
        {
            if (_messageLogger != null)
            {
                try
                {
                    _messageLogger.LogMessage($"{type} {message}");
                }
                catch { }
            }
        }
    }
}

