using Logic;
using Logic.MessageHandlers;
using Logic.Wrappers;
using System.Text.Json;

internal class SKLogInHandler(string userName, string password,
    IClientWebSocketWrapper socketWrapper,
    IGuidWrapper guidWrapper) : ISKLogInHandler
{
    private readonly string _userName = userName;
    private readonly string _password = password;
    private readonly IClientWebSocketWrapper _client = socketWrapper;
    private readonly IGuidWrapper _guidWrapper = guidWrapper;

    public async Task LogIn()
    {
        var logInMessage = MakeLogInMessage();
        await _client.SendMessage(logInMessage);
        var responseJson = await _client.ReceiveMessage();
        var response = JsonSerializer.Deserialize<LogInResponse>(responseJson);
        if (response == null || response.state != "COMPLETED" || response.statusCode != 200)
        {
            throw new SKLibraryException("Login failed.");
        }
    }

    private string MakeLogInMessage()
    {
        var requestId = _guidWrapper.NewGuid();

        var logIn = new LogInMessage(requestId, _userName, _password);

        var json = JsonSerializer.Serialize(logIn);

        return json;
    }
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    class LogInMessage(string requestId, string userName, string password)
    {
        public string requestid { get; set; } = requestId;
        public LogIn login { get; set; } = new(userName, password);

        public class LogIn(string userName, string password)
        {
            public string username { get; set; } = userName;
            public string password { get; set; } = password;
        }
    }
    class LogInResponse
    {
        public string state { get; set; }
        public int statusCode { get; set; }
    }
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
