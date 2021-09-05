using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IProfessionRepository
    {
        public Profession GetById(int id);
        public List<Profession> GetAll();
        public Profession GetByName(string name);
    }
    
    public class ProfessionRepository: IProfessionRepository
    {

        private readonly PostgreSqlContext _context;

        public ProfessionRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public Profession GetById(int id)
        {
            Profession found = _context.Professions.Include(profession => profession.Trades)
                .SingleOrDefault(profession => profession.Id == id);
            return found;
        }

        public Profession GetByName(string name)
        {
            Profession found = _context.Professions.Include(profession => profession.Trades)
                .SingleOrDefault(profession => profession.Name == name);
            return found;
        }

        public List<Profession> GetAll()
        {
            List<Profession> result = _context.Professions
                .Include(profession => profession.Trades)
                .ToList();
            return result;
        }
    }
}