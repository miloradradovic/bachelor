namespace Model.models
{
    public class JobAd
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Location Address { get; set; }
        public User Owner { get; set; }
        public AdditionalJobAdInfo AdditionalJobAdInfo { get; set; }
    }
}