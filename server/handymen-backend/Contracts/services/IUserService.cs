using Model.models;

namespace Contracts.services
{
    public interface IUserService
    {
        public User CreateUser(User toCreate);
    }
}