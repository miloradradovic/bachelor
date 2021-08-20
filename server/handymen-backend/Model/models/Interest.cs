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
    }
}