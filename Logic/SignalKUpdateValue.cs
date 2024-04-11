namespace Logic
{
    public class SignalKUpdateValue
    {
        public DateTime TimeStamp { get; internal set; }
        public string Path { get; internal set; }
        public object Value { get; internal set; }

        public SignalKUpdateValue(DateTime timeStamp, string path, object value)
        {
            TimeStamp = timeStamp;
            Path = path;
            Value = value;
        }
    }
}
