using Intellimen.Business.Exceptions;
using Intellimen.Business.Helpers;
using Intellimen.Helpers;
using NLog;
using NLog.Web;
using System.Net;
using System.Net.Http.Headers;

namespace Intellimen.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Logger _logger;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.Setup()
                .LoadConfigurationFromAppSettings()
                .GetCurrentClassLogger();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await UseExceptionHandler(context, ex);
            }
        }

        private async Task UseExceptionHandler(
            HttpContext context, Exception exception)
        {
            if (context.Request.GetTypedHeaders().Accept.Any(header => header.MediaType == "application/json"))
                await UseApiExceptionHandler(context, exception);
            else
                UsePageExceptionHandler(context, exception);
        }

        public async Task<HttpContext> UseApiExceptionHandler(
            HttpContext context, Exception exception)
        {
            dynamic? exceptionError = GetError(exception);

            HttpStatusCode statusCode = (HttpStatusCode)(exceptionError?.StatusCode
                 ?? HttpStatusCode.InternalServerError);
            string? error = exceptionError?.Error;

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = (int)statusCode,
                error = error ?? "Um erro ocorreu e não foi possível concluir a solicitação!"
            });

            return context;
        }

        private HttpContext UsePageExceptionHandler(
            HttpContext context, Exception exception)
        {
            dynamic? exceptionError = GetError(exception);

            HttpStatusCode statusCode = (HttpStatusCode)(exceptionError?.StatusCode
                 ?? HttpStatusCode.InternalServerError);

            string? message = (string?)exceptionError?.Error;

            SessionHelper.SetTempValue("Error", message);

            context.Response.StatusCode = (int)statusCode;
            if (statusCode == HttpStatusCode.InternalServerError)
                context.Response.Redirect($"/error/{HttpStatusCode
                    .InternalServerError.ToString().ToLower()}");

            return context;
        }

        private dynamic? GetError(Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            IntellimenException? appException = null;
            string? error = null;

            if (exception != null && exception is IntellimenException)
            {
                appException = exception as IntellimenException;
                statusCode = appException.StatusCode;

                if (statusCode != HttpStatusCode.InternalServerError)
                    error = appException.UserMessage;
            }

            exception?.Log();

            return new { StatusCode = statusCode, Error = error };
        }
    }
}
