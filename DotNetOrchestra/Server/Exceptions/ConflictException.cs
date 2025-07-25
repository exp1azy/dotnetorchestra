namespace DotNetOrchestra.Server.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException(string message) : base(409, message) { }
    }
}
