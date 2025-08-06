using Intellimen.Business.Services.Application.Login;
using Intellimen.Business.Util;
using Intellimen.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Intellimen
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder ConfigureDI(this WebApplicationBuilder builder)
        {
            Settings.IS_DESENV = builder.Configuration["Ambiente"] == "2";

            builder.Services.AddDbContext<IntellimenDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Intellimen")));

            AddServices(builder);

            return builder;
        }
        public static async Task<WebApplication> ConfigureSettingsAsync(this WebApplication webApplication)
        {
            using (var serviceScope = webApplication.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                
                Settings.LOGIN_DESENV_ATIVADO = webApplication.Configuration["LoginDesenvolvedor:Ativado"] == "1";
                Settings.LOGIN_DESENV = webApplication.Configuration["LoginDesenvolvedor:Login"]!;
                Settings.SENHA_DESENV = webApplication.Configuration["LoginDesenvolvedor:Senha"]!;
                Settings.PASSWORD_DEFAULT = webApplication.Configuration["PasswordDefault"]!;

                return webApplication;
            }
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ILoginService, LoginService>();
        }
    }
}
