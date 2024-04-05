using System.Net.WebSockets;
using System.Text;

namespace Logic.Wrappers
{
    internal class ClientWebSocketWrapper : IClientWebSocketWrapper
    {
        private readonly ClientWebSocket _clientWebSocket;

        public ClientWebSocketWrapper()
        {
            _clientWebSocket = new ClientWebSocket();
        }

        public async Task ConnectAsync(string serverUrl)
        {
            Uri serverUri = new(serverUrl);
            await _clientWebSocket.ConnectAsync(serverUri, CancellationToken.None);
        }

        public async Task<string> ReceiveMessage()
        {
            ArraySegment<byte> bytesReceived = new(new byte[1024]);
            WebSocketReceiveResult result = await _clientWebSocket.ReceiveAsync(bytesReceived, CancellationToken.None);
            if (bytesReceived.Array as byte[] != null)
            {
                return Encoding.UTF8.GetString(bytes: bytesReceived.Array, 0, result.Count);
            }
            return "";
        }
    }
}

