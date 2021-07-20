using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.models;

namespace DataAccessLayer.repositories
{
    public interface IUserRepository
    {
        public User Create(User toCreate);

        public List<User> GetUsersBySomething(Something something);

        public User GetUserByUsername(string username);
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
    }
}