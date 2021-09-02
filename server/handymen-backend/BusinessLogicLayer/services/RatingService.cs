using System;
using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IRatingService
    {
        public ApiResponse CreateRating(Rating toCreate, int jobId);
    }
    
    public class RatingService : IRatingService
    {

        private readonly IRatingRepository _ratingRepository;
        private readonly IPersonService _personService;
        private readonly IJobService _jobService;

        public RatingService(IRatingRepository ratingRepository, IPersonService personService, IJobService jobService)
        {
            _ratingRepository = ratingRepository;
            _personService = personService;
            _jobService = jobService;
        }

        public ApiResponse CreateRating(Rating toCreate, int jobId)
        {
            
            Job foundJob = _jobService.GetById(jobId);
            if (foundJob == null)
            {
                return new ApiResponse()
                {
                    Message = "Posao sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            if (_ratingRepository.GetByJob(foundJob.Id) != null)
            {
                return new ApiResponse()
                {
                    Message = "Vec ste ocenili majstora za ovaj posao.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            if (!foundJob.Finished)
            {
                return new ApiResponse()
                {
                    Message = "Posao nije zavrsen.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            HandyMan found = _personService.GetHandymanById(foundJob.HandyMan.Id);

            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Majstor sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            toCreate.RatedJob = foundJob;
            toCreate.PublishedDate = DateTime.Now;
            Rating created = _ratingRepository.CreateRating(toCreate);
            
            if (created == null)
            {
                return new ApiResponse()
                {
                    Message =
                        "Nesto se desilo sa bazom podataka prilikom kreiranja ocene. Molimo pokusajte ponovo kasnije.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            found.Ratings.Add(created);
            found.CalculateAverageRate();
            HandyMan updated = _personService.UpdateHandyman(found);

            if (updated == null)
            {
                return new ApiResponse()
                {
                    Message = "Nesto se desilo sa bazom prilikom azuriranja podataka o majstoru. Molimo pokusajte ponovo kasnije.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            return new ApiResponse()
            {
                Message = "Uspesno kreirana nova ocena.",
                ResponseObject = created.ToRatingDTO(jobId),
                Status = 201
            };
        }
    }
}