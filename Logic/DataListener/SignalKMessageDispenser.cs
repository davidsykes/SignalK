using Logic.DataListener.Interfaces;

namespace Logic.DataListener
{
    internal class SignalKMessageDispenser : ISignalKMessageDispenser
    {
        private IDeltaMessageConverter _deltaMessageConverter;
        private ISignalKUpdateHandler _updateHandler;

        internal SignalKMessageDispenser(IDeltaMessageConverter deltaMessageConverter, ISignalKUpdateHandler updateHandler)
        {
            _deltaMessageConverter = deltaMessageConverter;
            _updateHandler = updateHandler;
        }

        void ISignalKMessageDispenser.ConvertAndDispenseMessage(string signalKMessage)
        {
            var message = _deltaMessageConverter.ConvertMessage(signalKMessage);
            _updateHandler.Update(message);
        }
    }
}
