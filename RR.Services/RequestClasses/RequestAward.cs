using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Services.RequestClasses
{
    public  class RequestAward
    {
       

        public string Name { get; set; }

        [DisplayName("DisplayName")]
        public int IdOfReward { get; set; }
    }
}
