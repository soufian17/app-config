using config_app.Models;
using config_app.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace config_app.Controllers
{
    [ApiController]
    [Route("location")]
    public class LocationController : ControllerBase
    {
        public LocationController(ILocationRepository locationRepo)
        {
            _locationRepo = locationRepo;
        }

        private readonly ILocationRepository _locationRepo;

        [HttpPost]
        public LastLocation Post(LastLocation location)
        {
            _locationRepo.SetLastLocation(location);
            return location;
        }
        [HttpGet]
        public IEnumerable<LastLocation> Get()
        {
            return _locationRepo.GetAllLocations();
        }
    }
}
