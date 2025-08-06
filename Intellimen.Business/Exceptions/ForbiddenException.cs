using System.Net;

namespace Intellimen.Business.Exceptions
{
    public class ForbiddenException : IntellimenException
    {
        public ForbiddenException(string? message = null,
           string? errorMessage = null, object? request = null,
           object? response = null) : base(message, errorMessage,
               HttpStatusCode.Forbidden, request, response)
        {
        }
    }
}
