using IMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult InterFace()
        {
            if (Session["UserName"] != null)
            {
                ReportInterFace objReportInterFace = new ReportInterFace();
                return View("~/Views/Report/ReportInterFace.cshtml", objReportInterFace);
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }
    }
}
