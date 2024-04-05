namespace Logic.Wrappers
{
    internal class ClientWebSocketWrapper : IClientWebSocketWrapper
    {
        private string _streamingUrl;

        public ClientWebSocketWrapper(string streamingUrl)
        {
            _streamingUrl = streamingUrl;
        }
    }
}
