namespace Logic.DataSources
{
    public interface ISignalKDataSource
    {
        Task Initialise();
        ISignalKValue CreateValue<T>(string name);
        void Close();
    }
}
