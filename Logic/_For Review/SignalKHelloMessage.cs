using System.Text.Json;

namespace Logic.Review
{
    class SignalKHelloMessage_
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE1006 // Naming Styles
        internal string name { get; set; }
        internal string version { get; set; }
        internal string self { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore IDE1006 // Naming Styles
    }

    internal class SignalKHelloMessage
    {
        internal string Name { get; set; }
        internal string Version { get; set; }
        internal string Self { get; set; }

        internal SignalKHelloMessage(string hello)
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
