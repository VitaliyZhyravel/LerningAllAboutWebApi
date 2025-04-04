namespace LearningWebApi.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string? name,object key) : base($"Entity: {name},Id: {key}") { }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
