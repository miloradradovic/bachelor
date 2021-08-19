using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface ILocationService
    {
        public Location GetByLatAndLng(double lat, double lng);
        public ApiResponse GetByUser(User user);
    }
    
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Location GetByLatAndLng(double lat, double lng)
        {
            return _locationRepository.GetByLatAndLng(lat, lng);
        }

        public ApiResponse GetByUser(User user)
        {
            return new ApiResponse()
            {
                Message = "Successfully fetched location by user.",
                ResponseObject = user.Address.ToLocationDTO(),
                Status = 200
            };
        }
    }
}