using Logic.MessageHandlers;
using Logic.Wrappers;
using System.Text.Json;

internal class SKLogInHandler : ISKLogInHandler
{
    private readonly string _userName;
    private readonly string _password;
    private readonly IClientWebSocketWrapper _client;
    private readonly IGuidWrapper _guidWrapper;

    public SKLogInHandler(string userName, string password,
        IClientWebSocketWrapper socketWrapper,
        IGuidWrapper guidWrapper)
    {
        _userName = userName;
        _password = password;
        _client = socketWrapper;
        _guidWrapper = guidWrapper;
    }

    public async Task LogIn()
    {
        var logInMessage = MakeLogInMessage();
        await _client.SendMessage(logInMessage);
        await _client.ReceiveMessage();
    }

    private string MakeLogInMessage()
    {
        var requestId = _guidWrapper.NewGuid();

        var logIn = new LogInMessage(requestId, _userName, _password);

        var json = JsonSerializer.Serialize(logIn);

        return json;
    }
#pragma warning disable IDE1006 // Naming Styles
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
#pragma warning restore IDE1006 // Naming Styles
}
