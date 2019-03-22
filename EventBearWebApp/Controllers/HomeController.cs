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

        //public ActionResult _PartialIndexSearchLo(PlaceAndPlaceTypeModel model)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    //sql.AppendFormat("SELECT * FROM View_SearchLo WHERE PlaceType_ID = '{0}' ", model.PlaceType_ID);
        //    sql.AppendFormat("SELECT * FROM View_SearchLo ");
        //    if (!string.IsNullOrEmpty(model.PlaceType_ID))
        //        sql.AppendFormat("WHERE PlaceType_ID = '{0}' ", model.PlaceType_ID);
        //    if (!string.IsNullOrEmpty(model.Place_Province))
        //        sql.AppendFormat(" AND Place_Province = '{0}'", model.Place_Province);
        //    if (!string.IsNullOrEmpty(model.Place_District))
        //        sql.AppendFormat(" AND Place_District = '{0}';", model.Place_District);
        //    List<PlaceAndPlaceTypeModel> SearchLo = new List<PlaceAndPlaceTypeModel>();
        //    SearchLo = DatabaseUtilities.ExecuteQuery<PlaceAndPlaceTypeModel>(sql).ToList();
        //    return PartialView("_PartialIndexSearchLo", SearchLo);
        //}



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

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                StringBuilder sql = new StringBuilder();
                List<DBParameter> param = new List<DBParameter>();
                sql.AppendFormat("SELECT Customer_Email AS Email ,Customer_name +' ' +Customer_Lastname AS [Name]  FROM Customer WHERE Customer_Email = @Email AND Customer_Password = @Password ");
                param.Add("@Email", model.Email);
                param.Add("@Password", model.Password);
                var userprofile = DatabaseUtilities.ExecuteQuery<UserProfileModel>(sql, param.ToList()).FirstOrDefault();
                if (userprofile != null)
                {
                    Session["Email"] = userprofile.Email;
                    Session["Name"] = userprofile.Name;
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(CustomerModel model)

        {
            {
                Init();

                StringBuilder sql = new StringBuilder();
                List<DBParameter> param = new List<DBParameter>();

                //if (ModelState.IsValid)
                //{
                if (model.Customer_ID == null || model.Customer_ID == 0)
                {

                    var query = new StringBuilder();
                    query.Append("INSERT INTO Customer (");
                    query.Append("CustomerType_ID,");
                    query.Append("Customer_Name,");
                    query.Append("Customer_Lastname,");
                    query.Append("Customer_Tel,");
                    query.Append("Customer_Email,");
                    query.Append("Customer_Password,");
                    query.Append("Customer_ConfirmPassword )");

                    //query.Append("Customer_Address,");
                    //query.Append("Customer_Alley,");
                    //query.Append("Customer_Road,");
                    //query.Append("Customer_Province,");
                    //query.Append("Customer_District,");
                    //query.Append("Customer_SubDistrict,");
                    //query.Append("Customer_Zipcode )");

                    var queryParam = new StringBuilder();
                    queryParam.Append(" VALUES (");
                    queryParam.Append("@CustomerType_ID,");
                    queryParam.Append("@Customer_Name,");
                    queryParam.Append("@Customer_Lastname,");
                    queryParam.Append("@Customer_Tel,");
                    queryParam.Append("@Customer_Email,");
                    queryParam.Append("@Customer_Password,");
                    queryParam.Append("@Customer_ConfirmPassword )");


                    //queryParam.Append("@Customer_Address,");
                    //queryParam.Append("@Customer_Alley,");
                    //queryParam.Append("@Customer_Road,");
                    //queryParam.Append("@Customer_Province,");
                    //queryParam.Append("@Customer_District,");
                    //queryParam.Append("@Customer_SubDistrict,");
                    //queryParam.Append("@Customer_Zipcode )");


                    sql.AppendLine(query.ToString());
                    sql.AppendLine(queryParam.ToString());

                    param.Add("@CustomerType_ID", model.CustomerType_ID);
                    param.Add("@Customer_Name", model.Customer_Name);
                    param.Add("@Customer_Lastname", model.Customer_Lastname);
                    param.Add("@Customer_Tel", model.Customer_Tel);
                    param.Add("@Customer_Email", model.Customer_Email);
                    param.Add("@Customer_Password", model.Customer_Password);
                    param.Add("@Customer_ConfirmPassword", model.Customer_ConfirmPassword);

                    //param.Add("@Customer_Address", model.Customer_Address);
                    //param.Add("@Customer_Alley", model.Customer_Alley);
                    //param.Add("@Customer_Road", model.Customer_Road);
                    //param.Add("@Customer_Province", model.Customer_Province);
                    //param.Add("@Customer_District", model.Customer_District);
                    //param.Add("@Customer_SubDistrict", model.Customer_SubDistrict);
                    //param.Add("@Customer_Zipcode", model.Customer_Zipcode);


                    DatabaseUtilities.ExecuteNonQuery(sql, param);


                }
                else
                {                                

                }

                //}             
                return View("Index");
            }
        }

        public ActionResult _Profile()
        {
            var temp = DatabaseUtilities.ExecuteQuery<CustomerModel>
          ("Select * from Customer where Customer_ID = 100001").LastOrDefault();

            return View(temp);
        }

        [HttpPost]
        public new ActionResult Profile(CustomerModel model)
        {

            StringBuilder sql = new StringBuilder();
            List<DBParameter> param = new List<DBParameter>();

            if (model.Customer_ID != null && model.Customer_ID > 0)
            {
                var query = new StringBuilder();
                query.Append("UPDATE Customer SET ");
                //query.Append("CustomerType_ID = @CustomerType_ID,");
                query.Append("Customer_Name = @Customer_Name,");
                query.Append("Customer_Lastname = @Customer_Lastname,");
                query.Append("Customer_Tel = @Customer_Tel,");
                query.Append("Customer_Email = @Customer_Email,");
                query.Append("Customer_Password = @Customer_Password ");
                query.Append("Customer_ConfirmPassword = @Customer_ConfirmPassword,");
                query.Append("Customer_Address = @Customer_Address,");
                query.Append("Customer_Alley = @Customer_Alley,");
                query.Append("Customer_Road = @Customer_Road,");
                query.Append("Customer_Province = @Customer_Province,");
                query.Append("Customer_District = @Customer_District,");
                query.Append("Customer_SubDistrict = @Customer_SubDistrict ");
                query.Append("Customer_Zipcode = @Customer_Zipcode");
               

                sql.AppendLine(query.ToString());

                //param.Add("@CustomerType_ID", model.CustomerType_ID);
                param.Add("@Customer_Name", model.Customer_Name);
                param.Add("@Customer_Lastname", model.Customer_Lastname);
                param.Add("@Customer_Tel", model.Customer_Tel);
                param.Add("@Customer_Email", model.Customer_Email);
                param.Add("@Customer_Password", model.Customer_Password);
                param.Add("@Customer_ConfirmPassword", model.Customer_ConfirmPassword);
                param.Add("@Customer_Address", model.Customer_Address);
                param.Add("@Customer_Alley", model.Customer_Alley);
                param.Add("@Customer_Road", model.Customer_Road);
                param.Add("@Customer_Province", model.Customer_Province);
                param.Add("@Customer_District", model.Customer_District);
                param.Add("@Customer_SubDistrict", model.Customer_SubDistrict);
                param.Add("@Customer_Zipcode", model.Customer_Zipcode);

                DatabaseUtilities.ExecuteNonQuery(sql, param);

            }
            //else
            //{


            //}


            return View("Index");
        }

        public ActionResult _PartialSearchType(PlaceAndPlaceTypeModel model)
        {
            StringBuilder sql = new StringBuilder();
            //sql.AppendFormat("SELECT * FROM View_SearchLo WHERE PlaceType_ID = '{0}' ", model.PlaceType_ID);
            sql.AppendFormat("SELECT * FROM View_SearchLo WHERE PlaceType_ID = 1000");          
            List<PlaceAndPlaceTypeModel> Home = new List<PlaceAndPlaceTypeModel>();
            Home = DatabaseUtilities.ExecuteQuery<PlaceAndPlaceTypeModel>(sql).ToList();
            return PartialView("_PartialSearchType", Home);
        }

      
    }
}