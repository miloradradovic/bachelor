using DataAccessLayer.repositories;
using Model.models;

namespace BusinessLogicLayer.services
{

    public interface IHandymanService
    {
        public HandyMan GetById(int id);
        public HandyMan GetByEmailAndPassword(string email, string password);
        public HandyMan GetByEmail(string email);
    }
    
    public class HandymanService : IHandymanService
    {
        private readonly IHandymanRepository _handymanRepository;

        public HandymanService(IHandymanRepository handymanRepository)
        {
            _handymanRepository = handymanRepository;
        }

        public HandyMan GetById(int id)
        {
            return _handymanRepository.GetById(id);
        }

        public HandyMan GetByEmailAndPassword(string email, string password)
        {
            return _handymanRepository.GetByEmailAndPassword(email, password);
        }

        public HandyMan GetByEmail(string email)
        {
            return _handymanRepository.GetByEmail(email);
        }
    }
}