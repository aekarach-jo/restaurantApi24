using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectCafeApi.Models;
using ProjectCafeApi.Services;

namespace ProjectCafeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            var data = _employeeService.GetAllForApi();
            var num = data.Count();
            var id = "EMP0" + num.ToString();
            employee.Emp_Id = id;
            _employeeService.CreateEmployee(employee);
            return employee;
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetEmployee() => _employeeService.GetEmployeeAll();

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmpById(string id)
        {
            var data = _employeeService.GetEmployeeById(id);
            return data;
        }

        [HttpPut("{id}")]
        public IActionResult EditEmployee(string id, [FromBody] Employee employee)
        {
            var data = _employeeService.GetEmployeeById(id);
            if (data == null){
                return NotFound();
            }
            employee.Emp_Id = id;
            _employeeService.UpdateEmployee(id, employee);
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult DeleteEmployee(string id)
        {
            var data = _employeeService.GetEmployeeById(id);
            if (data == null){
                return NotFound();
            } else {
            data.Status = "deleted";
            _employeeService.UpdateEmployee(data.Emp_Id, data);
            return NoContent();
            }
        }
    }
}