using Logic;

Console.WriteLine("Hello, World!");

//var endPoints = await SignalKEndPoints.CreateAsync("192.168.1.87");

var streamingUrl = await new SignalKEndPointRetriever().RetrieveStreamingEndpoint("192.168.1.87");



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