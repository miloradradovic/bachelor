using Model.models;

namespace Model.dto
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        
        public int Radius { get; set; }

        public Location ToLocation()
        {
            return new Location()
            {
                Id = Id,
                Latitude = Latitude,
                Longitude = Longitude,
                Name = Name
            };
        }
    }
}