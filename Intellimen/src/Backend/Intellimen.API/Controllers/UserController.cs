using Intellimen.Application.UseCases.User.Register;
using Intellimen.Communication.Requests;
using Intellimen.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Intellimen.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public IActionResult Register(RequestRegisterUserJson request)
    {
        var useCase = new RegisterUserUseCase();
        var result = useCase.Execute(request);

        return Created(string.Empty, result);
    }
}
