namespace Logic.DataListener.Interfaces
{
    public interface IDeltaMessageDispenser
    {
        Task DispenseMessages(IDeltaMessageConverter messageProcessor);
    }
}
