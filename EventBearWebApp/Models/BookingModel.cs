using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBearWebApp.Models
{
    public class BookingModel
    {
        public Int64? Customer_ID { get; set; }
        public Int64? RoomAndZone_ID { get; set; }
        public Int64? Booking_ID { get; set; }
        public string Booking_Statua { get; set; }
        public DateTime? Booking_Date { get; set; }
        public DateTime? Booking_EndDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}