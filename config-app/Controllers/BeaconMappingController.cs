using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using config_app.Repositories;
using config_app.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace config_app.Controllers
{
    [ApiController]
    [Route("beacon-mapping")]
    public class BeaconMappingController : ControllerBase
    {
        public BeaconMappingController(IBeaconRepository beaconRepo)
        {
            _beaconRepo = beaconRepo;
        }

        private readonly IBeaconRepository _beaconRepo;

        [HttpGet]
        public IEnumerable<BeaconMapping> Get()
        {
            return _beaconRepo.GetAllBeaconMappings();
        }
    }
}