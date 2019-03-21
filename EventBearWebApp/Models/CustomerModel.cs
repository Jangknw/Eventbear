using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventBearWebApp.Models
{
    public class CustomerModel
    {
        
        public string CustomerType_ID { get; set; }
        public Int64? Customer_ID { get; set; }
        public string Customer_Name { get; set; }       
        public string Customer_Lastname { get; set; }
        public string Customer_Tel { get; set; }
        public string Customer_Email { get; set; }
        public string Customer_Password { get; set; }
        public string Customer_ConfirmPassword { get; set; }
        public string Customer_Address { get; set; }
        public string Customer_Alley { get; set; }
        public string Customer_Road { get; set; }
        public string Customer_Province { get; set; }
        public string Customer_District { get; set; }
        public string Customer_SubDistrict { get; set; }
        public string Customer_Zipcode { get; set; }     
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
    }
}