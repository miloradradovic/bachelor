using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IRegistrationRequestService
    {
        public RegistrationRequest Create(RegistrationRequest toCreate);
    }
    
    public class RegistrationRequestService : IRegistrationRequestService
    {
        
        private readonly IRegistrationRequestRepository _registrationRequestRepository;
        
        public RegistrationRequestService(IRegistrationRequestRepository repository)
        {
            _registrationRequestRepository = repository;
        }

        public RegistrationRequest Create(RegistrationRequest toCreate)
        {
            if (_registrationRequestRepository.GetByEmail(toCreate.Email) != null)
            {
                return null;
            }
            
            return _registrationRequestRepository.Create(toCreate);
        }
    }
}