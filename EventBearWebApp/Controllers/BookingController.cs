using DatabaseLibrary;
using EventBearWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EventBearWebApp.Controllers
{
    public class BookingController : Controller
    {
        public ActionResult _PartialIndexBooking(int Place_ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT RoomAndZone_Name,RoomAndZone_Price,RoomAndZone_Deposit,RoomAndZone_NumberPeople  FROM RoomAndZone WHERE Place_ID = '{0}'; ", Place_ID);
            IEnumerable<RoomAndZoneModel> roomAndZone = DatabaseUtilities.ExecuteQuery<RoomAndZoneModel>(sql).ToList();
            return PartialView("_PartialIndexBooking", roomAndZone);
        }



        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult Booking(BookingModel model)
        //{
        //    {
        //        Init();

        //        StringBuilder sql = new StringBuilder();
        //        List<DBParameter> param = new List<DBParameter>();

        //        {
        //            var query = new StringBuilder();
        //            query.Append("INSERT INTO Booking (");
        //            query.Append("Customer_ID,");
        //            query.Append("RoomAndZone_ID,");
        //            query.Append("Booking_ID,");
        //            query.Append("Booking_Statua,");
        //            query.Append("Booking_Date,");
        //            query.Append("Booking_EndDate )");


        //            var queryParam = new StringBuilder();
        //            queryParam.Append(" VALUES (");
        //            queryParam.Append("@Customer_ID,");
        //            queryParam.Append("@RoomAndZone_ID,");
        //            queryParam.Append("@Booking_ID,");
        //            queryParam.Append("@Booking_Statua,");
        //            queryParam.Append("@Booking_Date,");
        //            queryParam.Append("@Booking_EndDate )");


        //            sql.AppendLine(query.ToString());
        //            sql.AppendLine(queryParam.ToString());

        //            param.Add("@Customer_ID", 10000);
        //            param.Add("@RoomAndZone_ID", model.RoomAndZone_ID);
        //            param.Add("@Booking_ID", model.Booking_ID);
        //            param.Add("@Booking_Statua", model.Booking_Statua);
        //            param.Add("@Booking_Date", model.Booking_Date);
        //            param.Add("@Booking_EndDate", model.Booking_EndDate.ToSafeString());
        //            //param.Add("@RoomAndZone_ID", model.RoomAndZone_ID);                    

        //            DatabaseUtilities.ExecuteNonQuery(sql, param);
        //        }
        //    }
        //}


    }
}