using IMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using IMS.Models.CBL;

namespace IMS.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Authenticate(string loginid = "", string password = "")
        {
            string result = "Fail";
            try
            {
                if (loginid != "" && password != "")
                {
                    Authenticate ObjAuthenticate = new Authenticate().AuthenticateUser(loginid, password, Session.SessionID.ToString());
                    if (ObjAuthenticate.IsAuthenticated)
                    {
                        Session["Menu_Master_Role_Wise"] = ObjAuthenticate.ObjMenu_Master_Role_Wise;
                        ObjAuthenticate.ObjMenu_Master_Role_Wise = null;
                        Session["SYSSOFTECHSession"] = ObjAuthenticate;
                        Session["UserName"] = Convert.ToString(ObjAuthenticate.UserName);
                        Session["UserType"] = Convert.ToString(ObjAuthenticate.UserType);
                        result = "Success";
                    }
                    else
                    {
                        ViewBag.Msg = "User Name and password is wrong..!";
                    }
                }
                else
                {
                    ViewBag.Msg = "Enter User Name and password ";
                }

            }
            catch (Exception)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            if (Session["UserName"] != null)
            {

                return View("~/Views/Shared/DashBoard/DashBoard.cshtml");
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }
        //[ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            Session["SYSSOFTECHSession"] = null;
            return View("~/Views/Home/Index.cshtml");
        }
    }
}