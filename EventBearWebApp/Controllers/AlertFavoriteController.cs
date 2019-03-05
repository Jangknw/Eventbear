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
            sql.AppendFormat("SELECT RoomAndZone_Name,RoomAndZone_Price,RoomAndZone_Deposit,RoomAndZone_NumberPeople  FROM RoomAndZone WHERE Place_ID = '{0}'; ", Place_ID);
            IEnumerable<RoomAndZoneModel> roomAndZone = DatabaseUtilities.ExecuteQuery<RoomAndZoneModel>(sql).ToList();
            return PartialView("_PartialIndexAlertFavorite", roomAndZone);
        }
        // GET: AlertFavorite
        public ActionResult Index()
        {
            return View();
        }
    }
}