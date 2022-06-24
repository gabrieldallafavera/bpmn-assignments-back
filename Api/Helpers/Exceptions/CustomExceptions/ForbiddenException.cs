namespace Api.Helpers.Exceptions.CustomExceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message) { }
    }
}
