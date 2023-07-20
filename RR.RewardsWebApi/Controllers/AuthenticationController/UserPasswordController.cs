using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RR.DataBaseConnect;
using RR.Models.EmployeeInfo;
using RR.Services;
using RR.Services.RequestClasses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RR.RewardsWebApi.Controllers.AuthenticationController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPasswordController : ControllerBase
    {
        private readonly DataBaseAccess dataBaseAccess;
        public EmployeeServices EmployeeServices { get; set; }

        private readonly IConfiguration configuration;
        public UserPasswordController(DataBaseAccess dataBaseAccess,IConfiguration configuration)
        {
            this.dataBaseAccess = dataBaseAccess;
            this.EmployeeServices = new EmployeeServices();
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> login(RequestUserIdAndPassword requestUserIdAndPassword)
        {
            
            UserNamePassword userNamePassword = await dataBaseAccess.UserNamePassword
                .FirstOrDefaultAsync(x => x.EmailID.Equals(requestUserIdAndPassword.EmailID));

            if(userNamePassword == null)
            {
                return BadRequest("Null");
            }
            if (userNamePassword.EmailID != requestUserIdAndPassword.EmailID)
            {
                return BadRequest("UserId not FOund");
            }
            if (!BCrypt.Net.BCrypt.Verify(requestUserIdAndPassword.Password, userNamePassword.Password))
            {
                return BadRequest("WrongPassword");
            }
            List<EmployeeRoles> roles = dataBaseAccess.EmployeeRoles.Where(x => x.EmpId == userNamePassword.employeeId).ToList();

            string token = createToken(userNamePassword,roles);

            LoginResponse loginResponse = new LoginResponse();
            loginResponse.employeeId=userNamePassword.employeeId;
            loginResponse.roles = roles;
            /*loginResponse.roles.Select(x =>  { Id = x.IdOfRole, Name = x.RoleName });*/

            loginResponse.Token = token;





            return Ok(loginResponse);


        }
        private  string createToken(UserNamePassword userNamePassword, List<EmployeeRoles> roles)
        {

            List<Claim> claims = new List<Claim>
             {

                 new Claim(ClaimTypes.Name,userNamePassword.EmailID),



             };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(
               configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred




                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;








        }
    }
}
