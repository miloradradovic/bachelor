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
        public HandyMan GetHandymanById(int id);
        public Person GetById(int id);
        public HandyMan UpdateHandyman(HandyMan toUpdate);
        public ApiResponse GetAllHandymen();
        public ApiResponse SearchHandymen(SearchParams searchParams);
        public ApiResponse GetHandymanByIdApiResponse(int handymanId);
        public ApiResponse EditProfile(Person person, List<string> trades, string type);
        public ApiResponse GetUnverifiedHandymen();
        public ApiResponse GetHandymenByProfessionName(string professionName);
        public ApiResponse FilterHandymen(SearchParams searchParams);

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

        public ApiResponse GetHandymenByProfessionName(string professionName)
        {
            return _handymanService.GetByProfessionName(professionName);
        }

        public Person GetById(int id)
        {
            Administrator foundAdmin = _administratorService.GetById(id);
            if (foundAdmin != null)
            {
                return foundAdmin;
            }

            HandyMan foundHandy = _handymanService.GetById(id);
            if (foundHandy != null)
            {
                return foundHandy;
            }

            User foundUser = _userService.GetById(id);
            return foundUser;
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
                ApiResponse response = new ApiResponse();
                response.SetError("Uneti email je zauzet.", 400);
                return response;
            }
            return _handymanService.Register(toRegister, trades);
        }

        public ApiResponse RegisterUser(User toRegister)
        {
            if (GetByEmail(toRegister.Email) != null)
            {
                ApiResponse response = new ApiResponse();
                response.SetError("Uneti email je zauzet.", 400);
                return response;
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
                ApiResponse response = new ApiResponse();
                response.SetError("Uneti email je zauzet.", 400);
                return response;
            }

            return _administratorService.Create(toCreate);
        }

        public User GetUserById(int id)
        {
            return _userService.GetById(id);
        }

        public HandyMan GetHandymanById(int id)
        {
            return _handymanService.GetById(id);
        }

        public HandyMan UpdateHandyman(HandyMan toUpdate)
        {
            return _handymanService.Update(toUpdate);
        }

        public ApiResponse GetAllHandymen()
        {
            return _handymanService.GetAll();
        }

        public ApiResponse SearchHandymen(SearchParams searchParams)
        {
            return _handymanService.Search(searchParams);
        }

        public ApiResponse FilterHandymen(SearchParams searchParams)
        {
            return _handymanService.Filter(searchParams);
        }

        public ApiResponse GetHandymanByIdApiResponse(int handymanId)
        {
            ApiResponse response = new ApiResponse();
            HandyMan found = _handymanService.GetById(handymanId);
            if (found == null)
            {
                response.SetError("Majstor sa tim id nije pronadjen.", 400);
                return response;
            }

            List<Rating> ratings = new List<Rating>();
            foreach (Rating rating in found.Ratings)
            {
                if (rating.Verified)
                {
                    Rating fullRating = _handymanService.GetDetailedRatingProfile(rating.Id);
                    ratings.Add(fullRating);
                }
            }

            found.Ratings = ratings;

            response.GotDetailedHandyman(found, "Uspesno dobavljen majstor.", 200);
            return response;
        }

        public ApiResponse EditProfile(Person person, List<string> trades, string type)
        {
            Person foundPerson = GetById(person.Id);
            if (foundPerson.Email != person.Email)
            {
                Person byEmail = GetByEmail(person.Email);
                if (byEmail != null)
                {
                    ApiResponse response = new ApiResponse();
                    response.SetError("Uneti email je zauzet.", 400);
                    return response;
                }
            }

            if (type == "handyman")
            {
                return _handymanService.EditProfile((HandyMan) person, trades);
            }

            return _userService.EditProfile((User) person);

        }

        public ApiResponse GetUnverifiedHandymen()
        {
            return _handymanService.GetUnverifiedHandymen();
        }
    }
}