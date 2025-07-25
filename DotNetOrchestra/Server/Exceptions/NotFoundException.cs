namespace DotNetOrchestra.Server.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(404, message) { }
    }
}
