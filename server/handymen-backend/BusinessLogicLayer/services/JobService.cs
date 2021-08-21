﻿using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IJobService
    {
        public ApiResponse CreateJob(int interest);
        public ApiResponse FinishJob(int jobId);
        public Job GetById(int jobId);
    }
    
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IInterestService _interestService;
        private readonly IMailService _mailService;

        public JobService(IJobRepository jobRepository, IInterestService interestService, IMailService mailService)
        {
            _jobRepository = jobRepository;
            _interestService = interestService;
            _mailService = mailService;
        }

        public ApiResponse CreateJob(int interest)
        {
            Interest found = _interestService.GetById(interest);
            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Could not find interest by id.",
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
                    Message = "Something went wrong with the database while creating job. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            bool deleted = _interestService.DeleteRemainingInterests(saved.JobAd.Id);

            if (!deleted)
            {
                return new ApiResponse()
                {
                    Message =
                        "Something went wrong with the database while deleting remaining interests. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            List<HandyMan> handyMen = _interestService.GetRemainingHandymen(found.Id, saved.HandyMan.Id);
            foreach (HandyMan handyman in handyMen)
            {
                _mailService.SendEmail(new MailRequest()
                {
                    Body = "Greetings " + handyman.FirstName + ". Unfortunately, one of your interests refused you. Best regards!",
                    Subject = "Sucks :(",
                    ToEmail = handyman.Email
                });
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Greetings " + saved.JobAd.Owner.FirstName + ". Congrats on striking a deal with your job ad. When job is finished, please verify it on your account, so that you can leave your review. Best regards!",
                Subject = "Congrats on Job!",
                ToEmail = saved.JobAd.Owner.Email
            });
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Greetings " + saved.HandyMan.FirstName + ". Congrats on striking a new job. When job is finished, please verify it on your account, so that job ad owner can leave a review. Best regards!",
                Subject = "Congrats on Job!",
                ToEmail = saved.HandyMan.Email
            });

            return new ApiResponse()
            {
                Message = "Successfully created a new job.",
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
                    Message = "Could not find job by id",
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
                    Message = "Something went wrong with the database while updating your job. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            return new ApiResponse()
            {
                Message = "Successfully updated job.",
                ResponseObject = updated.ToJobDTO(),
                Status = 200
            };
        }

        public Job GetById(int jobId)
        {
            return _jobRepository.GetById(jobId);
        }
    }
}