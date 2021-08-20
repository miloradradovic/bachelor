using Model.models;

namespace Model.dto
{
    public class InterestDTO
    {
        public int Id { get; set; }
        public int JobAdId { get; set; }
        public int DaysEstimated { get; set; }
        public double PriceEstimated { get; set; }

        public Interest ToInterest()
        {
            return new Interest()
            {
                Id = Id,
                DaysEstimated = DaysEstimated,
                PriceEstimated = PriceEstimated
            };
        }
    }
}