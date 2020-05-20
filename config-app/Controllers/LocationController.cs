using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using config_app.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace config_app.Controllers
{
    [Route("location")]
    public class LocationController : Controller
    {
        public LocationController(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }
        private readonly ILocationRepository _locationRepo;

        [HttpGet]
        public IActionResult Index()
        {
            return View(_locationRepo.GetAllLocations());
        }
    }
}