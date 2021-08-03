using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IAdministratorService
    {
        public Administrator GetById(int id);
        public Administrator GetByEmailAndPassword(string email, string password);
        public Administrator GetByEmail(string email);
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
    }
}