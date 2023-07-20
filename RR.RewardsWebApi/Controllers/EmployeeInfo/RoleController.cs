using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RR.DataBaseConnect;
using RR.Models.EmployeeInfo;

namespace RR.RewardsWebApi.Controllers.EmployeeInfo
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController:ControllerBase
    {
        private readonly DataBaseAccess dataBaseAccess;
        public RoleController(DataBaseAccess dataBaseAccess)
        {
            this.dataBaseAccess = dataBaseAccess;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRole()
        {

            return await dataBaseAccess.Roles.ToListAsync();

        }

        [HttpPost]
        public async Task<ActionResult<Roles>> AddRole(Roles role)
        {
            await dataBaseAccess.Roles.AddAsync(role);
            await dataBaseAccess.SaveChangesAsync();

            return Ok(role);
        }

    }
}
