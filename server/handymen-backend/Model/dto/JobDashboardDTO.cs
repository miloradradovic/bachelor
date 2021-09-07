namespace Model.dto
{
    public class JobDashboardDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HandymanFirstName { get; set; }
        public string HandymanLastName { get; set; }
        public string HandymanEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string Finished { get; set; }
        public bool Rated { get; set; }
    }
}