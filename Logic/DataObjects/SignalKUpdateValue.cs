namespace Logic.DataObjects
{
    public class SignalKUpdateValue(string path, object value)
    {
        public string Path { get; set; } = path;
        public object Value { get; set; } = value;
    }
}
