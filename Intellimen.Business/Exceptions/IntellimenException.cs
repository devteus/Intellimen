using Newtonsoft.Json;
using System.Net;

namespace Intellimen.Business.Exceptions
{
    public class IntellimenException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public override string Message => UserMessage ?? base.Message;
        public string ErrorMessage => base.Message;
        public string? UserMessage { get; }
        public string? Request { get; set; }
        public string? Response { get; set; }

        public IntellimenException(string? message = null, string? errorMessage = null,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            dynamic? request = null, dynamic? response = null)
            : base(errorMessage ?? message)
        {
            StatusCode = statusCode;
            UserMessage = message;
            Request = request != null ? JsonConvert.SerializeObject(request) : null;
            Response = response != null ? JsonConvert.SerializeObject(response) : null;
        }

        public static IntellimenException? Parse(Exception ex) =>
            ex is IntellimenException appEx ? appEx : null;

        public static IntellimenException ParseByStatusCode(string? message = null,
            string? errorMessage = null, HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
            dynamic? request = null, dynamic? response = null) =>
            statusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundException(message, errorMessage, request, response),
                HttpStatusCode.Forbidden => new ForbiddenException(message, errorMessage, request, response),
                HttpStatusCode.BadRequest => new BadRequestException(message, errorMessage, request, response),
                HttpStatusCode.Unauthorized => new UnauthorizedException(message, errorMessage, request, response),
                HttpStatusCode.UnprocessableEntity => new UnprocessableEntityException(message, request, response),
                _ => new IntellimenException(errorMessage: errorMessage, request: request, response: response),
            };
    }
}
