using Logic.DataListener.Interfaces;

namespace Logic.DataListener
{
    internal class UpdateDispenser : IUpdateDispenser
    {
        private ISignalKUpdateHandler _updateHandler;

        internal UpdateDispenser(ISignalKUpdateHandler updateHandler)
        {
            _updateHandler = updateHandler;
        }

        void IUpdateDispenser.DispenseUpdates(SignalKDeltaMessage deltaMessage)
        {
            throw new NotImplementedException();
        }
    }
}
