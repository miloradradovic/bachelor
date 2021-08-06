using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IInterestRepository
    {
        public Interest Create(Interest interest);
        public Interest GetByJobAd(int jobAd);
        public Interest GetById(int id);
        public bool DeleteRemainingInterests(int jobAdId);
        public List<HandyMan> GetRemainingHandymen(int interestId, int jobHandyId);
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

        public Interest GetById(int id)
        {
            Interest found = _context.Interests
                .Include(interest => interest.HandyMan)
                .Include(interest => interest.JobAd)
                    .ThenInclude(ad => ad.Owner)
                .SingleOrDefault(i => i.Id == id);
            return found;
        }

        public bool DeleteRemainingInterests(int jobAdId)
        {
            List<Interest> interestsToDelete = (_context.Interests
                .Include(interest => interest.JobAd)
                .Where(interest => interest.JobAd.Id == jobAdId)).ToList();

            foreach (Interest interest in interestsToDelete)
            {
                try
                {
                    _context.Interests.Remove(interest);
                    _context.SaveChanges();
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public List<HandyMan> GetRemainingHandymen(int interestId, int jobHandyId)
        {
            List<Interest> toReturn = (_context.Interests
                .Include(interest => interest.HandyMan)
                .Where(interest => interest.HandyMan.Id != jobHandyId && interest.Id == interestId)).ToList();
            List<HandyMan> toReturnHandy = new List<HandyMan>();
            
            foreach (Interest interest in toReturn)
            {
                toReturnHandy.Add(interest.HandyMan);
            }

            return toReturnHandy;
        }
    }
}