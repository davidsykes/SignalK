namespace SignalKLibrary.DataSources
{
    public interface ISignalKSettableValue<ValueType>
    {
        Task Set(ValueType value);
    }
}
