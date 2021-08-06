using Model.models;

namespace Model.dto
{
    public class AdditionalJobAdInfoDTO
    {
        public int Id { get; set; }
        public bool Urgent { get; set; }
        public double PriceMax { get; set; }

        public AdditionalJobAdInfo ToAdditionalJobAdInfo()
        {
            return new AdditionalJobAdInfo()
            {
                Id = Id,
                PriceMax = PriceMax,
                Urgent = Urgent
            };
        }
    }
}