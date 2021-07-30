using System.Linq;
using Model.models;

namespace DataAccessLayer.repositories
{
    public interface IHandymanRepository
    {
        public HandyMan GetById(int id);
        public HandyMan GetByEmailAndPassword(string email, string password);
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
            HandyMan found = _context.HandyMen.SingleOrDefault(handyman => handyman.Id == id);
            return found;
        }

        public HandyMan GetByEmailAndPassword(string email, string password)
        {
            HandyMan found = _context.HandyMen.SingleOrDefault(handyman =>
                handyman.Email == email && handyman.Password == password);
            return found;
        }
    }
}