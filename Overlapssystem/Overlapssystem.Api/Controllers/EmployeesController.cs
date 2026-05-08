using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Application.Interfaces;

namespace Overlapssystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: api/employees
        // Henter alle medarbejdere til brug i dropdowns o.lign.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            try
            {
                // Hent entiteter fra din Application-service
                var employees = await _employeeService.GetAllEmployeesAsync();

                // Map entiteter til DTOs vha. din nye EmployeeMapper
                var dtos = employees.Select(EmployeeMapper.ToDto);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                // Log eventuel fejl her
                return StatusCode(500, "Der opstod en fejl ved hentning af medarbejdere.");
            }
        }
    }
}
