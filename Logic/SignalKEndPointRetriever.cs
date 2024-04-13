using System.Text.Json;
using System.Text.Json.Serialization;
using Logic.Wrappers;

namespace Logic
{
    class V1
    {
        [JsonPropertyName("version")]
        internal String? Version { get; set; }
        [JsonPropertyName("signalk-http")]
        internal String? SignalkHttp { get; set; }
        [JsonPropertyName("signalk-ws")]
        internal String? SignalkWs { get; set; }
    }
    class Endpoints
    {
        internal V1? v1 { get; set; }
    }
    internal class EndpointsMessage
    {
        [JsonPropertyName("endpoints")]
        internal Endpoints? Endpoints { get; set; }
    }

    internal class SignalKEndPointRetriever
    {
        readonly IHttpClientWrapper _httpClientWrapper;

        internal SignalKEndPointRetriever()
        {
            _httpClientWrapper = new HttpClientWrapper();
        }

        internal SignalKEndPointRetriever(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        internal async Task<string> RetrieveStreamingEndpoint(string serverIp)
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
