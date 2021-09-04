using System;
using System.Collections.Generic;
using Model.models;

namespace Model.dto
{
    public class JobAdDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public LocationDTO Address { get; set; }
        public AdditionalJobAdInfoDTO AdditionalJobAdInfo { get; set; }
        public DateTime DateWhen { get; set; }
        public List<string> Trades { get; set; }
        public List<string> Pictures { get; set; }
 
        public JobAd ToJobAd()
        {
            if (AdditionalJobAdInfo == null)
            {
                return new JobAd()
                {
                    Id = Id,
                    Address = Address.ToLocation(),
                    DateWhen = DateWhen,
                    Description = Description,
                    Title = Title
                };
            }
            
            return new JobAd()
            {
                Id = Id,
                Address = Address.ToLocation(),
                DateWhen = DateWhen,
                Description = Description,
                Title = Title,
                AdditionalJobAdInfo = AdditionalJobAdInfo.ToAdditionalJobAdInfo()
            };
        }
    }
}