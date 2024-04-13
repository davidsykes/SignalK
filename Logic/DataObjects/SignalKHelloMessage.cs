using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logic.Review
{
    class SignalKHelloMessage_
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [JsonPropertyName("name")]
        internal string Name { get; set; }
        [JsonPropertyName("version")]
        internal string Version { get; set; }
        [JsonPropertyName("self")]
        internal string Self { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }

    internal class SignalKHelloMessage
    {
        internal string Name { get; set; }
        internal string Version { get; set; }
        internal string Self { get; set; }

        internal SignalKHelloMessage(string hello)
        {
            SignalKHelloMessage_? m = JsonSerializer.Deserialize<SignalKHelloMessage_>(hello) ?? throw new SKLibraryException();
            Name = m.Name;
            Version = m.Version;
            Self = m.Self;
        }
    }
}
