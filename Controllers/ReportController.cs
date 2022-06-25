using IMS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

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
        [HttpGet]
        public ActionResult GetReportConfigData(int ReportId)
        {
            DataTable DT = CommonUtility.GetReportConfigData(ReportId);
            return Content(JsonConvert.SerializeObject(DT));
        }
        public ActionResult LedgerReport()
        {
            if (Session["UserName"] != null)
            {
                LedgerReport objLedgerReport = new LedgerReport();
                return View("~/Views/Report/LedgerReport.cshtml", objLedgerReport);
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        [HttpGet]
        public ActionResult GetLedger(int Group_Id, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                LedgerReport ledgerReport = new LedgerReport();
                dt = ledgerReport.GroupMaster_GetLedger(Group_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult GetPartyCityWise(string City, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                ReportInterFace reportInterFace = new ReportInterFace();
                dt = reportInterFace.GetPartyCityWise(City);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

    }
}
