using Intellimen;
using Intellimen.Helpers;
using Intellimen.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NLog.Web;
using System.Globalization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

bool https = builder.Configuration["Https"] == "1";

#region Propriedades requisições HTTP
ServicePointManager.Expect100Continue = true;
ServicePointManager.DefaultConnectionLimit = 9999;
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
    | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#endregion

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllersWithViews(options =>
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
.AddRazorRuntimeCompilation();

#region Sessão, Autenticação e AntifogeryToken
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "__ss";
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    if (https) options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "__ath";
                    options.LoginPath = "/login";
                    options.Cookie.SameSite = SameSiteMode.Lax;
                });

builder.Services.AddAntiforgery(options =>
{
    options.Cookie.Name = "__aft";
    options.HeaderName = "Afg-Token";
    options.Cookie.SameSite = SameSiteMode.Lax;
    if (https) options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
#endregion

#region NLog
builder.Logging.ClearProviders();
builder.Host.UseNLog();
#endregion

builder.ConfigureDI();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region Cache de arquivos estáticos
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", $"public,max-age={TimeSpan.FromDays(240).TotalSeconds}");
        ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddMonths(6).ToString("R", CultureInfo.InvariantCulture));
    }
});
#endregion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseStatusCodePages(ctx =>
{
    HttpStatusCode statusCode = (HttpStatusCode)ctx
        .HttpContext.Response.StatusCode;

    ctx.HttpContext.Response.Redirect(
        statusCode switch
        {
            HttpStatusCode.MethodNotAllowed
                or HttpStatusCode.Unauthorized
                or HttpStatusCode.Forbidden => "/",
            _ => $"/error/{statusCode.ToString().ToLower()}"
        });

    return Task.CompletedTask;
});

#region Handlers e Middlewares
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<BrowserSupportMiddleware>();
#endregion

app.MapRazorPages();

SessionHelper.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

app.Run();
