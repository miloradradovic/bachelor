using System.Collections.Generic;
using Model.models;

namespace Model.dto
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public List<Something> Somethings { get; set; }
        
        public UserDTO() {}

        public UserDTO(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public UserDTO(int id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public UserDTO(int id, string firstName, string lastName, int age, List<Something> somethings)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Somethings = somethings;
        }

        public User toEntity()
        {
            if (Id == 0)
            {
                return new User(FirstName, LastName, Age);
            }

            return new User(Id, FirstName, LastName, Age);
        }
    }
}