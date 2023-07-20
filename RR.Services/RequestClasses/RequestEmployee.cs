using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services.RequestClasses
{
    public class RequestEmployee
    {
        public string EmployeeId { get; set; }

        public string Name { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }

        public string Designation { get; set; }

        public List<int> Roles { get; set; }
    }
}
