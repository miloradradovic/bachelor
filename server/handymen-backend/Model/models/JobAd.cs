using System;
using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class JobAd
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Location Address { get; set; }
        public User Owner { get; set; }
        public AdditionalJobAdInfo AdditionalJobAdInfo { get; set; }
        public DateTime DateWhen { get; set; }
        public virtual List<Trade> Trades { get; set; }
        public virtual List<Picture> Pictures { get; set; }

        public JobAdDTO ToJobAdDTO()
        {
            if (AdditionalJobAdInfo == null)
            {
                return new JobAdDTO()
                {
                    Address = Address.ToLocationDTO(),
                    DateWhen = DateWhen,
                    Description = Description,
                    Id = Id,
                    Title = Title,
                    Trades = ConvertTradesToStrings()
                };
            }
            
            return new JobAdDTO()
            {
                Address = Address.ToLocationDTO(),
                DateWhen = DateWhen,
                Description = Description,
                Id = Id,
                Title = Title,
                Trades = ConvertTradesToStrings(),
                AdditionalJobAdInfo = AdditionalJobAdInfo.ToAdditionalJobAdInfoDTO()
            };
        }

        public JobAdDashboardDTO ToJobAdDashboardDTO()
        {
            if (AdditionalJobAdInfo == null)
            {
                return new JobAdDashboardDTO()
                {
                    Address = Address.Name,
                    DateWhen = DateWhen,
                    Description = Description,
                    Id = Id,
                    MaxPrice = 0,
                    Title = Title,
                    Urgent = "Ne",
                    Pictures = ManagePictures()
                };
            }

            return new JobAdDashboardDTO()
            {
                Address = Address.Name,
                DateWhen = DateWhen,
                Description = Description,
                Id = Id,
                MaxPrice = AdditionalJobAdInfo.PriceMax,
                Title = Title,
                Urgent = ManageUrgent(),
                Pictures = ManagePictures()
            };
        }

        private List<string> ManagePictures()
        {
            List<string> pictures = new List<string>();
            foreach (Picture picture in Pictures)
            {
                pictures.Add(picture.Name);
            }

            return pictures;
        }

        private string ManageUrgent()
        {
            if (AdditionalJobAdInfo.Urgent)
            {
                return "Da";
            }

            return "Ne";
        }

        private List<string> ConvertTradesToStrings()
        {
            List<string> trades = new List<string>();

            foreach (Trade trade in Trades)
            {
                trades.Add(trade.Name);
            }

            return trades;
        }
    }
}