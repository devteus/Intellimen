using System.Net;

namespace Intellimen.Business.Exceptions
{
    public class BadRequestException : IntellimenException
    {
        public BadRequestException(string message = null,
           string errorMessage = null, object request = null,
           object response = null) : base(message, errorMessage,
               HttpStatusCode.BadRequest, request, response)
        {
        }
    }
}
