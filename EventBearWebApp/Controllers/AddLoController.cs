using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DatabaseLibrary;
using EventBearWebApp.Models;
using System.Text;
using System.Data;

namespace EventBearWebApp.Controllers
{
    public class AddLoController : Controller
    {

        [HttpPost]
        public ActionResult DropDownDistrict(int ProvinID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT CAST(ID AS VARCHAR) AS Value ,Name AS Text FROM DB_District WHERE ProvinceID = '{0}' ", ProvinID );
            var  Dittict = DatabaseUtilities.ExecuteQuery<SelectListItem>(sql).ToList();
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
        // GET: AddLo
        public ActionResult IndexAddLo()
        {
            //int rowAffect = DatabaseUtilities.ExecuteNonQuery(@" INSERT INTO Province  Values ('PN01', 'กรุงเทพมหานคร',getdate(), 'test',getdate(),'test') ");
            Init();

            var temp = DatabaseUtilities.ExecuteQuery<PlaceModel>
             ("Select * from Place where Customer_ID = 100001").LastOrDefault();

            return View(temp);
            //return View();
        }

        [HttpPost]
        public ActionResult Place(PlaceModel model)
        {
            Init();

            StringBuilder sql = new StringBuilder();
            List<DBParameter> param = new List<DBParameter>();

            if (ModelState.IsValid)
            {
                if (model.Place_ID != null && model.Place_ID > 0)
                {
                    //int rowAffect = DatabaseUtilities.ExecuteNonQuery(String.Format(@" UPDATE Place SET Place_Name = '{0}' where Place_ID = {1}", model.Place_Name, model.Place_ID));
                    var query = new StringBuilder();
                    query.Append("UPDATE Place SET ");
                    query.Append("Customer_ID = @Customer_ID, ");
                    query.Append("PlaceType_ID = @PlaceType_ID,");
                    query.Append("Place_Name = @Place_Name,");
                    query.Append("Place_Address = @Place_Address,");
                    query.Append("Place_Alley = @Place_Alley,");
                    query.Append("Place_Road = @Place_Road,");
                    query.Append("Place_Province = @Place_Province,");
                    query.Append("Place_District = @Place_District,");
                    query.Append("Place_SubDistrict = @Place_SubDistrict,");
                    query.Append("Place_Zipcode = @Place_Zipcode,");
                    query.Append("Place_Tel = @Place_Tel,");
                    query.Append("Place_Email = @Place_Email ");
                    query.Append("WHERE Place_ID = @Place_ID");
                  
                    sql.AppendLine(query.ToString());

                    param.Add("@Customer_ID", 100001);
                    param.Add("@PlaceType_ID", model.PlaceType_ID);
                    param.Add("@Place_Name", model.Place_Name);
                    param.Add("@Place_Address", model.Place_Address);
                    param.Add("@Place_Alley", model.Place_Alley);
                    param.Add("@Place_Road", model.Place_Road);
                    param.Add("@Place_Province", model.Place_Province);
                    param.Add("@Place_District", model.Place_District);
                    param.Add("@Place_SubDistrict", model.Place_SubDistrict);
                    param.Add("@Place_Zipcode", model.Place_Zipcode);
                    param.Add("@Place_Tel", model.Place_Tel);
                    param.Add("@Place_Email", model.Place_Email);
                    param.Add("@Place_ID", model.Place_ID);

                    DatabaseUtilities.ExecuteNonQuery(sql, param);

                }
                else
                {
                    //sql.AppendLine(" INSERT INTO Place (Customer_ID,PlaceType_ID, Place_Name, Place_Address, Place_Alley, Place_Road, Place_Province, Place_District,Place_SubDistrict ,Place_Zipcode,Place_Tel) ");
                    //sql.AppendLine(" VALUES(@Customer_ID,@PlaceType_ID, @Place_Name, @Place_Address, @Place_Alley, @Place_Road,@Place_Province,@Place_District, @Place_SubDistrict ,@Place_Zipcode,@Place_Tel  ) ");
                    //param.Add("@Customer_ID", model.Customer_ID);

                    var query = new StringBuilder();
                   query.Append("INSERT INTO Place (");
                   query.Append("Customer_ID,");
                   query.Append("PlaceType_ID,");
                   query.Append("Place_Name,");
                   query.Append("Place_Address,");
                   query.Append("Place_Alley,");
                   query.Append("Place_Road,");
                   query.Append("Place_Province,");
                   query.Append("Place_District,");
                   query.Append("Place_SubDistrict,");
                   query.Append("Place_Zipcode,");
                    query.Append("Place_Tel,");
                    query.Append("Place_Email )");

                    var queryParam = new StringBuilder();
                    queryParam.Append("VALUES (");
                    queryParam.Append("@Customer_ID,");
                    queryParam.Append("@PlaceType_ID,");
                    queryParam.Append("@Place_Name,");
                    queryParam.Append("@Place_Address,");
                    queryParam.Append("@Place_Alley,");
                    queryParam.Append("@Place_Road,");
                    queryParam.Append("@Place_Province,");
                    queryParam.Append("@Place_District,");
                    queryParam.Append("@Place_SubDistrict,");
                    queryParam.Append("@Place_Zipcode,");
                    queryParam.Append("@Place_Tel,");
                    queryParam.Append("@Place_Email )");

                    sql.AppendLine(query.ToString());
                    sql.AppendLine(queryParam.ToString());

                    param.Add("@Customer_ID", 100001);
                    param.Add("@PlaceType_ID", model.PlaceType_ID);
                    param.Add("@Place_Name", model.Place_Name);
                    param.Add("@Place_Address", model.Place_Address);
                    param.Add("@Place_Alley", model.Place_Alley);
                    param.Add("@Place_Road", model.Place_Road);
                    param.Add("@Place_Province", model.Place_Province);
                    param.Add("@Place_District", model.Place_District);
                    param.Add("@Place_SubDistrict", model.Place_SubDistrict);
                    param.Add("@Place_Zipcode", model.Place_Zipcode);
                    param.Add("@Place_Tel", model.Place_Tel);
                    param.Add("@Place_Email", model.Place_Email);

                    DatabaseUtilities.ExecuteNonQuery(sql, param);

                }
              
            }

            return View("IndexAddLo");
        }


        public ActionResult _PartialIndexAddRoom(int Place_ID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("SELECT RoomAndZone_Name,RoomAndZone_Price,RoomAndZone_Deposit,RoomAndZone_NumberPeople  FROM RoomAndZone WHERE Place_ID = '{0}'; ", Place_ID);
            IEnumerable<RoomAndZoneModel> roomAndZone = DatabaseUtilities.ExecuteQuery<RoomAndZoneModel>(sql).ToList();
            return PartialView("_PartialIndexAddRoom", roomAndZone);
        }


        public ActionResult IndexAddRoom()
        {
            //RoomAndZoneModel RoomAndZone = DatabaseLibrary
            //      .DatabaseUtilities.ExecuteQuery<RoomAndZoneModel>
            //      (" Select RoomAndZone_Name from RoomAndZone ").FirstOrDefault();

            //List<RoomAndZoneModel> listRoomAndZone = DatabaseLibrary
            // .DatabaseUtilities.ExecuteQuery<RoomAndZoneModel>
            // (" Select * from RoomAndZone ").ToList();

            return View();
        }


        [HttpPost]
        public ActionResult AddRoomAndZone(RoomAndZoneModel model)

        {
            {
                Init();

                StringBuilder sql = new StringBuilder();
                List<DBParameter> param = new List<DBParameter>();

                if (ModelState.IsValid)
                {
                    if (model.RoomAndZone_ID != null && model.RoomAndZone_ID > 0)
                    {
                       
                        var query = new StringBuilder();
                        query.Append("UPDATE RoomAndZone SET ");
                        query.Append("Place_ID = @Place_ID, ");                     
                        query.Append("RoomAndZone_Name = @RoomAndZone_Name,");
                        query.Append("RoomAndZone_Price = @RoomAndZone_Price,");
                        query.Append("RoomAndZone_Deposit = @RoomAndZone_Deposit,");
                        query.Append("RoomAndZone_NumberPeople = @RoomAndZone_NumberPeople,");
                        query.Append("RoomAndZone_Note = @RoomAndZone_Note ");                     
                        query.Append("WHERE RoomAndZone_ID = @RoomAndZone_ID");

                        sql.AppendLine(query.ToString());

                        param.Add("@Place_ID", 100003);
                        param.Add("@RoomAndZone_Name", model.RoomAndZone_Name);
                        param.Add("@RoomAndZone_Price", model.RoomAndZone_Price);
                        param.Add("@RoomAndZone_Deposit", model.RoomAndZone_Deposit);
                        param.Add("@RoomAndZone_NumberPeople", model.RoomAndZone_NumberPeople);
                        param.Add("@RoomAndZone_Note", model.RoomAndZone_Price);
                        param.Add("@RoomAndZone_ID", model.RoomAndZone_ID);

                        DatabaseUtilities.ExecuteNonQuery(sql, param);

                    }
                    else
                    {
                        //sql.AppendLine(" INSERT INTO Place (Customer_ID,PlaceType_ID, Place_Name, Place_Address, Place_Alley, Place_Road, Place_Province, Place_District,Place_SubDistrict ,Place_Zipcode,Place_Tel) ");
                        //sql.AppendLine(" VALUES(@Customer_ID,@PlaceType_ID, @Place_Name, @Place_Address, @Place_Alley, @Place_Road,@Place_Province,@Place_District, @Place_SubDistrict ,@Place_Zipcode,@Place_Tel  ) ");
                        //param.Add("@Customer_ID", model.Customer_ID);

                        var query = new StringBuilder();
                        query.Append("INSERT INTO RoomAndZone (");
                        query.Append("Place_ID,");
                        query.Append("RoomAndZone_Name,");
                        query.Append("RoomAndZone_Price,");
                        query.Append("RoomAndZone_Deposit,");
                        query.Append("RoomAndZone_NumberPeople,");
                        query.Append("RoomAndZone_Note )");
                      

                        var queryParam = new StringBuilder();
                        queryParam.Append(" VALUES (");
                        queryParam.Append("@Place_ID,");
                        queryParam.Append("@RoomAndZone_Name,");
                        queryParam.Append("@RoomAndZone_Price,");
                        queryParam.Append("@RoomAndZone_Deposit,");
                        queryParam.Append("@RoomAndZone_NumberPeople,");
                        queryParam.Append("@RoomAndZone_Note )");
                       

                        sql.AppendLine(query.ToString());
                        sql.AppendLine(queryParam.ToString());

                        param.Add("@Place_ID", 100003);
                        param.Add("@RoomAndZone_Name", model.RoomAndZone_Name);
                        param.Add("@RoomAndZone_Price", model.RoomAndZone_Price);
                        param.Add("@RoomAndZone_Deposit", model.RoomAndZone_Deposit);
                        param.Add("@RoomAndZone_NumberPeople", model.RoomAndZone_NumberPeople);
                        param.Add("@RoomAndZone_Note", model.RoomAndZone_Note.ToSafeString());
                        //param.Add("@RoomAndZone_ID", model.RoomAndZone_ID);                    

                        DatabaseUtilities.ExecuteNonQuery(sql, param);

                    }

                }

                return View("IndexAddRoom");
            }
        }
    }
}