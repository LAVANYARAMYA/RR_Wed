using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RR.DataBaseConnect;
using RR.Models.EmployeeInfo;
using RR.Services;
using RR.Services.RequestClasses;

namespace RR.RewardsWebApi.Controllers.EmployeeInfo
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeeController : ControllerBase
    {
        public EmployeeServices employeeServices;

        public EmployeeController(DataBaseAccess dataBaseAccess)
        {
            employeeServices = new EmployeeServices(dataBaseAccess);
        }

        [HttpGet]
       /* [Authorize(Roles = "Admin,Moderator")]*/
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {


            var result = await employeeServices.GetEmployeeAsync();

           

            return Ok(result.Value.Select(x => new
            {
                name = x.Name,
                EmployeeId = x.EmployeeId,
                Designation=x.Designation,
                email=x.UserNamePassword.EmailID,
                Password = x.UserNamePassword.Password,
                Roles = x.Roles.Select(y => new
                {
                    roleId = y.IdOfRole,
                    Rolename = y.RoleName
                })
            }));


        }

        [HttpPost]

        public async Task<ActionResult<Employee>> AddEmployee(RequestEmployee requestEmployee)
        {
            var result = await employeeServices.AddEmployee(requestEmployee);

            return Ok(result.Value.Name+" Added");
        }
        [HttpPut]
        public async Task<ActionResult<Employee>> updateEmployee(RequestEmployee requestEmployee)
        {
            var result = await employeeServices.updateEmployee(requestEmployee);

            if(result==null)
            {
                return BadRequest("EmpId not Exist");
            }

            return Ok( new { Name=result.Value.Name , Email= result.Value.EmailId, EmployeeId = result.Value.EmployeeId});
        }
        /*[HttpDelete]
        [Route("{EmployeeId}")]

        public async Task<ActionResult<Employee>> deleteEmployee([FromRoute] string EmployeeId)
        {
           var result= await employeeServices.deleteEmployee(EmployeeId);
            return result.Value;

        }*/
    } 
}

