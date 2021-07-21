using System.Collections.Generic;

namespace Model.models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Profession> Professions { get; set; }
    }
}