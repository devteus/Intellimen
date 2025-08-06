using System.Net;

namespace Intellimen.Business.Exceptions
{
    public class UnauthorizedException : IntellimenException
    {
        public UnauthorizedException(string? message = null,
           string? errorMessage = null, object? request = null,
           object? response = null) : base(message, errorMessage,
           HttpStatusCode.Unauthorized, request, response)
        {
        }
    }
}
