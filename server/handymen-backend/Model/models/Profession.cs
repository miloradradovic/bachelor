using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Trade> Trades { get; set; }

        public ProfessionDTO ToProfessionDTO()
        {
            return new ProfessionDTO()
            {
                Id = Id,
                Name = Name
            };
        }
    }
}