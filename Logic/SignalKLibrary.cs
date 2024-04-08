using Logic.DataListener;
using Logic.DataSources;

namespace Logic
{
    public class SignalKLibrary
    {
        public async static Task<ISignalKDataSource> CreateDataSource(string streamingUrl, string userName, string password)
        {
            streamingUrl += "?subscribe=none";
            var ds = new SignalKDataSource(streamingUrl, userName, password);
            await ds.Initialise();
            return ds;
        }

        public async static Task<ISignalKDataListener> CreateDataListener(string streamingUrl)
        {
            var ds = new SignalKDataListener(streamingUrl);
            await ds.Initialise();
            return ds;
        }
    }
}
