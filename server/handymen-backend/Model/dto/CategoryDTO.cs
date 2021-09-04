using System.Collections.Generic;

namespace Model.dto
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Professions { get; set; }
    }
}