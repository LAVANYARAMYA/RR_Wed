using System.ComponentModel.DataAnnotations.Schema;

namespace RR.Models.EmployeeInfo
{
    public class Employee
    {
        public int Id { get; set; }


        public string EmployeeId { get; set; }

        public string Name { get; set; }

        public string EmailId { get; set; }

      

        public string Designation { get; set; }

      

        [ForeignKey("UserPassId")]
        public UserNamePassword UserNamePassword { get; set; }

        public virtual ICollection<EmployeeRoles> Roles { get; set; } = new List<EmployeeRoles>();
    }
}