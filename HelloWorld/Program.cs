// See https://aka.ms/new-console-template for more information
using Logic;
using System;
using System.Net.WebSockets;
using System.Text;

Console.WriteLine("Hello, World!");

//var endPoints = await SignalKEndPoints.CreateAsync("192.168.1.87");



var _httpClient = new HttpClient();
var url = "http://192.168.1.87:3000/signalk";

var response = await _httpClient.GetAsync(url);
var responseContent = response.Content;
using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
var responseText = await reader.ReadToEndAsync();

Console.WriteLine(responseText);

await DataSetter.SetData();
await DataGetter.GetData();

//SignalKWebSocket sksocket = new("192.168.1.87");

//await sksocket.ProcessAsync(() =>
//{
//    Console.WriteLine(sksocket.Self);
//});

return;


using (ClientWebSocket ws = new())
{
    Uri serverUri = new Uri("ws://192.168.1.87:3000/signalk/v1/stream?subscribe=none");
    await ws.ConnectAsync(serverUri, CancellationToken.None);
    while (ws.State == WebSocketState.Open)
    {
        //string msg = """
        //    {
        //        "name": "c# test server",
        //        "version": "1.0.4",
        //        "timestamp": "2018-06-21T15:09:16.704Z",
        //        "self": "vessels.urn:mrn:signalk:uuid:c0d79334-4e25-4245-8892-54e8ccc8021d",
        //        "roles": [
        //            "master",
        //            "main"
        //        ]
        //    }
        //    """;
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



        msg = "";
        await sendMessage(ws, msg);


        msg = """{"updates":[{"values": [{"path":"a.c", "value":3.143}]}]}""";
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
    Console.WriteLine("---------");
    response = response + await receiveMessage(ws);
    Console.WriteLine(response);
    Console.WriteLine("---------");
    response = response + await receiveMessage(ws);
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