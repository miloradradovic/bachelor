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
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<string> Trades { get; set; }

        public JobAd ToJobAd()
        {
            if (AdditionalJobAdInfo == null)
            {
                return new JobAd()
                {
                    Id = Id,
                    Address = Address.ToLocation(),
                    DateFrom = DateFrom,
                    DateTo = DateTo,
                    Description = Description,
                    Title = Title
                };
            }
            
            return new JobAd()
            {
                Id = Id,
                Address = Address.ToLocation(),
                DateFrom = DateFrom,
                DateTo = DateTo,
                Description = Description,
                Title = Title,
                AdditionalJobAdInfo = AdditionalJobAdInfo.ToAdditionalJobAdInfo()
            };
        }
    }
}