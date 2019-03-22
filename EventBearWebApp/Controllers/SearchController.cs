using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLibrary;
using EventBearWebApp.Models;
using System.Text;

namespace EventBearWebApp.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public ActionResult DropDownDistrict(int ProvinID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT CAST(ID AS VARCHAR) AS Value ,Name AS Text FROM DB_District WHERE ProvinceID = '{0}' ", ProvinID);
            var Dittict = DatabaseUtilities.ExecuteQuery<SelectListItem>(sql).ToList();
            return Json(new { Status = true, data = Dittict });
        }

        //[HttpPost]
        //public ActionResult DropDownSubDistrict(int DistrictID)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    sql.AppendFormat("SELECT CAST(ID AS VARCHAR) AS Value ,Name AS Text FROM DB_SubDistrict WHERE  DistrictID = '{0}' ", DistrictID);
        //    var SubDittict = DatabaseUtilities.ExecuteQuery<SelectListItem>(sql).ToList();
        //    return Json(new { Status = true, data = SubDittict });
        //}

        private void Init()
        {
            List<PlaceAndPlaceTypeModel> placetypename = DatabaseUtilities.ExecuteQuery<PlaceAndPlaceTypeModel>
            ("Select PlaceType_Name,PlaceType_ID from placetype").ToList();
            ViewBag.placetypename = placetypename;

            List<ProvinceModel> provincename = DatabaseUtilities.ExecuteQuery<ProvinceModel>
           ("Select ID, Name from DB_Province").ToList();
            ViewBag.provincename = provincename;
            ViewBag.District = new List<DistrictModel>();
            //ViewBag.SubDistrict = new List<DistrictModel>();
        }

        // GET: Search

        public ActionResult IndexSearchLo(PlaceAndPlaceTypeModel model)
        {           
            Init();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from View_SearchLo ");
            if (!string.IsNullOrEmpty(model.PlaceType_ID))
                sql.AppendFormat("WHERE PlaceType_ID = '{0}' ", model.PlaceType_ID);
            if (!string.IsNullOrEmpty(model.Place_Province))
                sql.AppendFormat(" AND Place_Province = '{0}'", model.Place_Province);
            if (!string.IsNullOrEmpty(model.Place_District))
                sql.AppendFormat(" AND Place_District = '{0}';", model.Place_District);
            IEnumerable<PlaceAndPlaceTypeModel> SearchLo = DatabaseUtilities.ExecuteQuery<PlaceAndPlaceTypeModel>(sql).ToList();
            return View();
        }

        //public ActionResult IndexSearchLo(int PlaceType_ID)
        //{
        //    int test = PlaceType_ID;
        //    Init();           
        //   StringBuilder sql = new StringBuilder();
        //    sql.AppendFormat("select * from View_SearchLo WHERE PlaceType_ID = '{0}'; ", PlaceType_ID);
        //    IEnumerable<PlaceAndPlaceTypeModel> SearchLo = DatabaseUtilities.ExecuteQuery<PlaceAndPlaceTypeModel>(sql).ToList();           
        //    return View();
        //}

        //public ActionResult IndexSearchLo(string search)
        //{        
        //    string test = search;
        //    Init();        
        //    var temp = DatabaseUtilities.ExecuteQuery<PlaceAndPlaceTypeModel>
        //    ("Select * from View_SearchLo Where PlaceType_ID = 6000").Last();
        //    return View(temp);        
        //}

        public ActionResult _PartialIndexSearchLo(PlaceAndPlaceTypeModel model)
        {
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat("SELECT * FROM View_SearchLo WHERE PlaceType_ID = '{0}' ", model.PlaceType_ID);
            sql.AppendFormat("SELECT * FROM View_SearchLo ");
            if (!string.IsNullOrEmpty(model.PlaceType_ID))
                sql.AppendFormat("WHERE PlaceType_ID = '{0}' ", model.PlaceType_ID);
            if (!string.IsNullOrEmpty(model.Place_Province))
                sql.AppendFormat(" AND Place_Province = '{0}'", model.Place_Province);
            if (!string.IsNullOrEmpty(model.Place_District))
                sql.AppendFormat(" AND Place_District = '{0}';", model.Place_District);                
                List<PlaceAndPlaceTypeModel> SearchLo = new List<PlaceAndPlaceTypeModel>();
            SearchLo = DatabaseUtilities.ExecuteQuery<PlaceAndPlaceTypeModel>(sql).ToList();
            return PartialView("_PartialIndexSearchLo", SearchLo);
        }


        public ActionResult IndexSearchLoDetail()
        {
            //Init();      
            //var temp = DatabaseUtilities.ExecuteQuery<PlaceModel>
            //("Select * from Place").Last();
            return View();
        }

        public ActionResult _PartialIndexSearchLoDetail(int Place_ID)
        {
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat("SELECT RoomAndZone_Name,RoomAndZone_Price,RoomAndZone_Deposit,RoomAndZone_NumberPeople,RoomAndZone_Note FROM RoomAndZone WHERE Place_ID = '{0}'; ", Place_ID);
            sql.AppendFormat("select * from View_SearchLoDetail WHERE Place_ID = '{0}'; ", Place_ID);
            IEnumerable<SearchLoDetailDViewModel> roomAndZone = DatabaseUtilities.ExecuteQuery<SearchLoDetailDViewModel>(sql).ToList();
            return PartialView("_PartialIndexSearchLoDetail", roomAndZone);           
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




        [HttpPost]
        public string InsertFavorite(int id)
        {

            StringBuilder sql = new StringBuilder();
            List<DBParameter> param = new List<DBParameter>();


            if (!string.IsNullOrWhiteSpace(id.ToSafeString()))
            {
                var query = new StringBuilder();
                query.Append("INSERT INTO Favorite (");
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