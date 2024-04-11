using System.Net.WebSockets;
using System.Text;

namespace Logic.Wrappers
{
    internal class ClientWebSocketWrapper : IClientWebSocketWrapper
    {
        private readonly ClientWebSocket _clientWebSocket;

        internal ClientWebSocketWrapper()
        {
            _clientWebSocket = new ClientWebSocket();
        }

        async Task IClientWebSocketWrapper.ConnectAsync(string serverUrl)
        {
            Uri serverUri = new(serverUrl);
            await _clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);
        }

        async Task<string> IClientWebSocketWrapper.ReceiveMessage()
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
            ArraySegment<byte> bytesToSend = new(Encoding.UTF8.GetBytes(message));
            return _clientWebSocket.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        void IClientWebSocketWrapper.Close()
        {
            _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normal Close", CancellationToken.None);
        }
    }
}

