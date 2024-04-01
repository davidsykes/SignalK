// See https://aka.ms/new-console-template for more information
using HelloWorld;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

Console.WriteLine("Hello, World!");



var _httpClient = new HttpClient();
var url = "http://192.168.1.87:3000/signalk";

var response = await _httpClient.GetAsync(url);
var responseContent = response.Content;
using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
var responseText = await reader.ReadToEndAsync();


Console.WriteLine(responseText);

SignalKWebSocket sksocket = new("192.168.1.87");

await sksocket.ProcessAsync(() =>
{
    Console.WriteLine("hsheh");
});


using (ClientWebSocket ws = new ClientWebSocket())
{
    Uri serverUri = new Uri("ws://192.168.1.87:3000/signalk/v1/stream");
    await ws.ConnectAsync(serverUri, CancellationToken.None);
    while (ws.State == WebSocketState.Open)
    {
        var ee = await SignalKWebSocket.ReceiveAsync(ws);

        string msg = """
            {
                "name": "c# test server",
                "version": "1.0.4",
                "timestamp": "2018-06-21T15:09:16.704Z",
                "self": "vessels.urn:mrn:signalk:uuid:c0d79334-4e25-4245-8892-54e8ccc8021d",
                "roles": [
                    "master",
                    "main"
                ]
            }
            """;
        var r1 = await messAsync(ws, msg);

        msg = """
            {
              "requestId": "1234-45653-343454",
              "login": {
                "username": "admin",
                "password": "admin"
              }
            }
            """;
        r1 = await messAsync(ws, msg);

        msg = """{"updates":[{"values": [{"path":"a.b", "value":3.14}]}]}""";
        var r2 = messAsync(ws, msg);

        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", CancellationToken.None);
    }
}

async Task<string> messAsync(ClientWebSocket ws, String msg)
{
    ArraySegment<byte> bytesToSend = new(Encoding.UTF8.GetBytes(msg));
    await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
    ArraySegment<byte> bytesReceived = new(new byte[1024]);
    WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
    Console.WriteLine("vvvvvvvvv");
    Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
    return Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
}