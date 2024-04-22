using SignalKLibrary;

Console.WriteLine("Hello, World!");

try
{
    var streamingUrl = await SignalKService.RetrieveStreamingEndpoint("192.168.1.87");
    var logger = new ConsoleLogger();
    var dataSource = await SignalKService.CreateDataSource(streamingUrl, "logger", "logger", logger);
    var value = dataSource.CreateValue<double>("home.temperature");
    await value.Set(3.412);
    dataSource.Close();
}
catch (SKLibraryException ex)
{
    Console.WriteLine(ex.Message);
}


class ConsoleLogger : ISignalKMessageLogger
{
    public void LogMessage(string message)
    {
        Console.WriteLine(message);
    }
}