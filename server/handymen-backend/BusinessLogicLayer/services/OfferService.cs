using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IOfferService
    {
        public ApiResponse MakeOffer(int handymanId, int jobAdId);
        public bool DeleteOffers(int jobAdId);
        public ApiResponse GetOffersByHandyman(HandyMan handyMan);
    }
    
    public class OfferService: IOfferService
    {

        private readonly IOfferRepository _offerRepository;
        private readonly IHandymanService _handymanService;
        private readonly IJobAdService _jobAdService;
        private readonly IMailService _mailService;
        private readonly IInterestService _interestService;

        public OfferService(IOfferRepository offerRepository, IHandymanService handymanService,
            IJobAdService jobAdService,
            IMailService mailService,
            IInterestService interestService)
        {
            _offerRepository = offerRepository;
            _handymanService = handymanService;
            _jobAdService = jobAdService;
            _mailService = mailService;
            _interestService = interestService;
        }

        public ApiResponse MakeOffer(int handymanId, int jobAdId)
        {
            ApiResponse response = new ApiResponse();
            HandyMan foundHandyman = _handymanService.GetById(handymanId);
            JobAd foundJobAd = _jobAdService.GetById(jobAdId);
            if (foundHandyman == null || foundJobAd == null)
            {
                response.SetError("Majstor ili oglas sa tim id nije pronadjen.", 400);
                return response;
            }
            Offer created = _offerRepository.Create(new Offer()
            {
                HandyMan = foundHandyman,
                JobAd = foundJobAd
            });
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Pozdrav " + foundHandyman.FirstName + "!<br>Neko Vam je ponudio posao. Ponuda se nalazi u 'Ponude' sekciji.",
                Subject = "Nova ponuda za posao",
                ToEmail = foundHandyman.Email
            });

            response.CreatedOffer(created, "Uspesno kreirana ponuda.", 200);
            return response;
        }

        public bool DeleteOffers(int jobAdId)
        {
            _offerRepository.DeleteRemainingOffers(jobAdId);
            return true;
        }

        public ApiResponse GetOffersByHandyman(HandyMan handyMan)
        {
            ApiResponse response = new ApiResponse();
            List<Offer> offers = _offerRepository.GetOffersByHandyman(handyMan.Id);
            List<JobAd> result = new List<JobAd>();
            foreach (Offer offer in offers)
            {
                if (_interestService.GetByJobAdAndHandyman(offer.JobAd.Id, handyMan.Id) == null)
                {
                    result.Add(_jobAdService.GetById(offer.JobAd.Id));
                }
            }
            
            response.GotJobAdDashboard(result, "Uspesno dobavljene sve ponude.", 200);
            return response;
        }
    }
}