using System;

namespace Model.dto
{
    public class JobAdDashboardDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double MaxPrice { get; set; }
        public string Urgent { get; set; }
        public DateTime DateWhen { get; set; }
    }
}