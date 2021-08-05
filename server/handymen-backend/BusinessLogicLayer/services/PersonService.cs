using System.Collections.Generic;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IPersonService
    {
        public Person GetByEmailAndPassword(string email, string password);
        public ApiResponse RegisterUser(User toRegister);
        public ApiResponse VerifyUser(string encrypted);
        public ApiResponse RegisterHandyman(HandyMan toRegister, List<string> trades);
        public Person GetByEmail(string email);
        public ApiResponse VerifyHandyman(HandymanVerificationData handymanVerificationData);
        public ApiResponse CreateAdministrator(Administrator toCreate);
        public User GetUserById(int id);

    }
    
    public class PersonService : IPersonService
    {
        private readonly IAdministratorService _administratorService;
        private readonly IHandymanService _handymanService;
        private readonly IUserService _userService;

        public PersonService(IAdministratorService administratorService, IHandymanService handymanService,
            IUserService userService)
        {
            _administratorService = administratorService;
            _handymanService = handymanService;
            _userService = userService;
        }

        public Person GetByEmailAndPassword(string email, string password)
        {
            Administrator foundAdmin = _administratorService.GetByEmailAndPassword(email, password);
            if (foundAdmin != null)
            {
                return foundAdmin;
            }

            HandyMan foundHandy = _handymanService.GetByEmailAndPassword(email, password);
            if (foundHandy != null)
            {
                return foundHandy;
            }

            User foundUser = _userService.GetByEmailAndPassword(email, password);
            return foundUser;
        }

        public Person GetByEmail(string email)
        {
            Administrator foundAdmin = _administratorService.GetByEmail(email);
            if (foundAdmin != null)
            {
                return foundAdmin;
            }

            HandyMan foundHandy = _handymanService.GetByEmail(email);
            if (foundHandy != null)
            {
                return foundHandy;
            }

            User foundUser = _userService.GetByEmail(email);
            return foundUser;
        }

        public ApiResponse RegisterHandyman(HandyMan toRegister, List<string> trades)
        {
            if (GetByEmail(toRegister.Email) != null)
            {
                return new ApiResponse()
                {
                    Message = "Entered email is taken.",
                    ResponseObject = null,
                    Status = 400
                };
            }
            return _handymanService.Register(toRegister, trades);
        }

        public ApiResponse RegisterUser(User toRegister)
        {
            if (GetByEmail(toRegister.Email) != null)
            {
                return new ApiResponse()
                {
                    Message = "Entered email is taken.",
                    ResponseObject = null,
                    Status = 400
                };
            }   
            return _userService.RegisterUser(toRegister);
        }

        public ApiResponse VerifyUser(string encrypted)
        {
            return _userService.VerifyUser(encrypted);
        }

        public ApiResponse VerifyHandyman(HandymanVerificationData handymanVerificationData)
        {
            return _handymanService.VerifyHandyman(handymanVerificationData);
        }

        public ApiResponse CreateAdministrator(Administrator toCreate)
        {
            Person foundByEmail = GetByEmail(toCreate.Email);
            if (foundByEmail != null)
            {
                return new ApiResponse()
                {
                    Message = "Entered email is already taken.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            return _administratorService.Create(toCreate);
        }

        public User GetUserById(int id)
        {
            return _userService.GetById(id);
        }
    }
}