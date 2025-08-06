using Intellimen.Business.Util;
using Intellimen.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Intellimen
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder ConfigureDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<IntellimenDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Intellimen")));

            return builder;
        }
        public static async Task<WebApplication> ConfigureSettingsAsync(this WebApplication webApplication)
        {
            using (var serviceScope = webApplication.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                Settings.PASSWORD_DEFAULT = webApplication.Configuration["PasswordDefault"]!;
                return webApplication;
            }
        }

    }
}
