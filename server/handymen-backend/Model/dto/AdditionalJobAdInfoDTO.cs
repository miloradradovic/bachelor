using Model.models;

namespace Model.dto
{
    public class AdditionalJobAdInfoDTO
    {
        public int Id { get; set; }
        public bool Urgent { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }

        public AdditionalJobAdInfo ToAdditionalJobAdInfo()
        {
            return new AdditionalJobAdInfo()
            {
                Id = Id,
                PriceFrom = PriceFrom,
                PriceTo = PriceTo,
                Urgent = Urgent
            };
        }
    }
}