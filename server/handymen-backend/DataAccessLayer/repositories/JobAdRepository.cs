using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IJobAdRepository
    {
        public JobAd CreateJobAd(JobAd jobAd);
        public List<JobAd> GetJobAdsForCurrentHandyman(HandyMan handyMan);
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

        public List<JobAd> GetJobAdsForCurrentHandyman(HandyMan handyMan)
        {
            List<JobAd> result = (from jobad in _context.JobAd.Include(jobad => jobad.Trades)
                where (handyMan.Trades.Any(item => jobad.Trades.Contains(item))) && 
                      (Math.Sqrt(Math.Pow(Math.Abs(handyMan.Circle.Latitude - jobad.Address.Latitude), 2) + 
                                 Math.Pow(Math.Abs(handyMan.Circle.Radius - jobad.Address.Longitude), 2)) <= handyMan.Circle.Radius)
                select jobad).ToList();
            return result;
        }
    }
}