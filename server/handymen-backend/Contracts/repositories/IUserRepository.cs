using Model.models;

namespace Contracts.repositories
{
    public interface IUserRepository
    {
        public User Create(User toCreate);
    }
}