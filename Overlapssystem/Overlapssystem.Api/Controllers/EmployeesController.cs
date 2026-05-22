using Microsoft.AspNetCore.Mvc;
using Overlapssystem.Api.Mappers;
using Overlapssystem.Shared.DTOs;
using Overlapssystem.Application.Interfaces;

namespace Overlapssystem.Api.Controllers;


// API controller responsible for managing employee data, allowing retrieval of employee information.
[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    // Constructor — receives the employee service via dependency injection.
    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // Gets a list of all employees
    // GET: api/employees
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();

        var dtos = employees.Select(EmployeeMapper.ToDto);

        return Ok(dtos);
    }
}
