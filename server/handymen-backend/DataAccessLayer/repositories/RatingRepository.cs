using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.dto;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IRatingRepository
    {
        public Rating CreateRating(Rating toCreate);
        public Rating GetByJob(int jobId);
        public List<Rating> GetRatings(bool verified);
        public Rating GetById(int ratingId);
        public void Delete(int ratingId);
        public Rating Update(Rating toUpdate);
    }
    
    public class RatingRepository : IRatingRepository
    {
        private readonly PostgreSqlContext _context;

        public RatingRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public Rating CreateRating(Rating toCreate)
        {
            _context.Ratings.Add(toCreate);
            _context.SaveChanges();
            return toCreate;
        }

        public Rating GetByJob(int jobId)
        {
            Rating found = _context.Ratings
                .Include(rating => rating.RatedJob)
                .SingleOrDefault(rating => rating.RatedJob.Id == jobId);
            return found;
        }

        public List<Rating> GetRatings(bool verified)
        {
            List<Rating> ratings = _context.Ratings.Include(rating => rating.RatedJob)
                .Where(rating => rating.Verified == verified).ToList();
            return ratings;
        }

        public Rating GetById(int ratingId)
        {
            Rating found = _context.Ratings.Include(rating => rating.RatedJob)
                .SingleOrDefault(rating => rating.Id == ratingId);
            return found;
        }

        public void Delete(int ratingId)
        {
            Rating toDelete = GetById(ratingId);
            _context.Ratings.Remove(toDelete);
            _context.SaveChanges();
        }

        public Rating Update(Rating toUpdate)
        {
            _context.Ratings.Update(toUpdate);
            _context.SaveChanges();
            return toUpdate;
        }
    }
}