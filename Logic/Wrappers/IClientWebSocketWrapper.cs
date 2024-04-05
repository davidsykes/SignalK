namespace Logic.Wrappers
{
    internal interface IClientWebSocketWrapper
    {
        Task ConnectAsync(string serverUrl);
        Task<string> ReceiveMessage();
    }
}
