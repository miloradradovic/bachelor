using Model.dto;

namespace Model.models
{
    public class AdditionalJobAdInfo
    {
        public int Id { get; set; }
        public bool Urgent { get; set; }
        public double PriceMax { get; set; }

        public AdditionalJobAdInfoDTO ToAdditionalJobAdInfoDTO()
        {
            return new AdditionalJobAdInfoDTO()
            {
                Id = Id,
                Urgent = Urgent,
                PriceMax = PriceMax
            };
        }
    }
}