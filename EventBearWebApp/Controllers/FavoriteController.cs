using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventBearWebApp.Controllers
{
    public class FavoriteController : Controller
    {
        // GET: Favorite
        public ActionResult Index()
        {
            return View();
        }
    }
}