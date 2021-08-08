using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IRatingRepository
    {
        public Rating CreateRating(Rating toCreate);
        public Rating GetByJob(int jobId);
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
    }
}