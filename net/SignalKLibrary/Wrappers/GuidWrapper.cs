namespace SignalKLibrary.Wrappers
{
    internal class GuidWrapper : IGuidWrapper
    {
        string IGuidWrapper.NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
