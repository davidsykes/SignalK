using SignalKLibrary.DataObjects;

namespace SignalKLibrary
{
    public interface ISignalKUpdateHandler
    {
        void Update(SignalKDeltaMessage signalKDeltaMessage);
        void InvalidServerMessage(string message);
    }
}
