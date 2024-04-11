namespace Logic
{
    internal class SignalKUpdateValue
    {
        internal DateTime TimeStamp { get; set; }
        internal string Path { get; set; }
        internal object Value { get; set; }

        internal SignalKUpdateValue(DateTime timeStamp, string path, object value)
        {
            TimeStamp = timeStamp;
            Path = path;
            Value = value;
        }
    }
}
