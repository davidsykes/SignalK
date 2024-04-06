using Logic.DataSources;

namespace Logic
{
    public class SignalKLibrary
    {
        public async static Task<ISignalKDataSource> CreateDataSource(string streamingUrl, string userName, string password)
        {
            var ds = new SignalKDataSource(streamingUrl, userName, password);
            await ds.Initialise();
            return ds;
        }
    }
}
