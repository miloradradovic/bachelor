using Model.dto;

namespace Model.models
{
    public class Location
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }

        public LocationDTO ToLocationDTO()
        {
            return new LocationDTO()
            {
                Id = Id,
                Latitude = Latitude,
                Longitude = Longitude,
                Name = Name
            };
        }
    }
}