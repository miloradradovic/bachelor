using System;
using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public Object ResponseObject { get; set; }
        public int Status { get; set; }

        private void SetMessageAndStatus(string message, int status)
        {
            Message = message;
            Status = status;
        }
        
        public void SetError(string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = null;
        }
        
        public void CreatedAdministrator(Administrator administrator, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = administrator.ToDto();
        }

        public void LoggedIn(string token, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = token;
        }

        public void GotCategories(List<Category> categories, string message, int status)
        {
            SetMessageAndStatus(message, status);
            List<CategoryDTO> dtos = new List<CategoryDTO>();
            foreach (Category category in categories)
            {
                dtos.Add(category.ToCategoryDTO());
            }

            ResponseObject = dtos;
        }

        public void GotHandymenDashboard(List<HandyMan> handymen, string message, int status)
        {
            SetMessageAndStatus(message, status);
            List<HandymanDashboardDTO> handymanDashboardDtos = new List<HandymanDashboardDTO>();
            foreach (HandyMan handy in handymen)
            {
                handymanDashboardDtos.Add(handy.ToDahboardDTO());
            }

            ResponseObject = handymanDashboardDtos;
        }

        public void RegisteredHandyman(HandyMan handyman, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = handyman.ToDtoWithTrades();
        }

        public void VerifiedHandyman(HandyMan handyman, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = handyman.ToDtoWithoutLists();
        }

        public void UpdatedHandymanProfile(HandyMan handyman, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = handyman.ToProfileDataDTO();
        }

        public void GotRegistrationRequests(List<HandyMan> handymen, string message, int status)
        {
            SetMessageAndStatus(message, status);
            List<RegistrationRequestDataDTO> result = new List<RegistrationRequestDataDTO>();
            foreach (HandyMan handyman in handymen)
            {
                result.Add(handyman.ToRegistrationRequestDataDTO());
            }

            ResponseObject = result;
        }

        public void CreatedInterest(Interest interest, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = interest.ToInterestDTO();
        }

        public void GotInterestDashboard(Interest interest, JobAd jobAd, int index)
        {
            if (index == 0)
            {
                List<InterestDashboardDTO> result = new List<InterestDashboardDTO>();
                result.Add(interest.ToInterestDashboardDTO(jobAd.ToJobAdDashboardDTO()));
                ResponseObject = result;
            }
            else
            {
                List<InterestDashboardDTO> result = (List<InterestDashboardDTO>) ResponseObject;
                result.Add(interest.ToInterestDashboardDTO(jobAd.ToJobAdDashboardDTO()));
                ResponseObject = result;
            }
        }

        public void CreatedJobAd(JobAd jobAd, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = jobAd.ToJobAdDTO();
        }

        public void GotJobAdDashboard(List<JobAd> jobAds, string message, int status)
        {
            SetMessageAndStatus(message, status);
            List<JobAdDashboardDTO> jobAdDtos = new List<JobAdDashboardDTO>();
            foreach (JobAd ad in jobAds)
            {
                jobAdDtos.Add(ad.ToJobAdDashboardDTO());
            }

            ResponseObject = jobAdDtos;
        }

        public void CreatedJob(Job job, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = job.ToJobDTO();
        }

        public void FinishedJob(Job job, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = job.ToJobDTO();
        }
        
        public void GotJobDashboard(Job job, bool rated, int index)
        {
            if (index == 0)
            {
                List<JobDashboardDTO> result = new List<JobDashboardDTO>();
                result.Add(job.ToJobDashboardDTO(rated));
                ResponseObject = result;
            }
            else
            {
                List<JobDashboardDTO> result = (List<JobDashboardDTO>) ResponseObject;
                result.Add(job.ToJobDashboardDTO(rated));
                ResponseObject = result;
            }
        }

        public void GotLocation(Location location, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = location.ToLocationDTO();
        }

        public void CreatedOffer(Offer offer, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = new OfferDTO()
            {
                Id = offer.Id
            };
        }

        public void GotDetailedHandyman(HandyMan handyman, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = handyman.ToDetailedHandymanDTO();
        }

        public void GotProfessions(List<Profession> professions, string message, int status)
        {
            SetMessageAndStatus(message, status);
            List<ProfessionDTO> dtos = new List<ProfessionDTO>();

            foreach (Profession profession in professions)
            {
                dtos.Add(profession.ToProfessionDTO());
            }

            ResponseObject = dtos;
        }

        public void DeletedRating(Rating rating, int jobId, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = rating.ToRatingDTO(jobId);
        }

        public void VerifiedRating(Rating rating, int jobId, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = rating.ToRatingDTO(jobId);
        }

        public void CreatedRating(Rating rating, int jobId, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = rating.ToRatingDTO(jobId);
        }

        public void GotRatings(List<Rating> ratings, string message, int status)
        {
            SetMessageAndStatus(message, status);
            List<RatingDTO> dtos = new List<RatingDTO>();
            foreach (Rating rating in ratings)
            {
                dtos.Add(new RatingDTO()
                {
                    Description = rating.Description,
                    Id = rating.Id,
                    Rate = rating.Rate,
                    PublishedDate = rating.PublishedDate
                });
            }

            ResponseObject = dtos;
        }

        public void GotTrades(List<Trade> trades, string message, int status)
        {
            SetMessageAndStatus(message, status);
            List<TradeDTO> tradeDtos = new List<TradeDTO>();
            foreach (Trade trade in trades)
            {
                tradeDtos.Add(trade.ToTradeDTO());
            }

            ResponseObject = tradeDtos;
        }

        public void GotCategoryProfession(Profession profession, Category category, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = new HandymanCategoryProfessionDTO()
            {
                CategoryDto = category.ToCategoryDTO(),
                ProfessionDto = profession.ToProfessionDTO()
            };
        }

        public void RegisteredUser(User user, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = user.ToDtoWithoutLists();
        }

        public void VerifiedUser(User user, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = user.ToDtoWithoutLists();
        }

        public void UpdatedUserProfile(User user, string message, int status)
        {
            SetMessageAndStatus(message, status);
            ResponseObject = user.ToProfileDataDTO();
        }
        
        
    }
}