public class ResourceNotFound : Exception
{
    public ResourceNotFound() { }

    public ResourceNotFound(string message)
        : base(message) { }

    public ResourceNotFound(string message, Exception inner)
        : base(message, inner) { }
}