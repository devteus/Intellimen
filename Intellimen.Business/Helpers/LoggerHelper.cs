using Intellimen.Business.Exceptions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace Intellimen.Business.Helpers
{
    public static class LoggerHelper
    {
        private static ILogger _logger;

        public static void Configure(ILogger logger) => _logger = logger;

        public static void Log(this Exception ex)
        {
            EventId eventId = new EventId();

            if (!(ex is IntellimenException appException))
                _logger.LogError(eventId, ex, ex.Message);

            else if (appException.StatusCode == HttpStatusCode.InternalServerError
                || appException?.Request != null || appException?.Response != null)
            {
                StringBuilder stringBuilder = new StringBuilder(ex.Message);
                if (appException?.Request != null) stringBuilder.Append($" REQUEST: ({appException.Request})");
                if (appException?.Response != null) stringBuilder.Append($" RESPONSE: ({appException.Response})");

                _logger.LogError(eventId, ex, stringBuilder.ToString());
            }
        }
    }
}
