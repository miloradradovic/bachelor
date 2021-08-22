using System.Collections.Generic;
using DataAccessLayer.Migrations;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IInterestService
    {
        public ApiResponse MakeInterest(Interest interest, int jobAdId, HandyMan handyMan);
        public Interest GetById(int id);
        public bool DeleteRemainingInterests(int jobAdId);
        public List<HandyMan> GetRemainingHandymen(int interestId, int jobHandyId);
        public ApiResponse GetByUser(User user);
        public Interest GetByJobAdAndHandyman(int jobAd, int handyman);
    }
    
    public class InterestService : IInterestService
    {

        private readonly IInterestRepository _interestRepository;
        private readonly IJobAdService _jobAdService;
        private readonly IMailService _mailService;

        public InterestService(IInterestRepository interestRepository, IJobAdService jobAdService, IMailService mailService)
        {
            _interestRepository = interestRepository;
            _jobAdService = jobAdService;
            _mailService = mailService;
        }

        public ApiResponse MakeInterest(Interest interest, int jobAdId, HandyMan handyMan)
        {
            interest.HandyMan = handyMan;
            JobAd found = _jobAdService.GetById(jobAdId);

            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Could not find job ad by id.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            if (_interestRepository.GetByJobAdAndHandymanId(found.Id, handyMan.Id) != null)
            {
                return new ApiResponse()
                {
                    Message = "You already notified user about your interest with this job ad.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            interest.JobAd = found;

            Interest saved = _interestRepository.Create(interest);

            if (saved == null)
            {
                return new ApiResponse()
                {
                    Message = "Something went wrong with the database while creating interest. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Greetings " + found.Owner.FirstName + "!<br>A handyman " + handyMan.FirstName + " " + handyMan.LastName + " is interested " +
                       "in your job ad. To see all the interests and accept any, <a href = 'https://localhost:4200'>log in</a> and click the section 'Interests'",
                Subject = "Interest in your job ad titled " + found.Title,
                ToEmail = found.Owner.Email
            });

            return new ApiResponse()
            {
                Message =
                    "Successfully made interest in job ad. Check your email to see if job ad owner is willing to work with you.",
                ResponseObject = saved.ToInterestDTO(),
                Status = 201
            };
        }

        public Interest GetById(int id)
        {
            return _interestRepository.GetById(id);
        }

        public bool DeleteRemainingInterests(int jobAdId)
        {
            return _interestRepository.DeleteRemainingInterests(jobAdId);
        }

        public List<HandyMan> GetRemainingHandymen(int interestId, int jobHandyId)
        {
            return _interestRepository.GetRemainingHandymen(interestId, jobHandyId);
        }

        public ApiResponse GetByUser(User user)
        {
            List<InterestDashboardDTO> result = new List<InterestDashboardDTO>();
            
            foreach (JobAd jobAd in user.JobAds)
            {
                JobAd fullJobAd = _jobAdService.GetById(jobAd.Id);
                List<Interest> interests = _interestRepository.GetByJobAd(fullJobAd.Id);
                foreach (Interest interest in interests)
                {
                    result.Add(interest.ToInterestDashboardDTO(fullJobAd.ToJobAdDashboardDTO()));
                }
                
            }

            return new ApiResponse()
            {
                Message = "Successfully fetched interests by user.",
                ResponseObject = result,
                Status = 200
            };
        }

        public Interest GetByJobAdAndHandyman(int jobAd, int handyman)
        {
            return _interestRepository.GetByJobAdAndHandymanId(jobAd, handyman);
        }
    }
}