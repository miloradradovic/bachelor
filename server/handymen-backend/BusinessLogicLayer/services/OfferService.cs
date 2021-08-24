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
            HandyMan foundHandyman = _handymanService.GetById(handymanId);
            JobAd foundJobAd = _jobAdService.GetById(jobAdId);
            if (foundHandyman == null || foundJobAd == null)
            {
                return new ApiResponse()
                {
                    Message = "Majstor ili oglas sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
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
            
            return new ApiResponse()
            {
                Message = "Uspesno kreirana ponuda.",
                ResponseObject = new OfferDTO()
                {
                    HandyMan = handymanId,
                    Id = created.Id,
                    JobAd = jobAdId
                }
            };
        }

        public bool DeleteOffers(int jobAdId)
        {
            _offerRepository.DeleteRemainingOffers(jobAdId);
            return true;
        }

        public ApiResponse GetOffersByHandyman(HandyMan handyMan)
        {
            List<Offer> offers = _offerRepository.GetOffersByHandyman(handyMan.Id);
            List<JobAdDashboardDTO> dtos = new List<JobAdDashboardDTO>();
            foreach (Offer offer in offers)
            {
                if (_interestService.GetByJobAdAndHandyman(offer.JobAd.Id, handyMan.Id) == null)
                {
                    dtos.Add(_jobAdService.GetById(offer.JobAd.Id).ToJobAdDashboardDTO());
                }
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljene sve ponude.",
                ResponseObject = dtos,
                Status = 200
            };
        }
    }
}