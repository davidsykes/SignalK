using Logic.DataObjects;

namespace Logic.DataListener.Interfaces
{
    internal interface IDeltaMessageConverter
    {
        SignalKDeltaMessage ConvertMessage(string deltaMessage);
    }
}
