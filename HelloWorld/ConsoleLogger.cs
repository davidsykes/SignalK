using Logic.Interfaces;

namespace HelloWorld
{
    internal class ConsoleLogger : IMessageLogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
