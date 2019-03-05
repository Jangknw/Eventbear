using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBearWebApp.Models
{
    public class DistrictModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}