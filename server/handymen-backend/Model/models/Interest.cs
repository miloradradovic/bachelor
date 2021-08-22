using Model.dto;

namespace Model.models
{
    public class Interest
    {
        public int Id { get; set; }
        public JobAd JobAd { get; set; }
        public HandyMan HandyMan { get; set; }
        public int DaysEstimated { get; set; }
        public double PriceEstimated { get; set; }
        
        public InterestDTO ToInterestDTO()
        {
            return new InterestDTO()
            {
                Id = Id,
                JobAdId = JobAd.Id,
                DaysEstimated = DaysEstimated,
                PriceEstimated = PriceEstimated
            };
        }

        public InterestDashboardDTO ToInterestDashboardDTO(JobAdDashboardDTO jobAdDashboardDto)
        {
            return new InterestDashboardDTO()
            {
                Address = jobAdDashboardDto.Address,
                DateWhen = jobAdDashboardDto.DateWhen,
                Description = jobAdDashboardDto.Description,
                Email = HandyMan.Email,
                EstimatedDays = DaysEstimated,
                EstimatedPrice = PriceEstimated,
                FirstName = HandyMan.FirstName,
                Id = Id,
                LastName = HandyMan.LastName,
                MaxPrice = jobAdDashboardDto.MaxPrice,
                Title = jobAdDashboardDto.Title,
                HandymanId = HandyMan.Id
            };
        }
    }
}