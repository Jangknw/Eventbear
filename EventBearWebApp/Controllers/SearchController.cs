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
        public ActionResult IndexSearchLo()
        {
            Init();
            return View();
        }

        public ActionResult _PartialIndexSearchLo(PlaceAndPlaceTypeModel model)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT * FROM View_SearchLo WHERE PlaceType_ID = '{0}';", model.PlaceType_ID);
            //sql.AppendFormat("SELECT * FROM View_SearchLo WHERE Place_Province = '{0}';", model.Place_Province);
            //sql.AppendFormat("SELECT * FROM View_SearchLo WHERE Place_District = '{0}';", model.Place_District);
            //sql.AppendFormat("SELECT * FROM View_SearchLo WHERE PlaceType_ID = '{0}' AND Place_Province '{0}';", model.PlaceType_ID,model.Place_Province);
            List<PlaceAndPlaceTypeModel> SearchLo = new List<PlaceAndPlaceTypeModel>();
            SearchLo = DatabaseUtilities.ExecuteQuery<PlaceAndPlaceTypeModel>(sql).ToList();
            return PartialView("_PartialIndexSearchLo", SearchLo);
        }


        public ActionResult IndexSearchLoDetail()
        {
            Init();
            List<PlaceTypeModel> placetypename = DatabaseLibrary
              .DatabaseUtilities.ExecuteQuery<PlaceTypeModel>
               ("Select PlaceType_Name,PlaceType_ID from placetype").ToList();
            ViewBag.placetypename = placetypename;

 
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
    }
}