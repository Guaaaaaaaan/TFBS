namespace TFBS.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string errorCode, string message)
        : base(404, errorCode, message) { }
}
