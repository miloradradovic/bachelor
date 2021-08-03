using System.Linq;
using Model.models;
using BC = BCrypt.Net.BCrypt;

namespace DataAccessLayer.repositories
{
    public interface IAdministratorRepository
    {
        public Administrator GetById(int id);
        public Administrator GetByEmailAndPassword(string email, string password);
        public Administrator GetByEmail(string email);
    }
    
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly PostgreSqlContext _context;

        public AdministratorRepository(PostgreSqlContext context)
        {
            _context = context;
        }
        
        public Administrator GetById(int id)
        {
            Administrator found = _context.Administrators.SingleOrDefault(admin => admin.Id == id);
            return found;
        }

        public Administrator GetByEmailAndPassword(string email, string password)
        {
            Administrator found = _context.Administrators.SingleOrDefault(admin => 
                admin.Email == email && admin.Verified);

            if (found != null)
            {
                if (BC.Verify(password, found.Password))
                {
                    return found;
                }
            }
            return null;
        }

        public Administrator GetByEmail(string email)
        {
            Administrator found = _context.Administrators.SingleOrDefault(admin => admin.Email == email);
            return found;
        }
    }
}