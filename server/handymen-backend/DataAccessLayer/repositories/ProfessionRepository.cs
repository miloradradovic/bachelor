using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{

    public interface IProfessionRepository
    {
        public Profession GetById(int id);
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
    }
}