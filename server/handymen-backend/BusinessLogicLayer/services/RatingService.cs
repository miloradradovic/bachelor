using System;
using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.dto;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IRatingService
    {
        public ApiResponse CreateRating(Rating toCreate, int jobId);
        public ApiResponse GetRatings(bool verified);
        public ApiResponse VerifyRating(int ratingId);
        public ApiResponse DeleteRating(int ratingId);
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

        public ApiResponse DeleteRating(int ratingId)
        {
            Rating found = _ratingRepository.GetById(ratingId);
            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Rejting sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            Job foundJob = _jobService.GetById(found.RatedJob.Id);
            if (foundJob == null)
            {
                return new ApiResponse()
                {
                    Message = "Posao sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            HandyMan foundHandyman = _personService.GetHandymanById(foundJob.HandyMan.Id);

            if (foundHandyman == null)
            {
                return new ApiResponse()
                {
                    Message = "Majstor sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            foundHandyman.Ratings.Remove(found);
            _personService.UpdateHandyman(foundHandyman);
            _ratingRepository.Delete(ratingId);
            return new ApiResponse()
            {
                Message = "Uspesno obrisan rejting.",
                ResponseObject = found.ToRatingDTO(foundJob.Id),
                Status = 200
            };
        }

        public ApiResponse VerifyRating(int ratingId)
        {
            Rating found = _ratingRepository.GetById(ratingId);
            if (found == null)
            {
                return new ApiResponse()
                {
                    Message = "Rejting sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            found.Verified = true;
            
            Job foundJob = _jobService.GetById(found.RatedJob.Id);
            if (foundJob == null)
            {
                return new ApiResponse()
                {
                    Message = "Posao sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            HandyMan foundHandyman = _personService.GetHandymanById(foundJob.HandyMan.Id);

            if (foundHandyman == null)
            {
                return new ApiResponse()
                {
                    Message = "Majstor sa tim id nije pronadjen.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            Rating updated = _ratingRepository.Update(found);
            if (updated == null)
            {
                return new ApiResponse()
                {
                    Message =
                        "Nesto se desilo sa bazom podataka prilikom verifikovanja ocene. Molimo pokusajte ponovo kasnije.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            foundHandyman.CalculateAverageRate();
            _personService.UpdateHandyman(foundHandyman);
            return new ApiResponse()
            {
                Message = "Uspesno verifikovana ocena.",
                ResponseObject = updated.ToRatingDTO(foundJob.Id),
                Status = 200
            };
            
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
            //found.CalculateAverageRate();
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
                Message = "Uspesno kreirana nova ocena. Bice prikazana nakon sto je nas administrator tim odobri.",
                ResponseObject = created.ToRatingDTO(jobId),
                Status = 201
            };
        }

        public ApiResponse GetRatings(bool verified)
        {
            List<Rating> ratings = _ratingRepository.GetRatings(verified);
            List<RatingDTO> dtos = new List<RatingDTO>();
            foreach (Rating rating in ratings)
            {
                dtos.Add(new RatingDTO()
                {
                    Description = rating.Description,
                    Id = rating.Id,
                    Rate = rating.Rate,
                    PublishedDate = rating.PublishedDate
                });
            }

            return new ApiResponse()
            {
                Message = "Uspesno dobavljeni neverifikovani rejtinzi.",
                ResponseObject = dtos,
                Status = 200
            };
        }
    }
}