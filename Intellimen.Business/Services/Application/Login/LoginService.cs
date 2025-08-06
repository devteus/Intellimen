using Intellimen.Business.DTOs;
using Intellimen.Business.Exceptions;
using Intellimen.Business.MessagesExceptions;
using Intellimen.Business.Requests;
using Intellimen.Business.Util;
using Intellimen.Repository.Context;
using Intellimen.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intellimen.Business.Services.Application.Login
{
    public class LoginService(IntellimenDbContext context) : ILoginService
    {
        private readonly IntellimenDbContext _intellimenDbContext = context;

        public async Task<UserDTO> AuthenticateAsync(LoginRequest request)
        {
            if (Settings.IS_DESENV) {
                if (!((request.Email ?? "").Equals(Settings.LOGIN_DESENV)
                    && (request.Password ?? "").Equals(Settings.SENHA_DESENV)))
                    throw new BadRequestException(DefaultExceptions.USER_NOT_FOUND); ;

                return new UserDTO
                {
                    Ide = 0,
                    Name = "Dev User",
                    Email = Settings.LOGIN_DESENV
                };
            }
            else {
                Validate(request);

                User? user = await _intellimenDbContext.User
                   .FirstOrDefaultAsync(u => u.Email.Equals(request.Email) && u.Password.Equals(Utilities.SHA256(request.Password)))
                   ?? throw new BadRequestException(DefaultExceptions.USER_NOT_FOUND);

                return new(user);
            }
        }

        private static void Validate(LoginRequest request)
        {
            var valid = new LoginValidator();
            var result = valid.Validate(request);

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).First();
                throw new BadRequestException(errorMessages);
            }
        }
    }
}
