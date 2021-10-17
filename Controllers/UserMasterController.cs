using IMS.Models.CBL;
using IMS.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace IMS.Controllers
{
    [SessionAuthentication]
    public class UserMasterController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("~/Views/Admin/Masters/UserMaster.cshtml");
        }
        public ActionResult GetUserMaster()
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@QueryType", "getall"));
                ds = DBManager.ExecuteDataSetWithParameter("Proc_Manage_UserMasters", CommandType.StoredProcedure, SqlParameters);
                ds.Tables[0].TableName = "UserLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }
        public ActionResult ManageUserMaster(CreateUserMaster createUser)
        {
            string result = "Fail";
            try
            {
                CreateUserMaster objUserCreation = createUser.ManageUsers(createUser);
                if (objUserCreation != null)
                {
                    if (objUserCreation.IsExists)
                    {
                        ViewBag.Msg = createUser.Msg;
                    }
                    else
                    {
                        ViewBag.Msg = createUser.Msg;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/UserMaster.cshtml");
        }
    }
}