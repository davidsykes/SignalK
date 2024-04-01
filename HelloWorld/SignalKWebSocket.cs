using System.Net.WebSockets;
using System.Text;

namespace HelloWorld
{
    internal class SignalKWebSocket(String ip)
    {
        private String uri = $"ws://{ip}:3000/signalk/v1/stream";

        internal static async Task<string> ReceiveAsync(ClientWebSocket ws)
        {
            ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
            WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
            Console.WriteLine("rrrrrrrrrr");
            Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
            return Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
        }

        internal async Task ProcessAsync(Action value)
        {

            using (ClientWebSocket ws = new ClientWebSocket())
            {
                Uri serverUri = new Uri(uri);
                await ws.ConnectAsync(serverUri, CancellationToken.None);

                var ee = await SignalKWebSocket.ReceiveAsync(ws);
                Console.WriteLine(ee);
            }
        }
    }
}
