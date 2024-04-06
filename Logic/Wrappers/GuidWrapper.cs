namespace Logic.Wrappers
{
    internal class GuidWrapper : IGuidWrapper
    {
        public string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
