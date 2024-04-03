namespace Logic.Review
{
    public class SignalKErrorException : Exception
    {
        public SignalKErrorException()
        {
        }

        public SignalKErrorException(string message)
            : base(message)
        {
        }

        public SignalKErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
