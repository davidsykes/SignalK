using Logic.DataSources;

namespace Logic
{
    public class SignalKLibrary
    {
        public async static Task<ISignalKDataSource> CreateDataSource(string streamingUrl)
        {
            var ds = new SignalKDataSource(streamingUrl, "logger", "logger");
            await ds.Initialise();
            return ds;
        }
    }
}
