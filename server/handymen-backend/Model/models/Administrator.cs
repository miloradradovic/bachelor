using Model.dto;

namespace Model.models
{
    public class Administrator : Person
    {
        // doesn't have any attributes, his only functionality is crud for other basic entities

        public AdministratorDTO ToDto()
        {
            return new AdministratorDTO()
            {
                Email = Email,
                FirstName = FirstName,
                Id = Id,
                LastName = LastName,
                Password = Password,
                Role = Role.ToString(),
                Verified = Verified
            };
        }
    }
}