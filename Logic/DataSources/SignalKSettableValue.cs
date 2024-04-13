using Logic.Wrappers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logic.DataSources
{
    internal class SignalKSettableValue(string name, IClientWebSocketWrapper webSocket) : ISignalKSettableValue
    {
        private readonly string _name = name;
        private readonly IClientWebSocketWrapper _webSocket = webSocket;

        async Task ISignalKSettableValue.Set(double value)
        {
            var valueMessage = new UpdatesMessage.ValuesMessage(_name, value);
            var updateMessage = new UpdatesMessage.UpdateMessage(new List<UpdatesMessage.ValuesMessage> { valueMessage });
            var updatesMessage = new UpdatesMessage([updateMessage]);

            var json = JsonSerializer.Serialize(updatesMessage);
            await _webSocket.SendMessage(json);
        }

        class UpdatesMessage
        {
            [JsonPropertyName("updates")]
            public IList<UpdateMessage> Updates { get; set; }

            internal UpdatesMessage(IList<UpdateMessage> updates)
            {
                this.Updates = updates;
            }

            internal class UpdateMessage
            {
                [JsonPropertyName("values")]
                public IList<ValuesMessage> Values { get; set; }

                internal UpdateMessage(IList<ValuesMessage> values)
                {
                    this.Values = values;
                }
            }

            internal class ValuesMessage
            {
                [JsonPropertyName("path")]
                public string Path { get; set; }
                [JsonPropertyName("value")]
                public double Value { get; set; }

                internal ValuesMessage(string path, double value)
                {
                    this.Path = path;
                    this.Value = value;
                }
            }
        }
    }
}

