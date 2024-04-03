using System.Net.WebSockets;
using System.Text;

class DataGetter
{
    public static async Task GetData()
    {

        using (ClientWebSocket ws = new())
        {
            Uri serverUri = new Uri("ws://192.168.1.87:3000/signalk/v1/stream");
            await ws.ConnectAsync(serverUri, CancellationToken.None);
            while (ws.State == WebSocketState.Open)
            {
                await ReceiveMessages(ws);

                //await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
            }
        }

        async Task ReceiveMessages(ClientWebSocket ws)
        {
            while (true)
            {
                var timeOut = new CancellationTokenSource(500).Token;
                ArraySegment<byte> bytesReceived = new(new byte[1024]);
                try
                {
                    WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, timeOut);
                    Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
                }
                catch (System.Threading.Tasks.TaskCanceledException)
                {
                    Console.WriteLine("Cancelled");
                    return;
                }
            }
        }
    }
}

