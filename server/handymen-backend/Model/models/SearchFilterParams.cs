﻿using System.Collections.Generic;
using Model.dto;

namespace Model.models
{
    public class SearchFilterParams
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Trades { get; set; }
        public int AvgRatingFrom { get; set; }
        public int AvgRatingTo { get; set; }
        public string Address { get; set; }
        public List<HandymanDashboardDTO> Handymen { get; set; }
    }
}