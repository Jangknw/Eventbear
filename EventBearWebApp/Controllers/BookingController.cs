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
        public ActionResult _PartialIndexBooking(int Customer_ID)
        {
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat("SELECT RoomAndZone_Name,RoomAndZone_Price,RoomAndZone_Deposit,RoomAndZone_NumberPeople  FROM RoomAndZone WHERE Place_ID = '{0}'; ", Place_ID);
            sql.AppendFormat("SELECT * FROM View_Booking WHERE Customer_ID = '{0}'; ", Customer_ID);
            IEnumerable<BookingPlaceDetailView> Booking = DatabaseUtilities.ExecuteQuery<BookingPlaceDetailView>(sql).ToList();
            return PartialView("_PartialIndexBooking", Booking);
        }



        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult AddBooking(BookingModel model)
        {
            
            StringBuilder sql = new StringBuilder();
            List<DBParameter> param = new List<DBParameter>();

            if (ModelState.IsValid)
            {
                if (model.Booking_ID != null && model.Booking_ID > 0)
                {
                    //int rowAffect = DatabaseUtilities.ExecuteNonQuery(String.Format(@" UPDATE Place SET Place_Name = '{0}' where Place_ID = {1}", model.Place_Name, model.Place_ID));
                    var query = new StringBuilder();
                    query.Append("UPDATE Booking SET ");
                    query.Append("Customer_ID,");
                    query.Append("RoomAndZone_ID,");
                    query.Append("Booking_ID,");
                    query.Append("Booking_Statua,");
                    query.Append("Booking_Date,");
                    query.Append("Booking_EndDate )");
             

                    sql.AppendLine(query.ToString());

                    param.Add("@Customer_ID", 100002);
                    param.Add("@RoomAndZone_ID", model.RoomAndZone_ID);
                    param.Add("@Booking_Statua", model.Booking_Statua);
                    param.Add("@Booking_Date", model.Booking_Date);
                    param.Add("@Booking_EndDate", model.Booking_EndDate.ToSafeString());                  

                    DatabaseUtilities.ExecuteNonQuery(sql, param);

                }
                else
                {
                    //sql.AppendLine(" INSERT INTO Place (Customer_ID,PlaceType_ID, Place_Name, Place_Address, Place_Alley, Place_Road, Place_Province, Place_District,Place_SubDistrict ,Place_Zipcode,Place_Tel) ");
                    //sql.AppendLine(" VALUES(@Customer_ID,@PlaceType_ID, @Place_Name, @Place_Address, @Place_Alley, @Place_Road,@Place_Province,@Place_District, @Place_SubDistrict ,@Place_Zipcode,@Place_Tel  ) ");
                    //param.Add("@Customer_ID", model.Customer_ID);

                    var query = new StringBuilder();
                    query.Append("INSERT INTO Booking (");
                    query.Append("Customer_ID,");
                    query.Append("RoomAndZone_ID,");
                    query.Append("Booking_ID,");
                    query.Append("Booking_Statua,");
                    query.Append("Booking_Date,");
                    query.Append("Booking_EndDate )");


                    var queryParam = new StringBuilder();
                    queryParam.Append(" VALUES (");
                    queryParam.Append("@Customer_ID,");
                    queryParam.Append("@RoomAndZone_ID,");
                    queryParam.Append("@Booking_Statua,");
                    queryParam.Append("@Booking_Date,");
                    queryParam.Append("@Booking_EndDate )");


                    sql.AppendLine(query.ToString());
                    sql.AppendLine(queryParam.ToString());

                    param.Add("@Customer_ID", 100002);
                    param.Add("@RoomAndZone_ID", model.RoomAndZone_ID);
                    param.Add("@Booking_Statua", model.Booking_Statua);
                    param.Add("@Booking_Date", model.Booking_Date);
                    param.Add("@Booking_EndDate", model.Booking_EndDate.ToSafeString());
                    //param.Add("@RoomAndZone_ID", model.RoomAndZone_ID);                    

                    DatabaseUtilities.ExecuteNonQuery(sql, param);

                }

            }

            return View("Index");
        }


    }
}