using System.Net;

namespace Intellimen.Business.Exceptions
{
    public class UnprocessableEntityException : IntellimenException
    {
        public UnprocessableEntityException(string? message = null,
          string? errorMessage = null, object? request = null,
          object? response = null) : base(message, errorMessage,
              HttpStatusCode.UnprocessableEntity, request, response)
        {
        }
    }
}
