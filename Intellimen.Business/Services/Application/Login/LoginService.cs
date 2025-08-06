using Intellimen.Repository.Context;

namespace Intellimen.Business.Services.Application.Login
{
    public class LoginService(IntellimenDbContext context)
    {
        private readonly IntellimenDbContext _intellimenDbContext = context;


    }
}
