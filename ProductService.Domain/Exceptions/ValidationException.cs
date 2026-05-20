using System.Net;

namespace ProductService.Domain.Exceptions;

public class ValidationException : AppException
{
    public ValidationException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.BadRequest;
}
