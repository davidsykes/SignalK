﻿using Logic;
using Logic.MessageHandlers;
using Logic.Wrappers;
using System.Text.Json;
using System.Text.Json.Serialization;

internal class SKLogInHandler(string userName, string password,
    IClientWebSocketWrapper socketWrapper,
    IGuidWrapper guidWrapper) : ISKLogInHandler
{
    private readonly string _userName = userName;
    private readonly string _password = password;
    private readonly IClientWebSocketWrapper _client = socketWrapper;
    private readonly IGuidWrapper _guidWrapper = guidWrapper;

    async Task ISKLogInHandler.LogIn()
    {
        var logInMessage = MakeLogInMessage();
        await _client.SendMessage(logInMessage);
        var responseJson = await _client.ReceiveMessage();
        var response = JsonSerializer.Deserialize<LogInResponse>(responseJson);
        if (response == null || response.State != "COMPLETED" || response.StatusCode != 200)
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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    class LogInMessage(string requestId, string userName, string password)
    {
        [JsonPropertyName("requestid")]
        internal string RequestId { get; set; } = requestId;
        [JsonPropertyName("login")]
        internal LogIn Login { get; set; } = new(userName, password);

        internal class LogIn(string userName, string password)
        {
            [JsonPropertyName("username")]
            internal string Username { get; set; } = userName;
            [JsonPropertyName("password")]
            internal string Password { get; set; } = password;
        }
    }
    class LogInResponse
    {
        [JsonPropertyName("state")]
        internal string State { get; set; }
        [JsonPropertyName("statuscode")]
        internal int StatusCode { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
