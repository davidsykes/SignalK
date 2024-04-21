namespace SignalKLibrary.DataSources
{
    public interface ISignalKDataSource
    {
        Task Initialise();
        ISignalKSettableValue CreateValue<T>(string name);
        void Close();
    }
}
