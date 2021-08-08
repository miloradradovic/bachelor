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
                    Message = "Could not find job by id.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            if (_ratingRepository.GetByJob(foundJob.Id) != null)
            {
                return new ApiResponse()
                {
                    Message = "You already rated handyman for this job.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            if (!foundJob.Finished)
            {
                return new ApiResponse()
                {
                    Message = "Job is not finished.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            HandyMan found = _personService.GetHandymanById(foundJob.HandyMan.Id);

            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Could not find handyman by id.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            toCreate.RatedJob = foundJob;
            Rating created = _ratingRepository.CreateRating(toCreate);
            
            if (created == null)
            {
                return new ApiResponse()
                {
                    Message =
                        "Something went wrong with the database while creating a new rate. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            found.Ratings.Add(created);
            HandyMan updated = _personService.UpdateHandyman(found);

            if (updated == null)
            {
                return new ApiResponse()
                {
                    Message = "Something went wrong with the database while updating handyman. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            return new ApiResponse()
            {
                Message = "Successfully created new rating.",
                ResponseObject = created.ToRatingDTO(jobId),
                Status = 201
            };
        }
    }
}