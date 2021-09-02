using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;
using BC = BCrypt.Net.BCrypt;

namespace DataAccessLayer.repositories
{
    public interface IHandymanRepository
    {
        public HandyMan GetById(int id, bool verified);
        public HandyMan GetById(int id);
        public HandyMan GetByEmailAndPassword(string email, string password);
        public HandyMan GetByEmail(string email);
        public HandyMan Create(HandyMan toSave);
        public HandyMan Update(HandyMan toUpdate);
        public bool Delete(HandyMan toDelete);
        public List<HandyMan> GetAll();
        public List<HandyMan> Search(SearchParams searchParams, bool searchByFirstName, bool searchByLastName);
        public Rating GetDetailedRatingProfile(int ratingId);
        public List<HandyMan> GetAllUnverified();
        public List<HandyMan> GetAllVerified();
    }

    public class HandymanRepository : IHandymanRepository
    {
        private readonly PostgreSqlContext _context;

        public HandymanRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public HandyMan GetById(int id)
        {
            HandyMan found = _context.HandyMen
                .Include(handy => handy.Ratings)
                .Include(handy => handy.Trades)
                .Include(handy => handy.DoneJobs)
                .Include(handy => handy.Address)
                .SingleOrDefault(handyman => handyman.Id == id);
            return found;
        }

        public HandyMan GetById(int id, bool verified)
        {
            HandyMan found = _context.HandyMen
                .Include(handy => handy.Ratings)
                .Include(handy => handy.Trades)
                .Include(handy => handy.DoneJobs)
                .Include(handy => handy.Address)
                .SingleOrDefault(handyman => handyman.Id == id && handyman.Verified == verified);
            return found;
        }

        public HandyMan GetByEmailAndPassword(string email, string password)
        {
            HandyMan found = _context.HandyMen
                .Include(handy => handy.Ratings)
                .Include(handy => handy.Trades)
                .Include(handy => handy.DoneJobs)
                .Include(handy => handy.Address)
                .SingleOrDefault(handyman =>
                handyman.Email == email && handyman.Verified);
            
            if (found != null)
            {
                if (BC.Verify(password, found.Password))
                {
                    return found;
                }

            }
            return null;
        }

        public HandyMan GetByEmail(string email)
        {
            HandyMan found = _context.HandyMen
                .Include(handy => handy.Ratings)
                .Include(handy => handy.Trades)
                .Include(handy => handy.DoneJobs)
                .Include(handy => handy.Address)
                .SingleOrDefault(handyman => handyman.Email == email);
            return found;
        }

        public HandyMan Create(HandyMan toSave)
        {
            _context.HandyMen.Add(toSave);
            _context.SaveChanges();
            return toSave;
        }

        public HandyMan Update(HandyMan toUpdate)
        {
            _context.HandyMen.Update(toUpdate);
            _context.SaveChanges();
            return toUpdate;
        }

        public bool Delete(HandyMan toDelete)
        {
            try
            {
                _context.HandyMen.Remove(toDelete);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<HandyMan> GetAll()
        {
            List<HandyMan> result = _context.HandyMen.Include(handy => handy.Ratings)
                .Include(handy => handy.Trades)
                .Include(handy => handy.DoneJobs)
                .Include(handy => handy.Address)
                .ToList();
            return result;
        }

        public List<HandyMan> Search(SearchParams searchParams, bool searchByFirstName, bool searchByLastName)
        {
            List<HandyMan> result = GetAllVerified();

            if (searchByFirstName && searchByLastName) //TT
            {
                result = _context.HandyMen.Include(handy => handy.Ratings)
                    .Include(handy => handy.Trades)
                    .Include(handy => handy.DoneJobs)
                    .Include(handy => handy.Address)
                    .Where(man => man.FirstName.Contains(searchParams.FirstName) &&
                                  man.LastName.Contains(searchParams.LastName) && man.Verified)
                    .ToList();
            }
            else if (searchByFirstName && !searchByLastName) //TF
            {
                result = _context.HandyMen.Include(handy => handy.Ratings)
                    .Include(handy => handy.Trades)
                    .Include(handy => handy.DoneJobs)
                    .Include(handy => handy.Address)
                    .Where(man => man.FirstName.Contains(searchParams.FirstName) && man.Verified)
                    .ToList();
            }
            else if (!searchByFirstName && searchByLastName) //FT
            {
                result = _context.HandyMen.Include(handy => handy.Ratings)
                    .Include(handy => handy.Trades)
                    .Include(handy => handy.DoneJobs)
                    .Include(handy => handy.Address)
                    .Where(man => man.LastName.Contains(searchParams.LastName) && man.Verified)
                    .ToList();
            }

            return result;
        }

        public Rating GetDetailedRatingProfile(int ratingId)
        {
            Rating found = _context.Ratings
                .Include(rating => rating.RatedJob)
                .ThenInclude(job => job.User)
                .SingleOrDefault(rating => rating.Id == ratingId);
            return found;
        }

        public List<HandyMan> GetAllUnverified()
        {
            List<HandyMan> result = _context.HandyMen
                .Include(handy => handy.Ratings)
                .Include(handy => handy.Trades)
                .Include(handy => handy.DoneJobs)
                .Include(handy => handy.Address)
                .Where(man => man.Verified == false)
                .ToList();
            return result;
        }

        public List<HandyMan> GetAllVerified()
        {
            List<HandyMan> result = _context.HandyMen
                .Include(handy => handy.Ratings)
                .Include(handy => handy.Trades)
                .Include(handy => handy.DoneJobs)
                .Include(handy => handy.Address)
                .Where(man => man.Verified)
                .ToList();
            return result;
        }
    }
}