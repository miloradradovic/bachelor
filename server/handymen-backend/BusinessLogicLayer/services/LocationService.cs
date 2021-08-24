using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface ILocationService
    {
        public Location GetByLatAndLng(double lat, double lng);
        public ApiResponse GetByUser(User user);
        public List<Location> GetLocationsByName(string name);
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
                Message = "Uspesno dobavljena lokacija za korisnika.",
                ResponseObject = user.Address.ToLocationDTO(),
                Status = 200
            };
        }

        public List<Location> GetLocationsByName(string name)
        {
            return _locationRepository.GetByName(name);
        }
    }
}