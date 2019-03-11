using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBearWebApp.Models
{
    public class FavoritePlaceViewModel
    {
        public string Name { get; set; }      
        public Int64? Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Lastname { get; set; }
        public string Customer_Tel { get; set; }      
        public Int64? RoomAndZone_ID { get; set; }
        public Int64? Favorite_ID { get; set; }  
        public string RoomAndZone_Name { get; set; }
        public int RoomAndZone_Price { get; set; }
        public int RoomAndZone_Deposit { get; set; }
        public string RoomAndZone_NumberPeople { get; set; }
        public string RoomAndZone_Note { get; set; }
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
    }
}