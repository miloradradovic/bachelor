using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{
    
    public interface IUserService
    {
        public User CreateUser(User toCreate);
        /*
        public List<User> GetUsersBySomethings(Something something);

        public User GetUserByUsername(string username);
        */
    }
    
    
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }

        /*
        public List<User> GetUsersBySomethings(Something something)
        {
            return _userRepository.GetUsersBySomething(something);
        }
        */
        public User CreateUser(User toCreate)
        {
            return _userRepository.Create(toCreate);
        }
        /*

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }
        */
    }
}