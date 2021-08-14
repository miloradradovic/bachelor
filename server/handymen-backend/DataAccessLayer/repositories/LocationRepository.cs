using System.Linq;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface ILocationRepository
    {
        public Location GetByLatAndLng(double lat, double lng);
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
    }
}