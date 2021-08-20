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
        public JobAd GetById(int id);
        public List<JobAd> GetAll();
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
            List<JobAd> result = (from jobad in _context.JobAd.Include(jobad => jobad.Trades).Include(jobad => jobad.Owner).Include(jobad => jobad.AdditionalJobAdInfo)
                where (handyMan.Trades.Any(item => jobad.Trades.Contains(item))) && 
                      (Math.Sqrt(Math.Pow(Math.Abs(handyMan.Address.Latitude - jobad.Address.Latitude), 2) + 
                                 Math.Pow(Math.Abs(handyMan.Radius - jobad.Address.Longitude), 2)) <= handyMan.Radius)
                select jobad).ToList();
            return result;
        }

        public JobAd GetById(int id)
        {
            JobAd found = _context.JobAd
                .Include(ad => ad.Trades)
                .Include(ad => ad.Owner)
                .Include(ad => ad.AdditionalJobAdInfo)
                .Include(ad => ad.Address)
                .SingleOrDefault(jobad => jobad.Id == id);
            return found;
        }

        public List<JobAd> GetAll()
        {
            List<JobAd> result = _context.JobAd.Include(ad => ad.Address)
                .Include(ad => ad.Owner)
                .Include(ad => ad.Trades)
                .Include(ad => ad.AdditionalJobAdInfo)
                .ToList();
            return result;
        }
    }
}