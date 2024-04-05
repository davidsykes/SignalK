using Logic.DataSources;

namespace Logic
{
    public class SignalKLibrary
    {
        public static ISignalKDataSource CreateDataSource(string streamingUrl)
        {
            var ds = new SignalKDataSource(streamingUrl);
            ds.Initialise();
            return ds;
        }
    }
}
