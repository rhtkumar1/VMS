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
    public class MasterController : Controller
    {
        #region Financial Year
        public ActionResult FinancialIndex()
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
                dt = financialMaster.FinancialMaster_Get();
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
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/FinancialMaster.cshtml");
        }
        #endregion

        #region State Master
        public ActionResult StateIndex()
        {
            return View("~/Views/Admin/Masters/StateMaster.cshtml");
        }
        [HttpGet]
        public ActionResult GetStateMaster(StateMaster stateMaster)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                dt = stateMaster.StateMaster_Get(stateMaster);
                dt.TableName = "StateLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public ActionResult ManageStateMaster(StateMaster stateMaster)
        {
            try
            {
                StateMaster objStateMaster = stateMaster.StateMaster_InsertUpdate(stateMaster);
                if (objStateMaster != null)
                {
                    if (objStateMaster.StateId > 0)
                    {
                        ViewBag.Msg = "Updated Sucessfully";
                    }
                    else
                    {
                        ViewBag.Msg = "Saved Sucessfully";
                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/StateMaster.cshtml");
        }
        #endregion
    }
}