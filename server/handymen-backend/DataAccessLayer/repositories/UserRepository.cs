using Contracts.repositories;
using Model.models;

namespace DataAccessLayer.repositories
{
    public class UserRepository: IUserRepository
    {

        private readonly PostgreSqlContext _context;

        public UserRepository(PostgreSqlContext context)
        {
            _context = context;
        }
        
        public User Create(User toCreate)
        {
            _context.Users.Add(toCreate);
            _context.SaveChanges();
            return toCreate;
        }
    }
}