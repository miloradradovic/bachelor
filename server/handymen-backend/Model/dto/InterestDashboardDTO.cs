using System;

namespace Model.dto
{
    public class InterestDashboardDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double MaxPrice { get; set; }
        public bool Urgent { get; set; }
        public DateTime DateWhen { get; set; }
        
        //handyman data
        public int HandymanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double EstimatedPrice { get; set; }
        public int EstimatedDays { get; set; }
    }
}