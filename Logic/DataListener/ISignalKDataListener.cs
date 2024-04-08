
namespace Logic.DataListener
{
    public interface ISignalKDataListener
    {
        Task Initialise();
        void ProcessMessages(ISignalKMessageProcessor messageProcessor);
    }
}
