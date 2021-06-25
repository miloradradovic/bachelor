using Contracts.repositories;
using Contracts.services;
using Model.models;

namespace Services.services
{
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }
        
        public User CreateUser(User toCreate)
        {
            return _userRepository.Create(toCreate);
        }
    }
}