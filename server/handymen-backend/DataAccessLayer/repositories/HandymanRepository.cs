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
    }
}