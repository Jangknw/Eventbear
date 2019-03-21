using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBearWebApp.Models
{
    public class RoomAndZoneModel
    {
        public Int64? Place_ID { get; set; }
        public Int64? RoomAndZone_ID { get; set; }
        public string RoomAndZone_Name { get; set; }
        public string RoomAndZone_Price { get; set; }
        public string RoomAndZone_Deposit { get; set; }
        public string RoomAndZone_NumberPeople { get; set; }
        public string RoomAndZone_Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }


    }
}