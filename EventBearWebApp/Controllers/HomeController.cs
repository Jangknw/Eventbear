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
        public ActionResult Index()
        {
            //int rowAffect = DatabaseUtilities.ExecuteNonQuery(@" INSERT INTO PlaceType  Values ('700', 'CustomerNaja4',getdate(), 'test',getdate(),'test') ");
            //ตัวอย่าง Select ทูน่า
            //PlaceTypeModel customerType = DatabaseLibrary
            //    .DatabaseUtilities.ExecuteQuery<PlaceTypeModel>
            //    (" Select * from PlaceType ").FirstOrDefault();

            //ตัวอย่าง Select ทูน่า
            //List<PlaceTypeModel> listCustomerType = DatabaseLibrary
            //    .DatabaseUtilities.ExecuteQuery<PlaceTypeModel>
            //    (" Select * from PlaceTypeModel ").ToList();




            return View(/*customerType*/);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}