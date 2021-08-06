using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IJobAdService
    {
        public ApiResponse CreateJobAd(JobAd jobAd, List<string> Trades, User loggedIn);
        public ApiResponse GetJobAdsForCurrentHandyman(HandyMan handyMan);
        public JobAd GetById(int id);
    }
    
    public class JobAdService : IJobAdService
    {

        private readonly IJobAdRepository _jobAdRepository;
        private readonly IPersonService _personService;
        private readonly ITradeService _tradeService;

        public JobAdService(IJobAdRepository jobAdRepository, IPersonService personService, ITradeService tradeService)
        {
            _jobAdRepository = jobAdRepository;
            _personService = personService;
            _tradeService = tradeService;
        }

        public ApiResponse CreateJobAd(JobAd jobAd, List<string> trades, User loggedIn)
        {
            jobAd.Owner = loggedIn;

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
            HandyMan handyManWithTrades = _personService.GetHandymanById(handyMan.Id);
            
            return new ApiResponse()
            {
                Message = "Successfully retrieved job ads for current handyman.",
                ResponseObject = _jobAdRepository.GetJobAdsForCurrentHandyman(handyManWithTrades),
                Status = 200
            };
        }

        public JobAd GetById(int id)
        {
            return _jobAdRepository.GetById(id);
        }
    }
}