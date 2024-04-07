using Logic.Wrappers;
using System.Text.Json;

namespace Logic.DataSources
{
    internal class SignalKValue(string name, IClientWebSocketWrapper webSocket) : ISignalKValue
    {
        private readonly string _name = name;
        private readonly IClientWebSocketWrapper _webSocket = webSocket;

        public async Task Set(double value)
        {
            var valueMessage = new UpdatesMessage.ValuesMessage(_name, value);
            var updateMessage = new UpdatesMessage.UpdateMessage(new List<UpdatesMessage.ValuesMessage> { valueMessage });
            var updatesMessage = new UpdatesMessage([updateMessage]);

            var json = JsonSerializer.Serialize(updatesMessage);
            await _webSocket.SendMessage(json);
        }

        class UpdatesMessage
        {
            public IList<UpdateMessage> updates { get; set; }

            internal UpdatesMessage(IList<UpdateMessage> updates)
            {
                this.updates = updates;
            }

            internal class UpdateMessage
            {
                public IList<ValuesMessage> values { get; set; }

                internal UpdateMessage(IList<ValuesMessage> values)
                {
                    this.values = values;
                }
            }

            internal class ValuesMessage
            {
                public string path { get; set; }
                public double value { get; set; }

                internal ValuesMessage(string path, double value)
                {
                    this.path = path;
                    this.value = value;
                }
            }
        }
    }
}

