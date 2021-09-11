using System;
using Model.dto;

namespace Model.models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Description { get; set; }
        public Job RatedJob { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Verified { get; set; }

        public RatingDTO ToRatingDTO(int jobId)
        {
            return new RatingDTO()
            {
                Id = Id,
                Rate = Rate,
                Description = Description,
                JobId =  jobId
            };
        }
    }
}