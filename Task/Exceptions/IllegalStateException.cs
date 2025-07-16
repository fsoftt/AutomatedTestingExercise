namespace EpamTask.Exceptions
{
    internal class IllegalStateException : Exception
    {
        public IllegalStateException(string message) : base(message)
        {
        }
        public IllegalStateException(string message, string url) : base($"{message}. Url: {url}")
        {
        }

        public IllegalStateException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
