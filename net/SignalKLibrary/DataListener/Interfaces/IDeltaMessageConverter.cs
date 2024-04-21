using SignalKLibrary.DataObjects;

namespace SignalKLibrary.DataListener.Interfaces
{
    internal interface IDeltaMessageConverter
    {
        SignalKDeltaMessage ConvertMessage(string deltaMessage);
    }
}
