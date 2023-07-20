using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services.RequestClasses
{
    public  class RequestUserIdAndPassword
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
    }
}
