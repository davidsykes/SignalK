namespace Logic.DataObjects
{
    public class SignalKUpdate(string source, DateTime timeStamp, IList<SignalKUpdateValue> values)
    {
        public String Source { get; set; } = source;
        public DateTime TimeStamp { get; set; } = timeStamp;
        public IList<SignalKUpdateValue> Values { get; set; } = values;
    }
}
