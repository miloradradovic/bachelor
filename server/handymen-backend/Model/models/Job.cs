namespace Model.models
{
    public class Job
    {
        public int Id { get; set; }
        public JobAd JobAd { get; set; }
        public HandyMan HandyMan { get; set; }
        public bool Finished { get; set; }
    }
}