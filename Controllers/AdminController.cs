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
    //[SessionAuthentication]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult DashBoard()
        {
            return View("~/Views/Shared/DashBoard/DashBoard.cshtml");
        }
    }
}