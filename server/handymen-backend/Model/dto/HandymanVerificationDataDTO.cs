using Model.models;

namespace Model.dto
{
    public class HandymanVerificationDataDTO
    {
        public int Id { get; set; }
        public bool Verify { get; set; }
        public string Message { get; set; }

        public HandymanVerificationData ToHandymanVerificationData()
        {
            return new HandymanVerificationData()
            {
                Id = Id,
                Message = Message,
                Verify = Verify
            };
        }
    }
}