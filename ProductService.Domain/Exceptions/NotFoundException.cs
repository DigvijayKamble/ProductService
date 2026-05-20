using System.Net;

namespace ProductService.Domain.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message) { }
    public override int StatusCode => (int)HttpStatusCode.NotFound;
}

