namespace Model.dto
{
    public class JobDTO
    {
        public int Id { get; set; }
        public JobAdDTO JobAd { get; set; }
        public HandymanDTO HandyMan { get; set; }
        public bool Finished { get; set; }
    }
}