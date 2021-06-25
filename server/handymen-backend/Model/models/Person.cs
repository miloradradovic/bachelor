namespace Model.models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        protected Person()
        {
        }

        protected Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        protected Person(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}