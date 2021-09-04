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
        public Interest InterestExists(int jobAd, int handyman);
        public Job GetJobByJobAdAndHandyman(int jobAd, int handyman);
        public Job GetJobByJobAd(int jobAd);
        public Offer GetOfferByJobAd(int jobAd);
        public List<JobAd> GetAllByCity(string city);
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
            List<JobAd> result = (from jobad in _context.JobAd.Include(jobad => jobad.Pictures).Include(jobad => jobad.Trades).Include(jobad => jobad.Owner).Include(jobad => jobad.AdditionalJobAdInfo)
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
                .Include(jobad => jobad.Pictures)
                .SingleOrDefault(jobad => jobad.Id == id);
            return found;
        }

        public List<JobAd> GetAllByCity(string city)
        {
            List<JobAd> result = _context.JobAd.Include(ad => ad.Address)
                .Include(ad => ad.Owner)
                .Include(ad => ad.Trades)
                .Include(ad => ad.AdditionalJobAdInfo)
                .Include(jobad => jobad.Pictures)
                .Where(ad => ad.Address.Name.Contains(city))
                .ToList();
            return result;
        }

        public List<JobAd> GetAll()
        {
            List<JobAd> result = _context.JobAd.Include(ad => ad.Address)
                .Include(ad => ad.Owner)
                .Include(ad => ad.Trades)
                .Include(ad => ad.AdditionalJobAdInfo)
                .Include(jobad => jobad.Pictures)
                .ToList();
            return result;
        }

        public Interest InterestExists(int jobAd, int handyman)
        {
            Interest found = _context.Interests.Include(interest => interest.JobAd)
                .Include(interest => interest.HandyMan)
                .SingleOrDefault(interest => interest.JobAd.Id == jobAd && interest.HandyMan.Id == handyman);
            return found;
        }

        public Job GetJobByJobAdAndHandyman(int jobAd, int handyman)
        {
            Job found = _context.Jobs
                .Include(job => job.User)
                .Include(job => job.HandyMan)
                .Include(job => job.JobAd)
                .SingleOrDefault(job => job.HandyMan.Id == handyman && job.JobAd.Id == jobAd);
            return found;
        }

        public Job GetJobByJobAd(int jobAd)
        {
            Job found = _context.Jobs
                .Include(job => job.User)
                .Include(job => job.HandyMan)
                .Include(job => job.JobAd)
                .SingleOrDefault(job => job.JobAd.Id == jobAd);
            return found;
        }

        public Offer GetOfferByJobAd(int jobAd)
        {
            Offer found = _context.Offers
                .Include(offer => offer.JobAd)
                .Include(offer => offer.HandyMan)
                .SingleOrDefault(offer => offer.JobAd.Id == jobAd);
            return found;
        }
    }
}