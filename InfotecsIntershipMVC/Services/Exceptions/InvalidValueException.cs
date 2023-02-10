namespace InfotecsIntershipMVC.Services.Exceptions
{
    [Serializable]
    public class InvalidValueException : Exception
    {
        public InvalidValueException() : base() { }
        public InvalidValueException(string message) : base(message) { }
        public InvalidValueException(string message, Exception inner) : base(message, inner) { }
    }
}
