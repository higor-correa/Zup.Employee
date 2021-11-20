using Microsoft.AspNetCore.Mvc;
using Zup.Employees.Application.Services.EmployeeContacts;
using Zup.Employees.Domain.DTOs;

namespace Zup.Employees.API.Controllers
{
    [ApiController]
    [Route("api/v1/employee/{employeeId}/contact")]
    public class EmployeeContactController : ControllerBase
    {
        private readonly IEmployeeContactFacade _contactFacade;

        public EmployeeContactController(IEmployeeContactFacade employeeContactFacade)
        {
            _contactFacade = employeeContactFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid employeeId)
        {
            return Ok(await _contactFacade.GetAllFromEmployee(employeeId));
        }

        [HttpGet("{id}", Name = nameof(EmployeeContactController) + nameof(Details))]
        public async Task<IActionResult> Details(Guid id)
        {
            var contact = await _contactFacade.GetAsync(id);

            return contact == null
                    ? NotFound()
                    : Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid employeeId, ContactDTO createContactDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            createContactDTO.EmployeeId = employeeId;

            var contact = await _contactFacade.CreateAsync(createContactDTO);

            return CreatedAtRoute(nameof(EmployeeContactController) + nameof(Details), new { employeeId, id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid employeeId, Guid id, ContactDTO contactDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            contactDTO.EmployeeId = employeeId;
            contactDTO.Id = id;

            var contact = await _contactFacade.UpdateAsync(contactDTO);

            return contact == null
                    ? NotFound()
                    : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contactFacade.DeleteAsync(id);

            return NoContent();
        }
    }
}
