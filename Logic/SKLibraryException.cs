namespace Logic
{
    public class SKLibraryException : Exception
    {
        internal SKLibraryException()
        {
        }

        internal SKLibraryException(string message)
            : base(message)
        {
        }

        internal SKLibraryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
