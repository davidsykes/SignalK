using Logic.DataListener.Interfaces;

namespace Logic.DataListener
{
    internal class SignalKMessageDispenser : ISignalKMessageDispenser
    {
        readonly private IDeltaMessageConverter _deltaMessageConverter;
        readonly private ISignalKUpdateHandler _updateHandler;

        internal SignalKMessageDispenser(IDeltaMessageConverter deltaMessageConverter, ISignalKUpdateHandler updateHandler)
        {
            _deltaMessageConverter = deltaMessageConverter;
            _updateHandler = updateHandler;
        }

        void ISignalKMessageDispenser.ConvertAndDispenseMessage(string signalKMessage)
        {
            try
            {
                var message = _deltaMessageConverter.ConvertMessage(signalKMessage);
                _updateHandler.Update(message);
            }
            catch (Exception)
            {
                try
                {
                    _updateHandler.InvalidServerMessage(signalKMessage);
                }
                catch { }
            }
        }
    }
}
