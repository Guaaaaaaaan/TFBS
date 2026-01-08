namespace TFBS.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string errorCode, string message)
        : base(400, errorCode, message) { }
}
