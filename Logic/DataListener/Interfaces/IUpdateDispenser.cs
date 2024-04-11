namespace Logic.DataListener.Interfaces
{
    internal interface IUpdateDispenser
    {
        void DispenseUpdates(SignalKDeltaMessage deltaMessage);
    }
}
