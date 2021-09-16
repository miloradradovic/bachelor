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
        public ApiResponse EditProfile(User toUpdate);
    }
    
    
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly ICryptingService _cryptingService;
        private readonly ILocationService _locationService;

        public UserService(IUserRepository repository, IMailService mailService,
            ICryptingService cryptingService, ILocationService locationService)
        {
            _userRepository = repository;
            _mailService = mailService;
            _cryptingService = cryptingService;
            _locationService = locationService;
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
            ApiResponse response = new ApiResponse();
            request.Password = BC.HashPassword(request.Password);

            Location foundLocation =
                _locationService.GetByLatAndLng(request.Address.Latitude, request.Address.Longitude);

            if (foundLocation != null)
            {
                request.Address = foundLocation;
            }

            User created = _userRepository.Create(request);

            if (created == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom registracije. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }
            
            _mailService.SendEmail(new MailRequest()
            {
                Body = "Pozdrav " + created.FirstName + "!<br>Molimo verifikujte Vas nalog klikom na sledeci <a href='https://localhost:4200/?id=" + _cryptingService.Encrypt(created.Id.ToString()) + "'>LINK</a>.",
                Subject = "Verifikacija naloga",
                ToEmail = created.Email
            });

            response.RegisteredUser(created,
                "Zahtev za registraciju je uspesno kreiran. Na Vas email poslali smo Vam verifikacioni link.", 201);
            return response;
        }

        public ApiResponse VerifyUser(string encrypted)
        {
            ApiResponse response = new ApiResponse();
            int decryptedId;
            bool canParse = int.TryParse(_cryptingService.Decrypt(encrypted), out decryptedId);
            if (!canParse)
            {
                response.SetError("Nesto nije u redu sa kriptovanim id. Molimo pokusajte ponovo.", 400);
                return response;
            }
            
            User toVerify = _userRepository.GetById(decryptedId);

            if (toVerify == null)
            {
                response.SetError("Vas nalog nije pronadjen.", 400);
                return response;
            }

            if (toVerify.Verified)
            {
                response.SetError("Vec ste verifikovali nalog.", 400);
                return response;
            }

            toVerify.Verified = true;
            User updated = _userRepository.Update(toVerify);

            if (updated == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom azuriranja Vaseg naloga. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }

            response.VerifiedUser(updated, "Uspesno ste verifikovali nalog. Sada mozete da se ulogujete!", 200);
            return response;
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public ApiResponse EditProfile(User toUpdate)
        {
            ApiResponse response = new ApiResponse();
            User foundUser = GetById(toUpdate.Id);
            Location foundLocation = _locationService.GetByLatAndLng(toUpdate.Address.Latitude, toUpdate.Address.Longitude);
            if (foundLocation != null)
            {
                toUpdate.Address = foundLocation;
            }
            foundUser.Address = toUpdate.Address;
            foundUser.Email = toUpdate.Email;
            foundUser.FirstName = toUpdate.FirstName;
            foundUser.LastName = toUpdate.LastName;
            User updated = _userRepository.Update(foundUser);
            if (updated == null)
            {
                response.SetError("Nesto se desilo sa bazom podataka prilikom azuriranja Vaseg profila. Molimo pokusajte ponovo kasnije.", 400);
                return response;
            }

            response.UpdatedUserProfile(updated,
                "Uspesno azuriran profil. Molimo da se ulogujete ponovo da bi promene imale efekat.", 200);
            return response;
        }
    }
}