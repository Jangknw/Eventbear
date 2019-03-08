﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBearWebApp.Models
{
    public class PlaceAndPlaceTypeModel
    {
        public Int64? Customer_ID { get; set; }
        public string PlaceType_ID { get; set; }
        public Int64? Place_ID { get; set; }
        public string Place_Name { get; set; }
        public string Place_Address { get; set; }
        public string Place_Alley { get; set; }
        public string Place_Road { get; set; }
        public string Place_District { get; set; }
        public string Place_SubDistrict { get; set; }
        public string Place_Province { get; set; }
        public string Place_Zipcode { get; set; }
        public string Place_Tel { get; set; }
        public string Place_Email { get; set; }   
        public string PlaceType_Name { get; set; }

    }
}