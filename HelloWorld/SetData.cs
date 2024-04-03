using System.Net.WebSockets;
using System.Text;

class DataSetter
{

    public static async Task SetData()
    {


        using (ClientWebSocket ws = new())
        {
            Uri serverUri = new Uri("ws://192.168.1.87:3000/signalk/v1/stream?subscribe=none");
            await ws.ConnectAsync(serverUri, CancellationToken.None);
            while (ws.State == WebSocketState.Open)
            {
                var msg = "";
                var r1 = await sendMessage(ws, msg);

                msg = """
            {
              "requestId": "1234-45653-343454",
              "login": {
                "username": "logger",
                "password": "logger"
              }
            }
            """;
                r1 = await sendMessage(ws, msg);

                msg = """{"updates":[{"values": [{"path":"a.d", "value":3.141}]}]}""";
                var r2 = sendMessage(ws, msg);

                await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
            }
        }

        async Task<string> sendMessage(ClientWebSocket ws, String msg)
        {
            Console.WriteLine("vvvvvvvvv");
            Console.WriteLine(msg);
            sendMessageText(ws, msg);
            Console.WriteLine("---------");
            var response = await receiveMessage(ws);
            Console.WriteLine(response);
            Console.WriteLine("^^^^^^^^^");
            return response;
        }

        async void sendMessageText(ClientWebSocket ws, String msg)
        {
            ArraySegment<byte> bytesToSend = new(Encoding.UTF8.GetBytes(msg));
            await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        async Task<string> receiveMessage(ClientWebSocket ws)
        {
            var timeOut = new CancellationTokenSource(500).Token;
            ArraySegment<byte> bytesReceived = new(new byte[1024]);
            try
            {
                WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, timeOut);
                return Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
            }
            catch (System.Threading.Tasks.TaskCanceledException)
            {
                Console.WriteLine("Cancelled");
                return "Cancelled";
            }
        }
    }
}