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
            ApiResponse response = new ApiResponse();
            Rating found = _ratingRepository.GetById(ratingId);
            if (found == null)
            {
                response.SetError("Rejting sa tim id nije pronadjen.", 400);
                return response;
            }

            Job foundJob = _jobService.GetById(found.RatedJob.Id);
            if (foundJob == null)
            {
                response.SetError("Posao sa tim id nije pronadjen.", 400);
                return response;
            }
            
            HandyMan foundHandyman = _personService.GetHandymanById(foundJob.HandyMan.Id);

            if (foundHandyman == null)
            {
                response.SetError("Majstor sa tim id nije pronadjen.", 400);
                return response;
            }

            foundHandyman.Ratings.Remove(found);
            _personService.UpdateHandyman(foundHandyman);
            _ratingRepository.Delete(ratingId);
            response.DeletedRating(found, foundJob.Id, "Uspesno obrisan rejting.", 200);
            return response;
        }

        public ApiResponse VerifyRating(int ratingId)
        {
            ApiResponse response = new ApiResponse();
            Rating found = _ratingRepository.GetById(ratingId);
            if (found == null)
            {
                response.SetError("Rejting sa tim id nije pronadjen.", 400);
                return response;
            }

            found.Verified = true;
            
            Job foundJob = _jobService.GetById(found.RatedJob.Id);
            if (foundJob == null)
            {
                response.SetError("Posao sa tim id nije pronadjen.", 400);
                return response;
            }
            
            HandyMan foundHandyman = _personService.GetHandymanById(foundJob.HandyMan.Id);

            if (foundHandyman == null)
            {
                response.SetError("Majstor sa tim id nije pronadjen.", 400);
                return response;
            }

            Rating updated = _ratingRepository.Update(found);
            if (updated == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom verifikovanja ocene. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }
            foundHandyman.CalculateAverageRate();
            _personService.UpdateHandyman(foundHandyman);
            response.VerifiedRating(updated, foundJob.Id, "Uspesno verifikovana ocena.", 200);
            return response;
        }

        public ApiResponse CreateRating(Rating toCreate, int jobId)
        {
            ApiResponse response = new ApiResponse();
            Job foundJob = _jobService.GetById(jobId);
            if (foundJob == null)
            {
                response.SetError("Posao sa tim id nije pronadjen.", 400);
                return response;
            }

            if (_ratingRepository.GetByJob(foundJob.Id) != null)
            {
                response.SetError("Vec ste ocenili majstora za ovaj posao.", 400);
                return response;
            }

            if (!foundJob.Finished)
            {
                response.SetError("Posao nije zavrsen.", 400);
                return response;
            }
            
            HandyMan found = _personService.GetHandymanById(foundJob.HandyMan.Id);

            if (found == null)
            {
                response.SetError("Majstor sa tim id nije pronadjen.", 400);
                return response;
            }

            toCreate.RatedJob = foundJob;
            toCreate.PublishedDate = DateTime.Now;
            Rating created = _ratingRepository.CreateRating(toCreate);
            
            if (created == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom kreiranja ocene. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }
            
            found.Ratings.Add(created);
            HandyMan updated = _personService.UpdateHandyman(found);

            if (updated == null)
            {
                response.SetError("Nesto se desilo sa bazom prilikom azuriranja podataka o majstoru. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }

            response.CreatedRating(created, jobId,
                "Uspesno kreirana nova ocena. Bice prikazana nakon sto je nas administrator tim odobri.", 201);
            return response;
        }

        public ApiResponse GetRatings(bool verified)
        {
            ApiResponse response = new ApiResponse();
            List<Rating> ratings = _ratingRepository.GetRatings(verified);
            response.GotRatings(ratings, "Uspesno dobavljeni neverifikovani rejtinzi.", 200);
            return response;
        }
    }
}