using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IJobRepository
    {
        public Job Create(Job toSave);
        public Job GetById(int id);
        public Job Update(Job toUpdate);
        public Job GetByJobAd(int jobAd);
        public List<Job> GetByUser(int user);
        public List<Job> GetByHandyman(int handyman);
        public bool CheckIfRated(Job job);
    }
    
    public class JobRepository : IJobRepository
    {

        private readonly PostgreSqlContext _context;

        public JobRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public Job Create(Job toSave)
        {
            _context.Jobs.Add(toSave);
            _context.SaveChanges();
            return toSave;
        }

        public Job GetById(int id)
        {
            Job found = _context.Jobs
                .Include(job => job.HandyMan)
                .Include(job => job.User)
                .Include(job => job.JobAd)
                .SingleOrDefault(job => job.Id == id);
            return found;
        }

        public Job Update(Job toUpdate)
        {
            _context.Jobs.Update(toUpdate);
            _context.SaveChanges();
            return toUpdate;
        }

        public Job GetByJobAd(int jobAd)
        {
            Job found = _context.Jobs.Include(job => job.JobAd)
                .SingleOrDefault(job => job.JobAd.Id == jobAd);
            return found;
        }

        public List<Job> GetByHandyman(int handyman)
        {
            List<Job> found = _context.Jobs
                .Include(job => job.User)
                .Include(job => job.HandyMan)
                .Include(job => job.JobAd)
                .Where(job => job.HandyMan.Id == handyman)
                .ToList();
            return found;
        }

        public List<Job> GetByUser(int user)
        {
            List<Job> found = _context.Jobs
                .Include(job => job.User)
                .Include(job => job.HandyMan)
                .Include(job => job.JobAd)
                .Where(job => job.User.Id == user)
                .ToList();
            return found;
        }

        public bool CheckIfRated(Job job)
        {
            Rating rating = _context.Ratings.Include(rating1 => rating1.RatedJob)
                .SingleOrDefault(rating1 => rating1.RatedJob.Id == job.Id);
            return rating != null;
        }
    }
}