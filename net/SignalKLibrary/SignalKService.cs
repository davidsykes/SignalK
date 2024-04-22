using SignalKLibrary.DataListener;
using SignalKLibrary.DataListener.Interfaces;
using SignalKLibrary.DataSources;

namespace SignalKLibrary
{
    public class SignalKService
    {
        public static Task<string> RetrieveStreamingEndpoint(string serverIp)
        {
            return new SignalKEndPointRetriever().RetrieveStreamingEndpoint(serverIp);
        }

        public static async Task<ISignalKDataSource> CreateDataSource(
            string streamingUrl, string userName, string password, ISignalKMessageLogger? messageLogger = null)
        {
            streamingUrl += "?subscribe=none";
            ISignalKDataSource ds = new SignalKDataSource(streamingUrl, userName, password, messageLogger);
            await ds.Initialise();
            return ds;
        }

        public static Task ProcessUpdates(string streamingUrl, ISignalKUpdateHandler signalKUpdateHandler, ISignalKMessageLogger? messageLogger = null)
        {
            ISignalKMessageHandler mh = new SignalKMessageHandler(streamingUrl, messageLogger);
            var messageConverter = new DeltaMessageConverter();
            ISignalKMessageDispenser signalKMessageDispenser = new SignalKMessageDispenser(messageConverter, signalKUpdateHandler);
            return mh.GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(signalKMessageDispenser);
        }
    }
}