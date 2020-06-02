using config_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.Repositories.Abstractions
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeData(string userId);
    }
}
