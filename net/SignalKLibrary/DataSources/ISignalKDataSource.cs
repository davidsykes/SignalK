namespace SignalKLibrary.DataSources
{
    public interface ISignalKDataSource
    {
        Task Initialise();
        ISignalKSettableValue<T> CreateValue<T>(string name);
        void Close();
    }
}
