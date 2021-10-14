using IMS.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class FinancialMasterController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("~/Views/Admin/Masters/FinancialMaster.cshtml");
        }
        [HttpGet]
        public ActionResult GetFinancialMaster(FinancialMaster financialMaster)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                dt = financialMaster.GetFinancialData();
                dt.TableName = "FinancialLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public ActionResult ManageFinancialMaster(FinancialMaster financialMaster)
        {
            string result = "Fail";
            try
            {
                FinancialMaster objFinancialMaster = financialMaster.FinancialMaster_InsertUpdate(financialMaster);
                if (objFinancialMaster != null)
                {
                    if (objFinancialMaster.FinancialId > 0)
                    {
                        ViewBag.Msg = "Updated Sucessfully";
                    }
                    else
                    {
                        ViewBag.Msg = "Saved Sucessfully";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/FinancialMaster.cshtml");
        }
    }
}