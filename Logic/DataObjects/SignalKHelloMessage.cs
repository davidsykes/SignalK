using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logic.Review
{
    internal class SignalKHelloMessage
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
        [JsonPropertyName("self")]
        public string Self { get; set; } = string.Empty;

        public static SignalKHelloMessage DeserialiseSignalKHelloMessage(string json)
        {
            return JsonSerializer.Deserialize<SignalKHelloMessage>(json) ?? throw new SKLibraryException("Malformed Hello Message.");
        }
    }
}
