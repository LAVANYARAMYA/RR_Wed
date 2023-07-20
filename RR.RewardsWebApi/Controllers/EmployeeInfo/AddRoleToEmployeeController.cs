using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RR.DataBaseConnect;
using RR.Models.EmployeeInfo;
using RR.Services;
using RR.Services.RequestClasses;

namespace RR.RewardsWebApi.Controllers.EmployeeInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddRoleToEmployeeController : ControllerBase
    {

        public EmployeeServices EmployeeService;

        public AddRoleToEmployeeController(DataBaseAccess dataAccess)
        {


            EmployeeService = new EmployeeServices(dataAccess);


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeRoles>>> get()
        {
            return await EmployeeService.getAllEmpRoles();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeRoles>> add(RequestRole requestrole)
        {
            return await EmployeeService.addRoleToEmployee(requestrole);
        }

        [HttpDelete]
        [Route("{empId}/{roleId}")]
        public async Task<ActionResult<EmployeeRoles>> deleteRole(string empId, int roleId)
        {
            return await EmployeeService.deleteRole(empId, roleId);
        }

    }
}
