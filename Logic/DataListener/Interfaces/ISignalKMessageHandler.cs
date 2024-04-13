namespace Logic.DataListener.Interfaces
{
    internal interface ISignalKMessageHandler
    {
        Task GetMessagesFromTheSignalKServerAndPassThemToTheSignalKMessageDispenser(ISignalKMessageDispenser messageDispenser);
    }
}
