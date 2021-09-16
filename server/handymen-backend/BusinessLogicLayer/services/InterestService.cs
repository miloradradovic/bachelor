using System.Collections.Generic;
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
            ApiResponse response = new ApiResponse();
            interest.HandyMan = handyMan;
            JobAd found = _jobAdService.GetById(jobAdId);

            if (found == null)
            {
                response.SetError("Posao sa tim id nije pronadjen.", 400);
                return response;
            }

            if (_interestRepository.GetByJobAdAndHandymanId(found.Id, handyMan.Id) != null)
            {
                response.SetError("Vec ste iskazali interesovanje za ovaj oglas.", 400);
                return response;
            }
            
            interest.JobAd = found;

            Interest saved = _interestRepository.Create(interest);

            if (saved == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom kreiranja interesa. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Pozdrav " + found.Owner.FirstName + "!<br>Majstor " + handyMan.FirstName + " " + handyMan.LastName + " je zainteresovan " +
                       "za Vas oglas za posao. Da biste videli sve majstore koji su zainteresovani i eventualno prihvatite neku od ponuda, <a href = 'https://localhost:4200'>ulogujte se</a> i kliknite sekciju 'Interesi'.",
                Subject = "Interes za vas oglas pod naslovom " + found.Title,
                ToEmail = found.Owner.Email
            });

            response.CreatedInterest(saved,
                "Uspesno ste iskazali interesovanje za ovaj oglas. Proveravajte email da biste ispratili da vlasnik oglasa zeli da radi sa Vama.",
                201);
            return response;
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
            ApiResponse response = new ApiResponse();
            
            foreach (JobAd jobAd in user.JobAds)
            {
                JobAd fullJobAd = _jobAdService.GetById(jobAd.Id);
                List<Interest> interests = _interestRepository.GetByJobAd(fullJobAd.Id);
                foreach (Interest interest in interests)
                {
                    response.GotInterestDashboard(interest, fullJobAd, interests.IndexOf(interest));
                }
            }

            response.Message = "Uspesno dobavljeni interesi za korisnika.";
            response.Status = 200;
            return response;
        }

        public Interest GetByJobAdAndHandyman(int jobAd, int handyman)
        {
            return _interestRepository.GetByJobAdAndHandymanId(jobAd, handyman);
        }
    }
}