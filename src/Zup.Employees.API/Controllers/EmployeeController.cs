using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zup.Employees.Application.Services.Employees;
using Zup.Employees.Domain.DTOs;

namespace Zup.Employees.API.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeFacade _employeeFacade;

        public EmployeeController(IEmployeeFacade employeeFacade)
        {
            _employeeFacade = employeeFacade;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO[]))]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeFacade.GetAsync());
        }

        [HttpGet("{id}", Name = nameof(EmployeeController) + nameof(Details))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _employeeFacade.GetAsync(id);

            return employee == null
                    ? NotFound()
                    : Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(EmployeeDTO createEmployeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var employee = await _employeeFacade.CreateAsync(createEmployeeDTO);

            return CreatedAtRoute(nameof(EmployeeController) + nameof(Details), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Edit(Guid id, EmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            employeeDTO.Id = id;
            await _employeeFacade.UpdateAsync(employeeDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _employeeFacade.DeleteAsync(id);
            return NoContent();
        }
    }
}
