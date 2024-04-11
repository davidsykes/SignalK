using Logic.DataListener.Interfaces;
using System.Text.Json;

namespace Logic.DataListener
{
    internal class DeltaMessageConverter : IDeltaMessageConverter
    {
        SignalKDeltaMessage IDeltaMessageConverter.ConvertMessage(string deltaMessageJson)
        {
            var deltaMessage = JsonSerializer.Deserialize<UpdatesMessage>(deltaMessageJson);
            if (deltaMessage != null)
            {
                var values = new List<SignalKUpdateValue>();

                foreach (UpdatesMessage.MessageUpdates update in deltaMessage.updates)
                {
                    foreach (UpdatesMessage.UpdateValue value in update.values)
                    {
                        DateTime timestamp = DateTime.Parse(update.timestamp);
                        values.Add(new SignalKUpdateValue(timestamp, value.path, value.value));
                    }
                }

                var message = new SignalKDeltaMessage
                {
                    Values = values
                };
                return message;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        class UpdatesMessage
        {
            public string context { get; set; }
            public IList<MessageUpdates> updates { get; set; }

            public class MessageUpdates
            {
                public string timestamp { get; set; }
                public IList<UpdateValue> values { get; set; }
            }

            public class UpdateValue
            {
                public string path { get; set; }
                public object value { get; set; }
            }
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
