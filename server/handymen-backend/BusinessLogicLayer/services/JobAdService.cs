using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IJobAdService
    {
        public ApiResponse CreateJobAd(JobAd jobAd, List<string> Trades, User loggedIn);
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
            jobAd.Owner = _personService.GetUserById(loggedIn.Id);

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
    }
}