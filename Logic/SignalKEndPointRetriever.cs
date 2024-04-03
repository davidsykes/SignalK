
namespace Logic
{
    public class SignalKEndPointRetriever
    {
        IHttpClientWrapper _httpClientWrapper;

        public SignalKEndPointRetriever()
        {
            _httpClientWrapper = new HttpClientWrapper();
        }

        internal SignalKEndPointRetriever(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public async Task<string> RetrieveStreamingEndpoint(string serverIp)
        {
            var endpointsUrl = $"http://{serverIp}:3000/signalk";
            var endpointsJson = await _httpClientWrapper.GetAsync(endpointsUrl);
            throw new NotImplementedException();
        }
    }
}
