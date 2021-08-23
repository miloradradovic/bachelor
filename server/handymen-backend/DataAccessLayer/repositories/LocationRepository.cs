using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface ILocationRepository
    {
        public Location GetByLatAndLng(double lat, double lng);
        public List<Location> GetByName(string name);
    }
    
    public class LocationRepository : ILocationRepository
    {
        private readonly PostgreSqlContext _context;

        public LocationRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public Location GetByLatAndLng(double lat, double lng)
        {
            Location found =
                _context.Locations.SingleOrDefault(location => location.Latitude == lat && location.Longitude == lng);
            return found;
        }

        public List<Location> GetByName(string name)
        {
            List<Location> locations = _context.Locations
                .Where(location => location.Name.Contains(name))
                .ToList();
            return locations;
        }
    }
}