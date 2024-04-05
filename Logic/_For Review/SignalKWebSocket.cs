using System.Net.WebSockets;
using System.Text;

namespace Logic.Review
{
    public class SignalKWebSocket(string ip)
    {
        public string Self => GetSelf();

        private readonly string uri = $"ws://{ip}:3000/signalk/v1/stream";
        private SignalKHelloMessage? _helloMessage;

        internal static async Task<string> ReceiveAsync(ClientWebSocket ws)
        {
            ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
            WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
            Console.WriteLine("rrrrrrrrrr");
            Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
            return Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
        }

        public async Task ProcessAsync(Action value)
        {

            using (ClientWebSocket ws = new ClientWebSocket())
            {
                Uri serverUri = new Uri(uri);
                await ws.ConnectAsync(serverUri, CancellationToken.None);

                var hello = await ReceiveAsync(ws);
                _helloMessage = new SignalKHelloMessage(hello);
            }
        }

        private string GetSelf()
        {
            if (_helloMessage == null)
            {
                throw new SignalKErrorException();
            }
            return _helloMessage.Self;
        }
    }
}
