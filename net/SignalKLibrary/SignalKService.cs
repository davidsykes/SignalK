using SignalKLibrary.DataListener;
using SignalKLibrary.DataListener.Interfaces;
using SignalKLibrary.DataSources;
using SignalKLibrary.Interfaces;

namespace SignalKLibrary
{
    public class SignalKService
    {
        SignalKEndPointRetriever? _signalKEndPointRetriever;

        public Task<string> RetrieveStreamingEndpoint(string serverIp)
        {
            _signalKEndPointRetriever ??= new SignalKEndPointRetriever();
            return _signalKEndPointRetriever.RetrieveStreamingEndpoint(serverIp);
        }

        public static async Task<ISignalKDataSource> CreateDataSource(
            string streamingUrl, string userName, string password, IMessageLogger? messageLogger = null)
        {
            streamingUrl += "?subscribe=none";
            ISignalKDataSource ds = new SignalKDataSource(streamingUrl, userName, password, messageLogger);
            await ds.Initialise();
            return ds;
        }

        public static Task ProcessUpdates(string streamingUrl, ISignalKUpdateHandler signalKUpdateHandler, IMessageLogger? messageLogger = null)
        {
            ISignalKMessageHandler mh = new SignalKMessageHandler(streamingUrl, messageLogger);
            var messageConverter = new DeltaMessageConverter();
            ISignalKMessageDispenser signalKMessageDispenser = new SignalKMessageDispenser(messageConverter, signalKUpdateHandler);
            return mh.GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(signalKMessageDispenser);
        }
    }
}