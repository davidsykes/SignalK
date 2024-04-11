using Logic.DataListener.Interfaces;

namespace Logic.DataListener
{
    internal class UpdateDispenser : IUpdateDispenser
    {
        private ISignalKUpdateHandler _updateHandler;

        public UpdateDispenser(ISignalKUpdateHandler updateHandler)
        {
            _updateHandler = updateHandler;
        }

        public void DispenseUpdates(SignalKDeltaMessage deltaMessage)
        {
            throw new NotImplementedException();
        }
    }
}
