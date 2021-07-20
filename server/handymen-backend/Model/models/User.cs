using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class User : Person
    {
        public int Age { get; set; }
        
        public virtual List<Something> Somethings { get; set; }

        public User(string firstName, string lastName, int age) : base(firstName, lastName)
        {
            Age = age;
        }

        public User(int id, string firstName, string lastName, int age) : base(id, firstName, lastName)
        {
            Age = age;
        }
        
        public User(int id, string firstName, string lastName, int age, List<Something> somethings) : base(id, firstName, lastName)
        {
            Age = age;
            Somethings = somethings;
        }

        public UserDTO toDto()
        {
            return new UserDTO(Id, FirstName, LastName, Age, Somethings);
        }

        public UserDTO toDto2()
        {
            return new UserDTO(Id, FirstName, LastName, Age);
        }
    }
}