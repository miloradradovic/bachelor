using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IJobService
    {
        public ApiResponse CreateJob(int interest);
        public ApiResponse FinishJob(int jobId);
        public Job GetById(int jobId);
        public ApiResponse GetByUser(User user);
        public ApiResponse GetByHandyman(HandyMan handyMan);
    }
    
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IInterestService _interestService;
        private readonly IMailService _mailService;
        private readonly IOfferService _offerService;

        public JobService(IJobRepository jobRepository, IInterestService interestService, IMailService mailService, IOfferService offerService)
        {
            _jobRepository = jobRepository;
            _interestService = interestService;
            _mailService = mailService;
            _offerService = offerService;
        }

        public ApiResponse CreateJob(int interest)
        {
            Interest found = _interestService.GetById(interest);
            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Interes sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            Job saved = _jobRepository.Create(new Job()
            {
                Finished = false,
                HandyMan = found.HandyMan,
                JobAd = found.JobAd,
                User = found.JobAd.Owner
            });

            if (saved == null)
            {
                return new ApiResponse()
                {
                    Message = "Nesto se desilo sa bazom podataka prilikom kreiranja posla. Molimo pokusajte ponovo kasnije.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            bool deleted = _interestService.DeleteRemainingInterests(saved.JobAd.Id);
            bool deleted2 = _offerService.DeleteOffers(saved.JobAd.Id);

            if (!deleted || !deleted2)
            {
                return new ApiResponse()
                {
                    Message =
                        "Nesto se desilo sa bazom podataka prilikom brisanja ponuda i interesa za posao. Molimo pokusajte ponovo kasnije.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            List<HandyMan> handyMen = _interestService.GetRemainingHandymen(found.Id, saved.HandyMan.Id);
            foreach (HandyMan handyman in handyMen)
            {
                _mailService.SendEmail(new MailRequest()
                {
                    Body = "Pozdrav " + handyman.FirstName + "!<br>Na zalost, jedan od vlasnika oglasa za koji ste bili zainteresovani je odbio Vasu ponudu.",
                    Subject = "Odbijena ponuda",
                    ToEmail = handyman.Email
                });
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Pozdrav " + saved.JobAd.Owner.FirstName + "!<br>Cestitamo na dogovoru za novi posao. Kada taj posao bude zavrsen, azurirajte status da biste mogli da ostavite komentar i ocenu.",
                Subject = "Cestitamo na poslu!",
                ToEmail = saved.JobAd.Owner.Email
            });
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Pozdrav " + saved.HandyMan.FirstName + "!<br>Cestitamo na dogovoru za novi posao. Kada taj posao bude zavrsen, azurirajte status da bi vlasnik oglasa mogao da ostavi komentar i ocenu.",
                Subject = "Cestitamo na poslu!",
                ToEmail = saved.HandyMan.Email
            });

            return new ApiResponse()
            {
                Message = "Uspesno kreiran posao.",
                ResponseObject = saved.ToJobDTO(),
                Status = 201
            };

        }

        public ApiResponse FinishJob(int jobId)
        {
            Job found = _jobRepository.GetById(jobId);

            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Posao sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            found.Finished = true;
            Job updated = _jobRepository.Update(found);

            if (updated == null)
            {
                return new ApiResponse()
                {
                    Message = "Nesto se desilo sa bazom podataka prilikom azuriranja statusa posla. Molimo pokusajte ponovo kasnije.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            return new ApiResponse()
            {
                Message = "Uspesno azuriran status posla.",
                ResponseObject = updated.ToJobDTO(),
                Status = 200
            };
        }

        public Job GetById(int jobId)
        {
            return _jobRepository.GetById(jobId);
        }

        public ApiResponse GetByUser(User user)
        {
            List<Job> jobs = _jobRepository.GetByUser(user.Id);
            List<JobDashboardDTO> jobAdDashboardDtos = new List<JobDashboardDTO>();
            foreach (Job job in jobs)
            {
                jobAdDashboardDtos.Add(job.ToJobDashboardDTO(_jobRepository.CheckIfRated(job)));
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljeni poslovi za korisnika.",
                ResponseObject = jobAdDashboardDtos,
                Status = 200
            };
        }

        public ApiResponse GetByHandyman(HandyMan handyMan)
        {
            List<Job> jobs = _jobRepository.GetByHandyman(handyMan.Id);
            List<JobDashboardDTO> jobAdDashboardDtos = new List<JobDashboardDTO>();
            foreach (Job job in jobs)
            {
                jobAdDashboardDtos.Add(job.ToJobDashboardDTO(_jobRepository.CheckIfRated(job)));
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljeni poslovi za majstora.",
                ResponseObject = jobAdDashboardDtos,
                Status = 200
            };
        }
    }
}