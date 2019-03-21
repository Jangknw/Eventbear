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
    public class AlertFavoriteController : Controller
    {
        public ActionResult _PartialIndexAlertFavorite(int Place_ID)
        {
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat("SELECT RoomAndZone_Name,RoomAndZone_Price,RoomAndZone_Deposit,RoomAndZone_NumberPeople  FROM RoomAndZone WHERE Place_ID = '{0}'; ", Place_ID);
            sql.AppendFormat("SELECT * FROM View_Favorite WHERE Place_ID = '{0}'; ", Place_ID);
            IEnumerable<FavoritePlaceViewModel> AlertFavorite = DatabaseUtilities.ExecuteQuery<FavoritePlaceViewModel>(sql).ToList();
            return PartialView("_PartialIndexAlertFavorite", AlertFavorite);
        }
      
        // GET: AlertFavorite
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult DeleteAlertFavorite(FavoritePlaceViewModel model)
        //{
        //    //StringBuilder sql = new StringBuilder();          
        //    //sql.AppendFormat("DELETE FROM Favorite WHERE Favorite_ID ='{0}'; ", model.Favorite_ID);
        //    //IEnumerable<FavoritePlaceViewModel> DeleteFavorite = DatabaseUtilities.ExecuteQuery<FavoritePlaceViewModel>(sql).ToList();          
        //    //return Json(new { success = true });
        //}


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


    }
}