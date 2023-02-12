namespace InfotecsIntershipMVC.Services.Converting.Exceptions
{
    [Serializable]
    public class InvalidDurationException : Exception
    {
        public InvalidDurationException() : base() { }
        public InvalidDurationException(string message) : base(message) { }
        public InvalidDurationException(string message, Exception inner) : base(message, inner) { }
    }
}
