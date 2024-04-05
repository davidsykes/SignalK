namespace Logic.DataSources
{
    public interface ISignalKDataSource
    {
        ISignalKValue CreateValue<T>(string name);
    }
}
