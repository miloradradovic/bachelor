using System;
using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;
using Org.BouncyCastle.Crypto.Prng;

namespace BusinessLogicLayer.services
{

    public interface IJobAdService
    {
        public ApiResponse CreateJobAd(JobAd jobAd, List<string> Trades, User loggedIn);
        public ApiResponse GetJobAdsForCurrentHandyman(HandyMan handyMan);
        public JobAd GetById(int id);
        public ApiResponse GetJobAdsByUser(User user);
    }
    
    public class JobAdService : IJobAdService
    {

        private readonly IJobAdRepository _jobAdRepository;
        private readonly IPersonService _personService;
        private readonly ITradeService _tradeService;
        private readonly ILocationService _locationService;

        public JobAdService(IJobAdRepository jobAdRepository, IPersonService personService, ITradeService tradeService, 
            ILocationService locationService)
        {
            _jobAdRepository = jobAdRepository;
            _personService = personService;
            _tradeService = tradeService;
            _locationService = locationService;
        }

        public ApiResponse CreateJobAd(JobAd jobAd, List<string> trades, User loggedIn)
        {
            jobAd.Owner = loggedIn;
            Location foundLocation = _locationService.GetByLatAndLng(jobAd.Address.Latitude, jobAd.Address.Longitude);
            if (foundLocation != null)
            {
                jobAd.Address = foundLocation;
            }
            
            foreach (string trade in trades)
            {
                Trade found = _tradeService.GetByName(trade);
                if (found == null)
                {
                    return new ApiResponse()
                    {
                        Message = "Something is wrong with one of the trades name.",
                        ResponseObject = null,
                        Status = 400
                    };
                }

                try
                {
                    jobAd.Trades.Add(found);
                }
                catch
                {
                    jobAd.Trades = new List<Trade>();
                    jobAd.Trades.Add(found);
                }
            }

            JobAd created = _jobAdRepository.CreateJobAd(jobAd);

            if (created == null)
            {
                return new ApiResponse()
                {
                    Message = "Something went wrong with the database while creating job ad. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            return new ApiResponse()
            {
                Message = "Successfully created job ad.",
                ResponseObject = created.ToJobAdDTO(),
                Status = 201
            };
        }

        public ApiResponse GetJobAdsForCurrentHandyman(HandyMan handyMan)
        {
            List<JobAd> jobAds = _jobAdRepository.GetAll();
            List<JobAd> fullJobAds = new List<JobAd>();
            List<JobAdDashboardDTO> jobAdDtos = new List<JobAdDashboardDTO>();
            foreach (JobAd jobAd in jobAds)
            {
                fullJobAds.Add(_jobAdRepository.GetById(jobAd.Id));
            }

            foreach (JobAd ad in fullJobAds)
            {
                if (CheckTrades(handyMan.Trades, ad.Trades) && DetermineCircle(handyMan.Address, handyMan.Radius, ad.Address)
                && !InterestExists(ad.Id, handyMan.Id) && !ExistsJob(ad.Id, handyMan.Id))
                {
                    jobAdDtos.Add(ad.ToJobAdDashboardDTO());
                }
            }

            return new ApiResponse()
            {
                Message = "Successfully fetched job ads by handyman.",
                ResponseObject = jobAdDtos,
                Status = 200
            };
        }

        private bool ExistsJob(int jobAd, int handyman)
        {
            Job found = _jobAdRepository.GetJobByJobAdAndHandyman(jobAd, handyman);
            if (found == null)
            {
                return false;
            }

            return true;
        }

        private bool InterestExists(int jobAd, int handyman)
        {
            Interest found = _jobAdRepository.InterestExists(jobAd, handyman);
            if (found == null)
            {
                return false;
            }

            return true;
        }

        private bool CheckTrades(List<Trade> handymanTrades, List<Trade> adTrades)
        {
            foreach (Trade adTrade in adTrades)
            {
                bool check = false;
                foreach (Trade handymanTrade in handymanTrades)
                {
                    if (handymanTrade.Name == adTrade.Name)
                    {
                        check = true;
                        break;
                    }
                }

                if (!check)
                {
                    return false;
                }
            }

            return true;
        }

        private bool DetermineCircle(Location handymanAddress, double handymanRadius, Location adAddress)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(handymanAddress.Latitude - adAddress.Latitude), 2) +
                             Math.Pow(Math.Abs(handymanRadius - adAddress.Longitude), 2)) <= handymanRadius;
        }

        public JobAd GetById(int id)
        {
            return _jobAdRepository.GetById(id);
        }

        public ApiResponse GetJobAdsByUser(User user)
        {
            List<JobAd> usersJobAds = user.JobAds;
            List<JobAdDashboardDTO> jobAdDtos = new List<JobAdDashboardDTO>();
            foreach (JobAd jobAd in usersJobAds)
            {
                jobAdDtos.Add(_jobAdRepository.GetById(jobAd.Id).ToJobAdDashboardDTO());
            }

            return new ApiResponse()
            {
                Message = "Successfully fetched job ads by user.",
                ResponseObject = jobAdDtos,
                Status = 200
            };
        }
    }
}