using config_app.Models;
using System.Collections.Generic;

namespace config_app.Repositories.Abstractions
{
    public interface ILocationRepository
    {
        void SetLastLocation(LastLocation location);
        IEnumerable<LastLocation> GetAllLocations();
    }
}