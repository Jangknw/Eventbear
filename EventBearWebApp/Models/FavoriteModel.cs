using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBearWebApp.Models
{
    public class FavoriteModel
    {
        public Int64? Customer_ID { get; set; }
        public Int64? RoomAndZone_ID { get; set; }
        public Int64? Favorite_ID { get; set; }       
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}