using firstPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public static List<Employee> employees = new()
        {
            new Employee {Id=1, Name="ram"},
            new Employee {Id=2, Name="hari"},
            new Employee {Id=3, Name="shyam"},
        };

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = employees.Find(x => x.Id == id);
            if(employee == null)
            {
                return NotFound("Employee Not Found");
            }

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Employee employee)
        {
            employees.Add(employee);
            return Ok(employee);
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            var employeeInList = employees.Find(x=>x.Id == employee.Id);
            if(employeeInList == null)
            {
                return NotFound("Not updated");
            }
            employeeInList.Name = employee.Name;
            return Ok(employeeInList);
        }

        [HttpDelete]

        public IActionResult DeleteCustomer(int id)
        {
            var employee = employees.Find(x => x.Id == id);
            if(employee == null)
            {
                return NotFound();
            }
            employees.Remove(employee);
            return Ok(employee);
        }

    }
}
