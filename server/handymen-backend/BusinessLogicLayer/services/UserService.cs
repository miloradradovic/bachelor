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
        public ApiResponse RegisterUser(User request);
        public ApiResponse VerifyUser(string encrypted);
    }
    
    
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly ICryptingService _cryptingService;

        public UserService(IUserRepository repository, IMailService mailService,
            ICryptingService cryptingService)
        {
            _userRepository = repository;
            _mailService = mailService;
            _cryptingService = cryptingService;
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return _userRepository.GetByEmailAndPassword(email, password);
        }

        public ApiResponse RegisterUser(User request)
        {
            
            request.Password = BC.HashPassword(request.Password);

            User created = _userRepository.Create(request);

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
                Body = "Greetings " + created.FirstName + ". Please verify your account by following this <a href='https://localhost:5001/api/users/verify/" + _cryptingService.Encrypt(created.Id.ToString()) + "'>LINK</a>",
                Subject = "Account verification",
                ToEmail = created.Email
            });
            
            return new ApiResponse()
            {
                Message = "Registration request successfully created. Verification link has been sent to your email.",
                ResponseObject = created.ToDtoWithoutLists(),
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
                    Status = 400
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
                ResponseObject = updated.ToDtoWithoutLists(),
                Status = 200
            };


        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }
    }
}