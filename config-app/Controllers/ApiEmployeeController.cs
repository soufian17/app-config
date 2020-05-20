using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using config_app.Repositories.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace config_app.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/employee")]
    public class ApiEmployeeController : Controller
    {
        public ApiEmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        private readonly IEmployeeRepository _employeeRepo;

        [HttpGet]
        [Route("{userId}")]
        public string GetEmployeeId(string userId)
        {
            return _employeeRepo.GetEmployeeId(userId);
        }
    }
}