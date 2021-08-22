using System;
using Model.models;

namespace Model.dto
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
        public int JobId { get; set; }
        public string UserEmail { get; set; }
        public DateTime PublishedDate { get; set; }

        public Rating ToRating()
        {
            return new Rating()
            {
                Id = Id,
                Description = Description,
                Rate = Rate
            };
        }
    }
}