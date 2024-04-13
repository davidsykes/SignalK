using HelloWorld;
using Logic;

Console.WriteLine("Hello, World!");

try
{
    var library = new SignalKLibrary();
    var streamingUrl = await library.RetrieveStreamingEndpoint("192.168.1.87");
    var dataSource = await SignalKLibrary.CreateDataSource(streamingUrl, "logger", "logger");
    var value = dataSource.CreateValue<double>("home.temperature");
    await value.Set(3.412);
    dataSource.Close();

    var messageProcessor = new MessageProcessor();
    SignalKLibrary.ProcessUpdates(streamingUrl, messageProcessor);
}
catch (SKLibraryException ex)
{
    Console.WriteLine(ex.Message);
}


var _httpClient = new HttpClient();
var url = "http://192.168.1.87:3000/signalk";

var response = await _httpClient.GetAsync(url);
var responseContent = response.Content;
using var reader = new StreamReader(await responseContent.ReadAsStreamAsync());
var responseText = await reader.ReadToEndAsync();

Console.WriteLine(responseText);

await DataGetter.GetData();
