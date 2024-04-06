using System.Text.Json;

namespace Logic.Review
{
    class SignalKHelloMessage_
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE1006 // Naming Styles
        public string name { get; set; }
        public string version { get; set; }
        public string self { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
    }

    public class SignalKHelloMessage
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public string Self { get; set; }

        public SignalKHelloMessage(string hello)
        {
            SignalKHelloMessage_? m = JsonSerializer.Deserialize<SignalKHelloMessage_>(hello);
            if (m == null)
            {
                throw new SKLibraryException();
            }
            Name = m.name;
            Version = m.version;
            Self = m.self;
        }
    }
}
