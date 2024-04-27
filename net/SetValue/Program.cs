using ApplicationParameters;
using SignalKLibrary;

var applicationParameterDefinitions = new List<ApplicationParameterDefinition>
            {
                new() {
                    Name = "serverip",
                    Type = ApplicationParameterType.RequiredParameter,
                    UsageMessage = "Ip address of the SignalK Server"
                },
                new() {
                    Name = "name",
                    Type = ApplicationParameterType.RequiredParameter,
                    UsageMessage = "The variable name to set"
                },
                new() {
                    Name = "value",
                    Type = ApplicationParameterType.RequiredParameter,
                    UsageMessage = "The value to set"
                }
            };

var manager = ApplicationParametersFactory.CreateApplicationParameters(
    "How to use the application",
    applicationParameterDefinitions,
    args
    );

var serverIp = manager.GetRequiredParameter<string>("serverip");
var name = manager.GetRequiredParameter<string>("name");
var value = manager.GetRequiredParameter<string>("value");


try
{
    var streamingUrl = await SignalKService.RetrieveStreamingEndpoint(serverIp);
    var logger = new ConsoleLogger();
    var dataSource = await SignalKService.CreateDataSource(streamingUrl, "logger", "logger", logger);
    var settableValue = dataSource.CreateValue<string>(name);
    await settableValue.Set(value);
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