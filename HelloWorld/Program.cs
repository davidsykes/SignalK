using HelloWorld;
using Logic;

Console.WriteLine("Hello, World!");

try
{
    var library = new SignalKLibrary();
    var streamingUrl = await library.RetrieveStreamingEndpoint("192.168.1.87");
    var logger = new ConsoleLogger();
    var dataSource = await SignalKLibrary.CreateDataSource(streamingUrl, "logger", "logger", logger);
    var value = dataSource.CreateValue<double>("home.temperature");
    await value.Set(3.412);
    dataSource.Close();

    var messageProcessor = new MessageProcessor();
    await SignalKLibrary.ProcessUpdates(streamingUrl, messageProcessor, logger);
}
catch (SKLibraryException ex)
{
    Console.WriteLine(ex.Message);
}


