namespace SignalKLibrary.Wrappers
{
    internal interface IClientWebSocketWrapper
    {
        Task ConnectAsync(string serverUrl);
        Task<string> ReceiveMessage();
        Task SendMessage(string message);
        void Close();
    }
}
