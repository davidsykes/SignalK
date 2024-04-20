using Logic.DataListener;
using Logic.DataListener.Interfaces;
using Logic.DataSources;
using Logic.Interfaces;

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