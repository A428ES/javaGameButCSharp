public class InvalidInput : Exception
{
    public InvalidInput() { }

    public InvalidInput(string message)
        : base(message) { }

    public InvalidInput(string message, Exception inner)
        : base(message, inner) { }
}