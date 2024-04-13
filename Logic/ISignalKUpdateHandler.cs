using Logic.DataObjects;

namespace Logic
{
    public interface ISignalKUpdateHandler
    {
        void Update(SignalKDeltaMessage signalKDeltaMessage);
        void InvalidServerMessage(string message);
    }
}
