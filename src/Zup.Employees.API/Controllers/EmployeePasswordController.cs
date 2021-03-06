using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zup.Employees.Application.Services.Employees;
using Zup.Employees.Security.Domain.DTOs;

namespace Zup.Employees.API.Controllers;

[ApiController]
[Route("api/v1/employee/{employeeId}/password")]
public class EmployeePasswordController : ControllerBase
{
    private readonly IEmployeeFacade _employeeFacade;

    public EmployeePasswordController(IEmployeeFacade employeeFacade)
    {
        _employeeFacade = employeeFacade;
    }

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDTO changePasswordDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _employeeFacade.ChangePasswordAsync(changePasswordDTO);

        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> CreatePasswordAsync([FromBody] CreatePasswordDTO changePasswordDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _employeeFacade.CreatePasswordAsync(changePasswordDTO);

        return NoContent();
    }
}
