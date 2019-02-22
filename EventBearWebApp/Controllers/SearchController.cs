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
        // GET: Search
        public ActionResult IndexSearchLo()
        {
            //int rowAffect = DatabaseUtilities.ExecuteNonQuery(@" INSERT INTO PlaceType  Values ('100', 'Testja2',getdate(), 'test',getdate(),'test') ");
            //int rowAffect1 = DatabaseUtilities.ExecuteNonQuery(@" INSERT INTO PlaceType  Values ('200', 'Testja3',getdate(), 'test',getdate(),'test') ");

            //PlaceTypeModel placetypename = DatabaseLibrary
            //    .DatabaseUtilities.ExecuteQuery<PlaceTypeModel>
            //    (" Select * from PlaceType ").FirstOrDefault();  

            //ตัวอย่าง Select ทูน่า
           // List<ProvinceModel> provincename = DatabaseLibrary
           //.DatabaseUtilities.ExecuteQuery<ProvinceModel>
           // ("Select Province_ID,Province_Name from Province").ToList();
           // ViewBag.provincename = provincename;

            List<PlaceTypeModel> placetypename = DatabaseLibrary
               .DatabaseUtilities.ExecuteQuery<PlaceTypeModel>
                ("Select PlaceType_Name,PlaceType_ID from placetype").ToList();
            ViewBag.placetypename = placetypename;
          


            //StringBuilder sql = new StringBuilder();
            //List<PlaceTypeModel> placeTypename = new List<PlaceTypeModel>();
            //sql.AppendLine(" SELECT * FROM PlaceType ");

            return View();
       

        }


        public ActionResult IndexSearchLoDetail()
        {

            //List<ProvinceModel> provincename = DatabaseLibrary
            // .DatabaseUtilities.ExecuteQuery<ProvinceModel>
            //  ("Select Province_ID,Province_Name from Province").ToList();
            //ViewBag.provincename = provincename;

            List<PlaceTypeModel> placetypename = DatabaseLibrary
              .DatabaseUtilities.ExecuteQuery<PlaceTypeModel>
               ("Select PlaceType_Name,PlaceType_ID from placetype").ToList();
            ViewBag.placetypename = placetypename;

            return View();
        }
    }
}