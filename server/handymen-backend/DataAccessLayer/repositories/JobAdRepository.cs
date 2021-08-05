using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IJobAdRepository
    {
        public JobAd CreateJobAd(JobAd jobAd);
    }
    
    public class JobAdRepository : IJobAdRepository
    {

        private readonly PostgreSqlContext _context;

        public JobAdRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public JobAd CreateJobAd(JobAd jobAd)
        {
            _context.JobAd.Add(jobAd);
            _context.SaveChanges();
            return jobAd;
        }
    }
}