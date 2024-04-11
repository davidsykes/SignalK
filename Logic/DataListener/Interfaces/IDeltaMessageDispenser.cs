namespace Logic.DataListener.Interfaces
{
    internal interface IDeltaMessageDispenser
    {
        Task DispenseMessages(IDeltaMessageConverter messageProcessor);
    }
}
