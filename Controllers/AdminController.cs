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
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View("~/Views/Shared/dashboard/dashboard.cshtml");
        }
        public ActionResult UserLists(string type = "view")
        {
            DataSet ds = new DataSet();
            try
            {
                if (type == "view")
                {
                    return View("~/Views/Admin/UserMaster.cshtml");
                }
                else
                    return UserList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private ContentResult UserList()
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