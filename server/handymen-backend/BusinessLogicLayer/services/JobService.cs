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
            ApiResponse response = new ApiResponse();
            Interest found = _interestService.GetById(interest);
            if (found == null)
            {
                response.SetError("Interes sa tim id nije pronadjen.", 400);
                return response;
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
                response.SetError("Nesto se desilo sa bazom podataka prilikom kreiranja posla. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }

            bool deleted = _interestService.DeleteRemainingInterests(saved.JobAd.Id);
            bool deleted2 = _offerService.DeleteOffers(saved.JobAd.Id);

            if (!deleted || !deleted2)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom brisanja ponuda i interesa za posao. Molimo pokusajte ponovo kasnije.", 400);
                return response;
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

            response.CreatedJob(saved, "Uspesno kreiran posao.", 201);
            return response;
        }

        public ApiResponse FinishJob(int jobId)
        {
            ApiResponse response = new ApiResponse();
            Job found = _jobRepository.GetById(jobId);

            if (found == null)
            {
                response.SetError("Posao sa tim id nije pronadjen.", 400);
                return response;
            }

            found.Finished = true;
            Job updated = _jobRepository.Update(found);

            if (updated == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom azuriranja statusa posla. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }

            response.FinishedJob(updated, "Uspesno azuriran status posla.", 200);
            return response;
        }

        public Job GetById(int jobId)
        {
            return _jobRepository.GetById(jobId);
        }

        public ApiResponse GetByUser(User user)
        {
            ApiResponse response = new ApiResponse();
            List<Job> jobs = _jobRepository.GetByUser(user.Id);
            foreach (Job job in jobs)
            {
                response.GotJobDashboard(job, _jobRepository.CheckIfRated(job), jobs.IndexOf(job));
            }

            response.Message = "Uspesno dobavljeni poslovi za korisnika.";
            response.Status = 200;
            return response;
        }

        public ApiResponse GetByHandyman(HandyMan handyMan)
        {
            ApiResponse response = new ApiResponse();
            List<Job> jobs = _jobRepository.GetByHandyman(handyMan.Id);
            List<JobDashboardDTO> jobAdDashboardDtos = new List<JobDashboardDTO>();
            foreach (Job job in jobs)
            {
                response.GotJobDashboard(job, _jobRepository.CheckIfRated(job), jobs.IndexOf(job));
            }

            response.Message = "Uspesno dobavljeni poslovi za majstora.";
            response.Status = 200;
            return response;
        }
    }
}