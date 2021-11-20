using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zup.Employees.Application.Services.Security;

namespace Zup.Employees.API.Controllers;

[AllowAnonymous]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly ILoginFacade _loginFacade;

    public LoginController(ILoginFacade loginFacade)
    {
        _loginFacade = loginFacade;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LoginAsync([FromBody] string email, [FromBody] string password, [FromBody] List<string> roles)
    {
        var token = await _loginFacade.AuthenticateAsync(email, password, roles);

        return string.IsNullOrWhiteSpace(token)
                    ? Unauthorized()
                    : Ok(new { token });
    }
}
