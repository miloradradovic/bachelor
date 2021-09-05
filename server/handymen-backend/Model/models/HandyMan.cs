using System;
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
        public double AverageRate { get; set; }
        public string City { get; set; }

        public RegistrationRequestDataDTO ToRegistrationRequestDataDTO()
        {
            return new RegistrationRequestDataDTO()
            {
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Location = Address.ToLocationDTO(),
                Radius = Radius
            };
        }
        
        public DetailedHandymanDTO ToDetailedHandymanDTO()
        {
            return new DetailedHandymanDTO()
            {
                Address = Address.ToLocationDTO(),
                AvgRating = AverageRate,
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

        public void CalculateAverageRate()
        {
            int sum = 0;
            foreach (Rating rating in Ratings)
            {
                sum = sum + rating.Rate;
            }

            if (Ratings.Count == 0)
            {
                AverageRate = 0.0;
            }
            AverageRate = sum / Ratings.Count;
            
        }

        public ProfileDataDTO ToProfileDataDTO()
        {
            return new ProfileDataDTO()
            {
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Location = new LocationDTO()
                {
                    Id = Address.Id,
                    Latitude = Address.Latitude,
                    Longitude = Address.Longitude,
                    Name = Address.Name,
                    Radius = Radius
                },
                Trades = TradesToString(),
                AverageRate = AverageRate
            };
        }
    }
}