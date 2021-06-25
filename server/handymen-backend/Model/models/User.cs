using Model.dto;

namespace Model.models
{
    public class User : Person
    {
        public int Age { get; set; }

        public User(string firstName, string lastName, int age) : base(firstName, lastName)
        {
            Age = age;
        }

        public User(int id, string firstName, string lastName, int age) : base(id, firstName, lastName)
        {
            Age = age;
        }

        public UserDTO toDto()
        {
            return new UserDTO(Id, FirstName, LastName, Age);
        }
    }
}