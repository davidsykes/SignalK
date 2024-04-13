namespace Logic.DataSources
{
    public interface ISignalKDataSource
    {
        Task Initialise();
        ISignalKSettableValue CreateValue<T>(string name);
        void Close();
    }
}
