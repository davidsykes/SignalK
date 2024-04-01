using System.Text.Json;

namespace Logic
{
    class SignalKHelloMessage_
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE1006 // Naming Styles
        public String name { get; set; }
        public String version { get; set; }
        public String self { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
    }

    public class SignalKHelloMessage
    {
        public String Name { get; set; }
        public String Version { get; set; }
        public String Self { get; set; }

        public SignalKHelloMessage(String hello)
        {
            SignalKHelloMessage_? m = JsonSerializer.Deserialize<SignalKHelloMessage_>(hello);
            if (m == null)
            {
                throw new SignalKErrorException();
            }
            Name = m.name;
            Version = m.version;
            Self = m.self;
        }
    }
}
