using Logic;
using Logic.DataObjects;

namespace HelloWorld
{
    internal class MessageProcessor : ISignalKUpdateHandler
    {
        public void InvalidServerMessage(string message)
        {
            Console.WriteLine($"INVALID: {message}");
        }

        public void Update(SignalKDeltaMessage signalKDeltaMessage)
        {
            foreach (var update in signalKDeltaMessage.Updates)
            {
                Console.WriteLine(update.TimeStamp.ToString());
                foreach (var value in update.Values)
                    Console.WriteLine($"\t{value.Path} => {value.Value}");
            }
        }
    }
}
