using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RR.DataBaseConnect;
using RR.Models.EmployeeInfo;
using RR.Services.RequestClasses;

namespace RR.Services
{
    public class EmployeeServices
    {
        private readonly DataBaseAccess dataBaseAccess;

        public EmployeeServices()
        {

        }

        public EmployeeServices(DataBaseAccess dataBaseAccess)
        {
            this.dataBaseAccess = dataBaseAccess;
        }

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeAsync()
        {
            var result = await dataBaseAccess.Employee.Include(x=>x.UserNamePassword).Include(x=>x.Roles).ToListAsync();

         

            return result;
        }

        public async Task<ActionResult<Employee>> AddEmployee(RequestEmployee requestEmployee)
        {
            Employee employee = new Employee();
            employee.EmployeeId = requestEmployee.EmployeeId;

            employee.Name = requestEmployee.Name;
          employee.EmailId = requestEmployee.EmailId;
          //  employee.Password = requestEmployee.Password;
            employee.Designation = requestEmployee.Designation;


            UserNamePassword UserNamePassword = new UserNamePassword();
          
            UserNamePassword.EmailID = requestEmployee.EmailId;
            string passwordHash =
             BCrypt.Net.BCrypt.HashPassword(requestEmployee.Password);
            UserNamePassword.Password = passwordHash;
           // UserNamePassword.Password = requestEmployee.Password;
            UserNamePassword.employeeId = requestEmployee.EmployeeId;
            employee.UserNamePassword = UserNamePassword;



            foreach (var roleID in requestEmployee.Roles)
            {
                EmployeeRoles employeeRole = new EmployeeRoles();


                employeeRole.IdOfRole = roleID;

                employeeRole.role = await dataBaseAccess.Roles.FindAsync(roleID);

                employeeRole.RoleName = employeeRole.role.RoleName;

                employeeRole.EmpId = requestEmployee.EmployeeId;
              
                //

                // EmployeeId column in employeeRole will not be null 
                employee.Roles.Add(employeeRole);

                //
                await dataBaseAccess.EmployeeRoles.AddAsync(employeeRole);


            }
            await dataBaseAccess.Employee.AddAsync(employee);

            await dataBaseAccess.SaveChangesAsync();

            return employee;
        }
        // update Employee 

        public async Task<ActionResult<Employee>> updateEmployee(RequestEmployee requestEmployee)
        {

            Employee employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x=>x.EmployeeId==requestEmployee.EmployeeId);
            if(employee == null)
            {
                return null;
            }
            employee.EmployeeId = requestEmployee.EmployeeId;

            employee.Name = requestEmployee.Name;
             employee.EmailId = requestEmployee.EmailId;
            //  employee.Password = requestEmployee.Password;
            employee.Designation = requestEmployee.Designation;


            UserNamePassword UserNamePassword = await dataBaseAccess.UserNamePassword.FirstOrDefaultAsync(x => x.employeeId.Equals(requestEmployee.EmployeeId));

            UserNamePassword.EmailID = requestEmployee.EmailId;

            string passwordHash =
             BCrypt.Net.BCrypt.HashPassword(requestEmployee.Password);
            UserNamePassword.Password = passwordHash;


            UserNamePassword.employeeId = requestEmployee.EmployeeId;

            employee.UserNamePassword = UserNamePassword;

            List<EmployeeRoles> roles = await dataBaseAccess.EmployeeRoles.Where(x => x.EmpId == requestEmployee.EmployeeId).ToListAsync();
            List<EmployeeRoles> deleteRolesNotGivenByTheUser= new List<EmployeeRoles>();

            
            // FOr checking the exisiting and matched role
            foreach (EmployeeRoles role in roles)
            {
                if(requestEmployee.Roles.Contains(role.IdOfRole))
                {
                    
                
                    requestEmployee.Roles.Remove(role.IdOfRole);

                }
                else
                {
                    deleteRolesNotGivenByTheUser.Add(role);
                }
                
            }

            // For removing the roles specified by the admin 
            foreach(EmployeeRoles employeeRoles in deleteRolesNotGivenByTheUser)
            {
                EmployeeRoles employeeRoles1 = employeeRoles;
                  dataBaseAccess.EmployeeRoles.Remove(employeeRoles1);

                await dataBaseAccess.SaveChangesAsync();
            }
            // Add the new  role
            foreach(var role in requestEmployee.Roles)
            {
                EmployeeRoles employeeRole = new EmployeeRoles();
                employeeRole.IdOfRole = role;

                employeeRole.role = await dataBaseAccess.Roles.FindAsync(role);

                employeeRole.RoleName = employeeRole.role.RoleName;

                employeeRole.EmpId = requestEmployee.EmployeeId;


                // EmployeeId column in employeeRole will not be null 
                employee.Roles.Add(employeeRole);


                await dataBaseAccess.EmployeeRoles.AddAsync(employeeRole);
            }
             dataBaseAccess.Employee.Update(employee);
            await dataBaseAccess.SaveChangesAsync();

            return employee;



        }
        // getAll Roles

        public async Task<ActionResult<IEnumerable<EmployeeRoles>>> getAllEmpRoles()
        {
            return await dataBaseAccess.EmployeeRoles.ToListAsync();
        }

        // add Role to the employee

        public async Task<ActionResult<EmployeeRoles>> addRoleToEmployee(RequestRole requestrole)
        {


            EmployeeRoles employeeRoles = new EmployeeRoles();

            employeeRoles.IdOfRole = requestrole.rId;
            employeeRoles.EmpId = requestrole.employeeId;
            employeeRoles.role = await dataBaseAccess.Roles.FindAsync(employeeRoles.IdOfRole);
            employeeRoles.RoleName= employeeRoles.role.RoleName;

            await dataBaseAccess.EmployeeRoles.AddAsync(employeeRoles);

            await dataBaseAccess.SaveChangesAsync();

            return employeeRoles;
        }

        public async Task<ActionResult<EmployeeRoles>> deleteRole(string empId , int roleId)
        {
            EmployeeRoles employeeRoles=await dataBaseAccess.EmployeeRoles.FirstOrDefaultAsync(x=>x.IdOfRole==roleId &&  x.EmpId.Equals(empId));

            if(employeeRoles==null)
            {
                return null;
            }
            else
            {
                 dataBaseAccess.EmployeeRoles.Remove(employeeRoles);
               await  dataBaseAccess.SaveChangesAsync();

                return employeeRoles;
            }
        }

        // delete Employee
        public async Task<ActionResult<Employee>> deleteEmployee(string employeeId)
        {
            Employee employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x=>x.EmployeeId.Equals(employeeId));   


            if (employee == null)
            {
                return null;
            }
            else
            {
                dataBaseAccess.Employee.Remove(employee);
                await dataBaseAccess.SaveChangesAsync();

                return employee;
            }
        }

        public async Task<ActionResult<Employee>> getEmployeeById(string employeeId)
        {
            Employee employee = await dataBaseAccess.Employee.FirstOrDefaultAsync(x => x.EmployeeId.Equals(employeeId));

            return employee;
        }


    }
}