using Logic;
using Logic.DataObjects;

namespace HelloWorld
{
    internal class MessageProcessor : ISignalKUpdateHandler
    {
        public void InvalidServerMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void Update(SignalKDeltaMessage signalKDeltaMessage)
        {
            throw new NotImplementedException();
        }
    }
}
