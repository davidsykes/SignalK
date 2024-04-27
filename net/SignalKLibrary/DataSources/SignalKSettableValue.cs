﻿using SignalKLibrary.Wrappers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SignalKLibrary.DataSources
{
    internal class SignalKSettableValue<ValueType>(string name, IClientWebSocketWrapper webSocket) : ISignalKSettableValue<ValueType>
    {
        private readonly string _name = name;
        private readonly IClientWebSocketWrapper _webSocket = webSocket;

        public async Task Set(ValueType value)
        {
            var valueMessage = new UpdatesMessage.ValuesMessage(_name, value);
            var updateMessage = new UpdatesMessage.UpdateMessage([valueMessage]);
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
                public ValueType Value { get; set; }

                internal ValuesMessage(string path, ValueType value)
                {
                    this.Path = path;
                    this.Value = value;
                }
            }
        }
    }
}

