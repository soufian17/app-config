using config_app.DAL;
using config_app.Models;
using config_app.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository(ConfigAppContext context)
        {
            _context = context;
        }

        private readonly ConfigAppContext _context;

        public Employee GetEmployeeData(string userId)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.UserId == userId);

            if (employee != null)
            {
                if (employee.ValidUntil < DateTime.Now)
                {
                    return null;
                }
                else
                {
                    return employee;

                }
            }
            else
            {
                var newEmployee = new Employee { ValidUntil = DateTime.MaxValue, Role = EmployeeRole.Employee, UserId = userId, EmployeeId = Employee.GenerateEmployeeId() };
                _context.Add(newEmployee);
                _context.SaveChanges();
                return newEmployee;
            }

        }

        public void AddVisitorAccount(string userId, DateTime expiryDate)
        {
            _context.Employees.Add(new Employee
            {
                UserId = userId,
                EmployeeId = Employee.GenerateEmployeeId(),
                Role = EmployeeRole.Visitor,
                ValidUntil = expiryDate
            });
            _context.SaveChanges();
        }
    }
}
