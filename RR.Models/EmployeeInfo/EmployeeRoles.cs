using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RR.Models.EmployeeInfo
{
    public class EmployeeRoles
    {
        public int Id { get; set; }
        public string EmpId { get; set; }

        public string RoleName { get; set; }

       
      
        

        [DisplayName("IdOfRole")]
        public int IdOfRole { get; set; }

        [ForeignKey("RoleId")]
        public Roles role { get; set; }
    }
}
