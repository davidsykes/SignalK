namespace Logic.DataObjects
{
    public class SignalKDeltaMessage(string context, IList<SignalKUpdate> updates)
    {
        public string Context { get; set; } = context;
        public IList<SignalKUpdate> Updates { get; set; } = updates;
    }
}
