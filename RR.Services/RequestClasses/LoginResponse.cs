using RR.Models.EmployeeInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services.RequestClasses
{
    public  class LoginResponse
    {
        public string employeeId { get; set; }

        public List<EmployeeRoles> roles { get; set; }

        public string Token { get; set; }
    }
}
