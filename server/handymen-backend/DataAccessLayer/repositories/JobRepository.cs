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
    }
}