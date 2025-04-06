using Intellimen.Communication.Requests;
using Intellimen.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Intellimen.API.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status201Created)]
    public IActionResult Registrar(RequestRegistrarUsuarioJson request)
    {
        return Created();
    }
}
