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
    public class AlertBookingController : Controller
    {
        public ActionResult _PartialIndexAlertBooking(int Place_ID)
        {
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat("SELECT RoomAndZone_Name,RoomAndZone_Price,RoomAndZone_Deposit,RoomAndZone_NumberPeople  FROM RoomAndZone WHERE Place_ID = '{0}'; ", Place_ID);
            sql.AppendFormat("SELECT * FROM View_Booking WHERE Place_ID = '{0}'; ", Place_ID);
            IEnumerable<BookingPlaceDetailView> AddBooking = DatabaseUtilities.ExecuteQuery<BookingPlaceDetailView>(sql).ToList();
            return PartialView("_PartialIndexAlertBooking", AddBooking);
        }

        // GET: AlertBooking
        public ActionResult Index()
        {
            var temp = DatabaseUtilities.ExecuteQuery<BookingModel>
             ("Select * from Booking Where Booking_ID = 100059").LastOrDefault();
            return View(temp);
            //return View();
        }

        [HttpPost]
        public ActionResult UpdateBooking(BookingModel model)
        {

            StringBuilder sql = new StringBuilder();
            List<DBParameter> param = new List<DBParameter>();

            if (model.Booking_ID != null && model.Booking_ID > 0)
            {
                var query = new StringBuilder();
                query.Append("UPDATE Booking SET ");
                query.Append("Booking_Statua = @Booking_Statua,");
                query.Append("Booking_Date = @Booking_Date,");
                query.Append("Booking_EndDate = @Booking_EndDate ");
                query.Append("WHERE Booking_ID = @Booking_ID");

                sql.AppendLine(query.ToString());

                param.Add("@Booking_Statua", model.Booking_Statua);
                param.Add("@Booking_Date", model.Booking_Date);
                param.Add("@Booking_EndDate", model.Booking_EndDate);
                param.Add("@Booking_ID", model.Booking_ID);

                DatabaseUtilities.ExecuteNonQuery(sql, param);

            }
            //else
            //{


            //}


            return View("Index");
        }


        //[HttpPost]
        //public string UpdateBooking(int id)
        //{

        //    StringBuilder sql = new StringBuilder();
        //    List<DBParameter> param = new List<DBParameter>();


        //    if (!string.IsNullOrWhiteSpace(id.ToSafeString()))
        //    {

        //        var query = new StringBuilder();
        //        query.Append("UPDATE Booking SET ");
        //        query.Append("Booking_Statua,");
        //        query.Append("Booking_Date,");
        //        query.Append("Booking_EndDate )");
        //        query.Append("WHERE Booking_ID = @Booking_ID");

        //        sql.AppendLine(query.ToString());

        //        param.Add("@Booking_Statua", id);
        //        param.Add("@Booking_Date", id);
        //        param.Add("@Booking_EndDate", id);
        //        param.Add("@Booking_ID", id);

        //        DatabaseUtilities.ExecuteNonQuery(sql, param);

        //    }

        //    return "SUCCESS";
        //}

        [HttpPost]
        public string DeleteAlertBooking(int id)
        {

            StringBuilder sql = new StringBuilder();
            List<DBParameter> param = new List<DBParameter>();

            if (id > 0)
            {

                var query = new StringBuilder();
                query.Append("DELETE FROM Booking ");
                query.Append("WHERE Booking_ID = @Booking_ID");

                sql.AppendLine(query.ToString());

                param.Add("@Booking_ID", id);

                DatabaseUtilities.ExecuteNonQuery(sql, param);

            }

            return "SUCCESS";
        }

    }
}