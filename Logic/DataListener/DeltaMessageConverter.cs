using Logic.DataListener.Interfaces;
using Logic.DataObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Logic.DataListener
{
    internal class DeltaMessageConverter : IDeltaMessageConverter
    {
        JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

        SignalKDeltaMessage IDeltaMessageConverter.ConvertMessage(string deltaMessageJson)
        {
            var deltaMessage = JsonSerializer.Deserialize<UpdatesMessage>(deltaMessageJson, options)!;
            var updates = new List<SignalKUpdate>();

            foreach (UpdatesMessage.MessageUpdates update in deltaMessage.Updates)
            {
                DateTime timestamp = DateTime.Parse(update.Timestamp);
                var values = new List<SignalKUpdateValue>();
                foreach (UpdatesMessage.UpdateValue value in update.Values)
                {
                    values.Add(new SignalKUpdateValue(value.Path, value.Value));
                }
                updates.Add(new SignalKUpdate(update.Source, timestamp, values));
            }

            return new SignalKDeltaMessage(deltaMessage.Context, updates);
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        class UpdatesMessage
        {
            [JsonPropertyName("context")]
            public string Context { get; set; }
            public IList<MessageUpdates> Updates { get; set; }

            internal class MessageUpdates
            {
                [JsonPropertyName("$source")]
                public string Source { get; set; }
                public string Timestamp { get; set; }
                public IList<UpdateValue> Values { get; set; }
            }

            internal class UpdateValue
            {
                public string Path { get; set; }
                public object Value { get; set; }
            }
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
