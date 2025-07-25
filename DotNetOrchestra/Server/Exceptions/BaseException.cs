namespace DotNetOrchestra.Server.Exceptions
{
    public class BaseException : Exception
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public BaseException(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}
