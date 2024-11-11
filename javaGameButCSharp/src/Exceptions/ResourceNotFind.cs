public class ResourceNotFound : Exception
{
    public ResourceNotFound() { }

    public ResourceNotFound(string message)
        : base(message) { }

    public ResourceNotFound(string message, Exception inner)
        : base(message, inner) { }

    // Override ToString() to display a custom message without the stack trace
    public override string ToString()
    {
        return $"ERROR: Resource Not Found: {Message}";
    }
}