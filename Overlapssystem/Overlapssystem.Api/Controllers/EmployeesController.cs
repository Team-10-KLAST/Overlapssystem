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
        // Gets a list of all employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            var dtos = employees.Select(EmployeeMapper.ToDto);

            return Ok(dtos);
        }
    }
}
