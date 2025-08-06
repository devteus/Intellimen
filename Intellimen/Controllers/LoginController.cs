using Intellimen.Business.DTOs;
using Intellimen.Business.Exceptions;
using Intellimen.Business.Requests;
using Intellimen.Business.Services.Application.Login;
using Intellimen.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Intellimen.Controllers
{
    public class LoginController(ILoginService loginService) : Controller
    {
        private readonly ILoginService _loginService = loginService;

        public async Task<IActionResult> IndexAsync()
        {
            if (!SessionHelper.IsLogado)
                await SessionHelper.LogOutAsync();

            TempData["Error"] = "";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            try
            {
                UserDTO? userDTO = await _loginService.AuthenticateAsync(request);
                if (!SessionHelper.IsLogado)
                    await SessionHelper.SignInAsync(userDTO!);

                return RedirectToAction("Index", "Home");
            }
            catch (IntellimenException ex)
            {
                TempData["Error"] = ex.Message;
                return View("Index");
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await SessionHelper.LogOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
