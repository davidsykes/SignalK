using System.Text.Json;
using System.Text.Json.Serialization;
using Logic.Wrappers;

namespace Logic
{
    class V1
    {
        [JsonPropertyName("version")]
        public String? Version { get; set; }
        [JsonPropertyName("signalk-http")]
        public String? SignalkHttp { get; set; }
        [JsonPropertyName("signalk-ws")]
        public String? SignalkWs { get; set; }
    }
    class Endpoints
    {
        public V1? v1 { get; set; }
    }
    internal class EndpointsMessage
    {
        [JsonPropertyName("endpoints")]
        public Endpoints? Endpoints { get; set; }
    }

    public class SignalKEndPointRetriever
    {
        readonly IHttpClientWrapper _httpClientWrapper;

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
            try
            {
                EndpointsMessage? ep = JsonSerializer.Deserialize<EndpointsMessage>(endpointsJson);

                return ep!.Endpoints!.v1!.SignalkWs!;
            }
            catch (JsonException)
            {
                throw new SKLibraryException("Streaming Endpoint not found.");
            }
        }
    }
}
