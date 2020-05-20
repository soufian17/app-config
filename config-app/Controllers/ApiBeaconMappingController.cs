using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using config_app.Repositories;
using config_app.Repositories.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace config_app.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/beacon-mapping")]
    public class ApiBeaconMappingController : Controller
    {
        public ApiBeaconMappingController(IBeaconRepository beaconRepo)
        {
            _beaconRepo = beaconRepo;
        }

        private readonly IBeaconRepository _beaconRepo;

        [HttpGet]
        public IEnumerable<BeaconMapping> Get()
        {

            return _beaconRepo.GetAllBeaconMappings();
        }
        [HttpGet]
        [Route("{proximityUuid}/{minor}/{major}")]
        public BeaconMapping Get(string proximityUuid, int minor, int major)
        {
            return _beaconRepo.GetBeaconMapping(proximityUuid, minor, major);
        }
        [HttpGet]
        [Route("{connectableName}")]
        public string GetReadableName(string connectableName)
        {
            return _beaconRepo.GetReadableLocationName(connectableName);
        }
    }
}
