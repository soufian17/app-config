using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using config_app.Repositories.Abstractions;

namespace config_app.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/visitor")]
    public class ApiVisitorController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;

        public ApiVisitorController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpPost]
        [Route("{userId}")]
        public void Post(string userId)
        {
            _employeeRepo.AddVisitorAccount(userId, DateTime.Now.AddDays(1));
        }

    }
}
