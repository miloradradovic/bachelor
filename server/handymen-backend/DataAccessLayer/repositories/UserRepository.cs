using System.Linq;
using Model.models;
using BC = BCrypt.Net.BCrypt;

namespace DataAccessLayer.repositories
{
    public interface IUserRepository
    {
        
        public User Create(User toCreate);
        public User GetById(int id);
        public User GetByEmailAndPassword(string email, string password);
        public User GetByEmail(string email);
        public User Update(User toUpdate);
        /*
        public List<User> GetUsersBySomething(Something something);

        public User GetUserByUsername(string username);
        */
    }
    
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

        public User GetById(int id)
        {
            User found = _context.Users.SingleOrDefault(user => user.Id == id);
            return found;
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            User found = _context.Users.SingleOrDefault(user => user.Email == email && user.Verified);
            
            if (found != null)
            {
                if (BC.Verify(password, found.Password))
                {
                    return found;
                }

            }
            return null;
        }

        public User GetByEmail(string email)
        {
            User found = _context.Users.SingleOrDefault(user => user.Email == email);
            return found;
        }

        public User Update(User toUpdate)
        {
            _context.Users.Update(toUpdate);
            _context.SaveChanges();
            return toUpdate;
        }

        /*
        public List<User> GetUsersBySomething(Something something)
        {
            IEnumerable<User> users = 
                from patient in _context.Users.Include(user => user.Somethings)
                where patient.Somethings.Any(sth => sth.name == something.name)
                select patient;
            foreach (var user in users)
            {
                Console.WriteLine("hi");
            }
            return users.ToList();
        }

        public User GetUserByUsername(string username)
        {
            User found = _context.Users.SingleOrDefault(user => user.FirstName == username);
            return found;
        }
        */
    }
}