using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Profession> Professions { get; set; }

        public CategoryDTO ToCategoryDTO()
        {
            return new CategoryDTO()
            {
                Id = Id,
                Name = Name,
                Professions = ManageProfessions()
            };
        }

        private List<string> ManageProfessions()
        {
            List<string> professions = new List<string>();
            foreach (Profession profession in Professions)
            {
                professions.Add(profession.Name);
            }

            return professions;
        }
    }
}