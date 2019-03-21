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
    public class FavoriteController : Controller
    {
        public ActionResult _PartialIndexFavorite(int Customer_ID)
        {
            StringBuilder sql = new StringBuilder();          
            sql.AppendFormat("SELECT * FROM View_Favorite WHERE Customer_ID = '{0}'; ", Customer_ID);
            IEnumerable<FavoritePlaceViewModel> Favorite = DatabaseUtilities.ExecuteQuery<FavoritePlaceViewModel>(sql).ToList();
            return PartialView("_PartialIndexFavorite", Favorite);
        }
     
        // GET: Favorite
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string DeleteAlertFavorite(int id)
        {

            StringBuilder sql = new StringBuilder();
            List<DBParameter> param = new List<DBParameter>();

            if (id > 0)
            {

                var query = new StringBuilder();
                query.Append("DELETE FROM Favorite ");
                query.Append("WHERE Favorite_ID = @Favorite_ID");

                sql.AppendLine(query.ToString());

                param.Add("@Favorite_ID", id);

                DatabaseUtilities.ExecuteNonQuery(sql, param);

            }

            return "SUCCESS";
        }

        [HttpPost]
        public string InsertBooking(int id)
        {

            StringBuilder sql = new StringBuilder();
            List<DBParameter> param = new List<DBParameter>();


            if (!string.IsNullOrWhiteSpace(id.ToSafeString()))
            {
                var query = new StringBuilder();
                query.Append("INSERT INTO Booking (");
                query.Append("Customer_ID,");
                query.Append("RoomAndZone_ID )");

                var queryParam = new StringBuilder();
                queryParam.Append(" VALUES (");
                queryParam.Append("@Customer_ID,");
                queryParam.Append("@RoomAndZone_ID )");

                sql.AppendLine(query.ToString());
                sql.AppendLine(queryParam.ToString());

                param.Add("@Customer_ID", 100002);
                param.Add("@RoomAndZone_ID", id);

                DatabaseUtilities.ExecuteNonQuery(sql, param);

            }

            return "SUCCESS";
        }

    }
}