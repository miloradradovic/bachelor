using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IOfferRepository
    {
        public Offer Create(Offer offer);
        public bool DeleteRemainingOffers(int jobAdId);
        public List<Offer> GetOffersByHandyman(int handyman);
    }
    
    public class OfferRepository: IOfferRepository
    {

        private readonly PostgreSqlContext _context;

        public OfferRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public Offer Create(Offer offer)
        {
            _context.Offers.Add(offer);
            _context.SaveChanges();
            return offer;
        }

        public bool DeleteRemainingOffers(int jobAdId)
        {
            List<Offer> offers = _context.Offers
                .Include(offer => offer.HandyMan)
                .Include(offer => offer.JobAd)
                .Where(offer => offer.JobAd.Id == jobAdId)
                .ToList();

            foreach (Offer offer in offers)
            {
                _context.Offers.Remove(offer);
                _context.SaveChanges();
            }

            return true;
        }

        public List<Offer> GetOffersByHandyman(int handyman)
        {
            List<Offer> offers = _context.Offers
                .Include(offer => offer.JobAd)
                .Include(offer => offer.HandyMan)
                .Where(offer => offer.HandyMan.Id == handyman)
                .ToList();
            return offers;
        }
    }
}