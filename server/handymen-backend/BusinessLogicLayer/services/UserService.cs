using System;
using DataAccessLayer.repositories;
using Model.models;
using BC = BCrypt.Net.BCrypt;

namespace BusinessLogicLayer.services
{
    
    public interface IUserService
    {
        public User GetById(int id);
        public User GetByEmailAndPassword(string email, string password);
        public User GetByEmail(string email);
        public ApiResponse RegisterUser(RegistrationRequest request);
        public ApiResponse VerifyUser(string encrypted);
    }
    
    
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly ICryptingService _cryptingService;
        private readonly IAdministratorService _administratorService;
        private readonly IHandymanService _handymanService;

        public UserService(IUserRepository repository, IMailService mailService,
            ICryptingService cryptingService, IAdministratorService administratorService, IHandymanService handymanService)
        {
            _userRepository = repository;
            _mailService = mailService;
            _cryptingService = cryptingService;
            _administratorService = administratorService;
            _handymanService = handymanService;
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetByEmailAndPassword(email, password);
        }

        public ApiResponse RegisterUser(RegistrationRequest request)
        {
            User findUserByEmail = _userRepository.GetByEmail(request.Email);
            Administrator findAdministratorByEmail = _administratorService.GetByEmail(request.Email);
            HandyMan findHandymanByEmail = _handymanService.GetByEmail(request.Email);
            if (findUserByEmail != null || findAdministratorByEmail != null || findHandymanByEmail != null)
            {
                return new ApiResponse()
                {
                    Message = "Entered email is taken.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            User created = _userRepository.Create(new User()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = BC.HashPassword(request.Password),
                Role = Role.USER,
                Verified = false
            });

            if (created == null)
            {
                return new ApiResponse()
                {
                    Message = "Something went wrong with the database. Please, try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Please verify your account. Verification link: https://localhost:5001/users/verify/" + _cryptingService.Encrypt(created.Id.ToString()),
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

        public ApiResponse VerifyUser(string encrypted)
        {
            int decryptedId;
            bool canParse = int.TryParse(_cryptingService.Decrypt(encrypted), out decryptedId);
            if (!canParse)
            {
                return new ApiResponse()
                {
                    Message = "Something is wrong with encrypted id. Please try again.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            
            User toVerify = _userRepository.GetById(decryptedId);

            if (toVerify == null)
            {
                return new ApiResponse()
                {
                    Message = "Could not find your account.",
                    ResponseObject = null,
                    Status = 404
                };
            }

            toVerify.Verified = true;
            User updated = _userRepository.Update(toVerify);

            if (updated == null)
            {
                return new ApiResponse()
                {
                    Message = "Something went wrong while updating your account. Please try again later.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            return new ApiResponse()
            {
                Message = "Successfully verified your account. Now you can log in!",
                ResponseObject = updated,
                Status = 200
            };


        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }
    }
}