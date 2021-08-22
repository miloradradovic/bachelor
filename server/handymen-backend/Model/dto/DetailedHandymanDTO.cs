using System.Collections.Generic;

namespace Model.dto
{
    public class DetailedHandymanDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public LocationDTO Address { get; set; }
        public double Radius { get; set; }
        public double AvgRating { get; set; }
        public List<string> Trades { get; set; }
        public List<RatingDTO> Ratings { get; set; }
    }
}