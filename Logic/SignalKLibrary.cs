using Logic.DataListener;
using Logic.DataListener.Interfaces;
using Logic.DataSources;

namespace Logic
{
    public class SignalKLibrary
    {
        SignalKEndPointRetriever? _signalKEndPointRetriever;

        public Task<string> RetrieveStreamingEndpoint(string serverIp)
        {
            _signalKEndPointRetriever ??= new SignalKEndPointRetriever();
            return _signalKEndPointRetriever.RetrieveStreamingEndpoint(serverIp);
        }

        public static async Task<ISignalKDataSource> CreateDataSource(string streamingUrl, string userName, string password)
        {
            streamingUrl += "?subscribe=none";
            ISignalKDataSource ds = new SignalKDataSource(streamingUrl, userName, password);
            await ds.Initialise();
            return ds;
        }

        public static Task ProcessUpdates(string streamingUrl, ISignalKUpdateHandler signalKUpdateHandler)
        {
            ISignalKMessageHandler mh = new SignalKMessageHandler(streamingUrl);
            var messageConverter = new DeltaMessageConverter();
            ISignalKMessageDispenser signalKMessageDispenser = new SignalKMessageDispenser(messageConverter, signalKUpdateHandler);
            return mh.GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(signalKMessageDispenser);
        }
    }
}
