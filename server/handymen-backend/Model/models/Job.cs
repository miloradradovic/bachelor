using Model.dto;

namespace Model.models
{
    public class Job
    {
        public int Id { get; set; }
        public JobAd JobAd { get; set; }
        public HandyMan HandyMan { get; set; }
        public bool Finished { get; set; }
        public User User { get; set; }

        public JobDTO ToJobDTO()
        {
            return new JobDTO()
            {
                Id = Id,
                Finished = Finished
            };
        }
    }
}