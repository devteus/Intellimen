using System.Globalization;

namespace Intellimen.API.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;    
    }

    public async Task Invoke(HttpContext context)
    {
        var languagesSupported = CultureInfo.GetCultures(CultureTypes.AllCultures);
        var searchCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        var infoCultura = new CultureInfo("pt-BR");

        if (!string.IsNullOrWhiteSpace(searchCulture) 
            && languagesSupported.Any(c => c.Name.Equals(searchCulture))) {
           infoCultura = new CultureInfo(searchCulture);
        }

        CultureInfo.CurrentCulture = infoCultura;
        CultureInfo.CurrentUICulture = infoCultura;

        await _next(context);
    }
}
