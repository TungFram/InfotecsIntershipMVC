namespace InfotecsIntershipMVC.Services.Converting.Exceptions
{
    [Serializable]
    public class InvalidDateTimeException : Exception
    {
        public InvalidDateTimeException() : base() { }
        public InvalidDateTimeException(string message) : base(message) { }
        public InvalidDateTimeException(string message, Exception inner) : base(message, inner) { }
    }
}
