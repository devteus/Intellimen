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
        var linguagensSuportadas = CultureInfo.GetCultures(CultureTypes.AllCultures);
        var buscarCultura = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        var infoCultura = new CultureInfo("pt-BR");

        if (!string.IsNullOrWhiteSpace(buscarCultura) 
            && linguagensSuportadas.Any(c => c.Name.Equals(buscarCultura))) {
           infoCultura = new CultureInfo(buscarCultura);
        }

        CultureInfo.CurrentCulture = infoCultura;
        CultureInfo.CurrentUICulture = infoCultura;

        await _next(context);
    }
}
