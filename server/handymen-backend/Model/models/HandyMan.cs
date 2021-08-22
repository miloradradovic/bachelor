using System.Collections.Generic;
using System.Linq;
using Model.dto;

namespace Model.models
{
    public class HandyMan : Person
    {
        public virtual List<Trade> Trades { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<Job> DoneJobs { get; set; }
        public Location Address { get; set; }
        
        public double Radius { get; set; }
        public virtual List<Offer> Offers { get; set; }

        public DetailedHandymanDTO ToDetailedHandymanDTO()
        {
            return new DetailedHandymanDTO()
            {
                Address = Address.ToLocationDTO(),
                AvgRating = CalculateAverageRate(),
                Email = Email,
                Id = Id,
                Name = FirstName + " " + LastName,
                Radius = Radius,
                Ratings = ManageRatings(),
                Trades = TradesToString()
            };
        }

        public List<RatingDTO> ManageRatings()
        {
            List<RatingDTO> dtos = new List<RatingDTO>();
            foreach (Rating rating in Ratings)
            {
                dtos.Add(new RatingDTO()
                {
                    Description = rating.Description,
                    Id = rating.Id,
                    JobId = rating.RatedJob.Id,
                    Rate = rating.Rate,
                    UserEmail = rating.RatedJob.User.Email,
                    PublishedDate = rating.PublishedDate
                });
            }

            return dtos;
        }
        
        public HandymanDTO ToDtoWithTrades()
        {
            return new HandymanDTO()
            {
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Password = Password,
                Role = Role.ToString(),
                Trades = TradesToString(),
                Verified = Verified
            };
        }

        public HandymanDTO ToDtoWithoutLists()
        {
            return new HandymanDTO()
            {
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Password = Password,
                Role = Role.ToString(),
                Verified = Verified
            };
        }

        public HandymanDashboardDTO ToDahboardDTO()
        {
            return new HandymanDashboardDTO()
            {
                Address = Address.Name,
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName
            };
        }
        
        private List<string> TradesToString()
        {
            List<string> trades = new List<string>();
            
            foreach (Trade trade in Trades)
            {
                trades.Add(trade.Name);
            }

            return trades;
        }

        public double CalculateAverageRate()
        {
            int sum = 0;
            foreach (Rating rating in Ratings)
            {
                sum = sum + rating.Rate;
            }

            if (Ratings.Count == 0)
            {
                return 0.0;
            }
            return sum / Ratings.Count;
            
        }
    }
}