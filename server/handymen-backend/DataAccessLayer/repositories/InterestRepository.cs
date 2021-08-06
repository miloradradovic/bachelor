using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IInterestRepository
    {
        public Interest Create(Interest interest);
        public Interest GetByJobAd(int jobAd);
    }
    
    public class InterestRepository : IInterestRepository
    {

        private readonly PostgreSqlContext _context;

        public InterestRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public Interest Create(Interest interest)
        {
            _context.Interests.Add(interest);
            _context.SaveChanges();
            return interest;
        }

        public Interest GetByJobAd(int jobAd)
        {
            Interest found = _context.Interests
                .Include(interest => interest.HandyMan)
                .Include(interest => interest.JobAd)
                .SingleOrDefault(i => i.JobAd.Id == jobAd);
            return found;
        }
    }
}