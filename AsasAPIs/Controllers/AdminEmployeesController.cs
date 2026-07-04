using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AsasAPIs.Data;
using AsasAPIs.Models;
using Microsoft.EntityFrameworkCore;
using AsasAPIs.DTOs;
using Asas.AsasHash.Asas.Models;

using Asas.AsasHash;
namespace AsasAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminEmployeesController : ControllerBase
    {
        private readonly AsasContext _context;
        private readonly AsasHash _asasHash;
        public AdminEmployeesController(AsasContext context, AsasHash asasHash) {

            _context = context;
            _asasHash = asasHash;
        }

        [HttpGet]
        public IActionResult GetEmployees() {
            var employees = _context.Employees
                .Include(e => e.EmpAuto)
                .Select(e => new EmployeeResponseDto {
                    Name = e.Name,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    Major = e.Major,
                    IsActive = e.IsActive,
                    JobTitle = e.EmpAuto != null ? e.EmpAuto.JobTitle : null
                })
                .ToList();
            return Ok(employees);

        }
    

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto employeeDto) {
            if (employeeDto == null) {
                return BadRequest("Employee data is required");
            }
            Hash hash = new Hash
            {
                RawPassword = employeeDto.Password,
                IsSucceeded = false

            };
            var passHash = _asasHash.HashPassword(hash);
            if (passHash.IsSucceeded != true) return BadRequest("An unexpected error occurred");
            var employee = new Employee {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HashingPassword = passHash.HashedPassword,
                Major = employeeDto.Major,
                ComId = employeeDto.ComId,
                IsActive = true,
                EmpAutoId = employeeDto.EmpAutoId
            };
             await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            var responseDto = new EmployeeResponseDto
            {
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Major = employee.Major,
                IsActive = employee.IsActive,
                JobTitle = "empty!"
            };

            return CreatedAtAction(nameof(GetEmployees), new { id = employee.EmployeeId }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id , [FromBody] UpdateEmployeeDto update)
        {

            if (update == null) return BadRequest("Invalid data.");

            var emp = await _context.Employees.FindAsync(id);

            if (emp == null) return NotFound("Employee not found.");

            emp.PhoneNumber = update.PhoneNumber;
            emp.Major = update.Major;
            emp.Name = update.Name;
            emp.EmpAutoId = update.EmpAutoId;
            await _context.SaveChangesAsync();

            return Ok("Employee updated successfully.");
        }
        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> DeactivateEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null)return NotFound($"Employee with ID {id} not found");
            if(!employee.IsActive) return BadRequest("Employee is already deactivated");

            employee.IsActive = false;
            _context.SaveChanges();
            return Ok($"Employee '{employee.Name}' has been successfully deactivated");
        }
    }
    }

