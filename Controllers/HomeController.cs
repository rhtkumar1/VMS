using IMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using IMS.Models.CBL;
using Newtonsoft.Json;

namespace IMS.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(string loginid = "", string password = "")
        {
            string result = "Fail";
            try
            {
                if (loginid != "" && password != "")
                {
                    Authenticate ObjAuthenticate = new Authenticate().AuthenticateUser(loginid, password, Session.SessionID.ToString());
                    if (ObjAuthenticate.IsAuthenticated)
                    {
                        Session["FYYears"] = new FinancialMaster().FYList(ObjAuthenticate.CompanyID);
                        Session["OpenFYID"] = 1;
                        Session["Menu_Master_Role_Wise"] = ObjAuthenticate.ObjMenu_Master_Role_Wise;
                        ObjAuthenticate.ObjMenu_Master_Role_Wise = null;
                        Session["SYSSOFTECHSession"] = ObjAuthenticate;
                        Session["MinuList"] = ObjAuthenticate.Menu_List;
                        ObjAuthenticate.Menu_List = null;
                        Session["UserName"] = Convert.ToString(ObjAuthenticate.UserName);
                        Session["UserType"] = Convert.ToString(ObjAuthenticate.UserType);
                        result = "Success";
                        //Redirecting the user to the Login View of Account Controller  
                        //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{ { "controller", "Home" }, { "action", "Index" }});
                        return RedirectToAction("DashBoard", "Admin");//RedirectToRoute(new System.Web.Routing.RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } }); //RedirectToAction("~/Admin/DashBoard");
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
                return View("~/Views/Home/Index.cshtml");

            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Home/Index.cshtml");
            //return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            //if (Session["UserName"] != null)
            //{

            //    return View("~/Views/Shared/DashBoard/DashBoard.cshtml");
            //}
            //else
            //{
                return View("~/Views/Home/Index.cshtml");
            //}
        }
        public ActionResult Index()
        {
            Session["Menu_Master_Role_Wise"] = null;
            Session["SYSSOFTECHSession"] = null;
            Session["MinuList"] = null;
            Session["UserName"] = null;
            Session["UserType"] = null;
            Session["SYSSOFTECHSession"] = null;
            Session.Abandon();
            return View("~/Views/Home/Index.cshtml");
        }
        //[ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            
            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult SetFYID(int FYID)
        {
            DataTable dt = new DataTable();
            Session["OpenFYID"] = FYID;
            return Content(JsonConvert.SerializeObject(dt));
        }
    }
}