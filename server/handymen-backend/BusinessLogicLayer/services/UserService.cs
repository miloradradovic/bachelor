using System.Collections.Generic;
using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{
    
    public interface IUserService
    {
        public User CreateUser(User toCreate);
        public User GetById(int id);
        public User GetByEmailAndPassword(string email, string password);
        public ApiResponse CreateRegistrationRequest(RegistrationRequest request);
        /*
        public List<User> GetUsersBySomethings(Something something);

        public User GetUserByUsername(string username);
        */
    }
    
    
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRegistrationRequestService _registrationRequestService;
        private readonly IMailService _mailService;

        public UserService(IUserRepository repository, IRegistrationRequestService requestService, IMailService mailService)
        {
            _userRepository = repository;
            _registrationRequestService = requestService;
            _mailService = mailService;
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

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetByEmailAndPassword(email, password);
        }

        public ApiResponse CreateRegistrationRequest(RegistrationRequest request)
        {
            RegistrationRequest created = _registrationRequestService.Create(request);
            if (created == null)
            {
                return new ApiResponse()
                {
                    Message = "Registration request with that email already exists. Please verify your account.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Please verify your account. Verification link: https://localhost:5001/users/verify?id=" + created.Id,
                Subject = "Account verification",
                ToEmail = created.Email
            });
            
            return new ApiResponse()
            {
                Message = "Registration request successfully created. Verification link has been sent to your email.",
                ResponseObject = created,
                Status = 201
            };
        }

        /*

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetUserByUsername(username);
        }
        */
    }
}