namespace SignalKLibrary
{
    public class SKLibraryException : Exception
    {
        public SKLibraryException()
        {
        }

        public SKLibraryException(string message)
            : base(message)
        {
        }

        public SKLibraryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
