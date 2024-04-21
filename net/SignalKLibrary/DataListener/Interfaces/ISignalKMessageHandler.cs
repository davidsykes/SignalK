namespace SignalKLibrary.DataListener.Interfaces
{
    internal interface ISignalKMessageHandler
    {
        Task GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(ISignalKMessageDispenser messageDispenser);
    }
}
