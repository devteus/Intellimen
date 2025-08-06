using System.Net;

namespace Intellimen.Business.Exceptions
{
    public class NotFoundException : IntellimenException
    {
        public NotFoundException(string? message = null,
           string? errorMessage = null, object? request = null,
           object? response = null) : base(message, errorMessage,
               HttpStatusCode.NotFound, request, response)
        {
        }
    }
}
