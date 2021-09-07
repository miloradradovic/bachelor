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

        public JobDashboardDTO ToJobDashboardDTO(bool rated)
        {
            return new JobDashboardDTO()
            {
                Finished = ManageFinished(),
                HandymanEmail = HandyMan.Email,
                HandymanFirstName = HandyMan.FirstName,
                HandymanLastName = HandyMan.LastName,
                Id = Id,
                Title = JobAd.Title,
                UserEmail = User.Email,
                UserFirstName = User.FirstName,
                UserLastName = User.LastName,
                Rated = rated
            };
        }

        private string ManageFinished()
        {
            if (Finished)
            {
                return "Da";
            }

            return "Ne";
        }
    }
}