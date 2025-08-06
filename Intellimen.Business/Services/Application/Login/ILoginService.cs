using Intellimen.Business.DTOs;
using Intellimen.Business.Requests;

namespace Intellimen.Business.Services.Application.Login
{
    public interface ILoginService
    {
        public Task<UserDTO> AuthenticateAsync(LoginRequest request);
    }
}
