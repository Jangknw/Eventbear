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
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult DropDownDistrict(int ProvinID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT CAST(ID AS VARCHAR) AS Value ,Name AS Text FROM DB_District WHERE ProvinceID = '{0}' ", ProvinID);
            var Dittict = DatabaseUtilities.ExecuteQuery<SelectListItem>(sql).ToList();
            return Json(new { Status = true, data = Dittict });
        }

        [HttpPost]
        public ActionResult DropDownSubDistrict(int DistrictID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT CAST(ID AS VARCHAR) AS Value ,Name AS Text FROM DB_SubDistrict WHERE  DistrictID = '{0}' ", DistrictID);
            var SubDittict = DatabaseUtilities.ExecuteQuery<SelectListItem>(sql).ToList();
            return Json(new { Status = true, data = SubDittict });
        }

        private void Init()
        {
            List<PlaceTypeModel> placetypename = DatabaseUtilities.ExecuteQuery<PlaceTypeModel>
            ("Select PlaceType_Name,PlaceType_ID from placetype").ToList();
            ViewBag.placetypename = placetypename;

            List<ProvinceModel> provincename = DatabaseUtilities.ExecuteQuery<ProvinceModel>
           ("Select ID, Name from DB_Province").ToList();
            ViewBag.provincename = provincename;
            ViewBag.District = new List<DistrictModel>();
            ViewBag.SubDistrict = new List<DistrictModel>();


        }
        public ActionResult Index()
        {
            Init();
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}