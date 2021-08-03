using System;
using System.Linq;
using Model.models;

namespace DataAccessLayer.repositories
{
    
    public interface IRegistrationRequestRepository
    {
        
        public RegistrationRequest Create(RegistrationRequest toCreate);
        public RegistrationRequest GetByEmail(string email);

    }
    
    public class RegistrationRequestRepository : IRegistrationRequestRepository
    {
        
        private readonly PostgreSqlContext _context;

        public RegistrationRequestRepository(PostgreSqlContext context)
        {
            _context = context;
        }

        public RegistrationRequest Create(RegistrationRequest toCreate)
        {
            
            _context.RegistrationRequests.Add(toCreate);
            _context.SaveChanges();
            return toCreate; 
        }

        public RegistrationRequest GetByEmail(string email)
        {
            RegistrationRequest found =
                _context.RegistrationRequests.SingleOrDefault(request => request.Email == email);
            return found;
        }
    }
}