using DataAccessLayer.repositories;
using Model.models;
using BC = BCrypt.Net.BCrypt;

namespace BusinessLogicLayer.services
{

    public interface IAdministratorService
    {
        public Administrator GetById(int id);
        public Administrator GetByEmailAndPassword(string email, string password);
        public Administrator GetByEmail(string email);
        public ApiResponse Create(Administrator toCreate);
    }
    
    public class AdministratorService : IAdministratorService
    {
        private readonly IAdministratorRepository _administratorRepository;
        
        public AdministratorService(IAdministratorRepository repository)
        {
            _administratorRepository = repository;
        }

        public Administrator GetById(int id)
        {
            return _administratorRepository.GetById(id);
        }

        public Administrator GetByEmailAndPassword(string email, string password)
        {
            return _administratorRepository.GetByEmailAndPassword(email, password);
        }

        public Administrator GetByEmail(string email)
        {
            return _administratorRepository.GetByEmail(email);
        }

        public ApiResponse Create(Administrator toCreate)
        {
            toCreate.Verified = true;
            toCreate.Password = BC.HashPassword(toCreate.Password);
            Administrator created = _administratorRepository.Create(toCreate);
            if (created == null)
            {
                return new ApiResponse()
                {
                    Message =
                        "Nesto se desilo sa bazom podataka prilikom kreiranja administratora. Molimo pokusajte ponovo kasnije.",
                    ResponseObject = null,
                    Status = 400
                };
            }

            return new ApiResponse()
            {
                Message = "Uspesno kreiran administrator.",
                ResponseObject = created.ToDto(),
                Status = 201
            };
        }
    }
}