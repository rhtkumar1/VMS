using IMS.Models;
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
        string AppToken = "";
        #region Financial Year
        public ActionResult FinancialIndex()
        {
            FinancialMaster financialMaster = new FinancialMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            financialMaster.AppToken = AppToken;
            financialMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/FinancialMaster.cshtml", financialMaster);
        }
        [HttpGet]
        public ActionResult GetFinancialMaster(FinancialMaster financialMaster, string AppToken = "")
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
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                financialMaster.AppToken = AppToken;
                financialMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
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
            return View("~/Views/Admin/Masters/FinancialMaster.cshtml", financialMaster);
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

        #region Location Master
        public ActionResult LocationIndex()
        {
            LocationMaster locationMaster = new LocationMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            locationMaster.AppToken = AppToken;
            locationMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/LocationMaster.cshtml", locationMaster);
        }
        [HttpGet]
        public ActionResult GetLocationMaster(LocationMaster locationMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = locationMaster.LocationMaster_Get();
                dt.TableName = "LocationLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageLocationMaster(LocationMaster locationMaster)
        {
            try
            {
                //locationMaster.Loginid = SyssoftechSession
                LocationMaster objLocationMaster = locationMaster.LocationMaster_InsertUpdate(locationMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                locationMaster.AppToken = AppToken;
                locationMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objLocationMaster != null)
                {
                    if (objLocationMaster.LocationId > 0)
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
            return View("~/Views/Admin/Masters/LocationMaster.cshtml", locationMaster);
        }
        #endregion

        #region Company Master
        public ActionResult CompanyIndex()
        {
            CompanyMaster companyMaster = new CompanyMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            companyMaster.AppToken = AppToken;
            companyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/CompanyMaster.cshtml", companyMaster);
        }
        [HttpGet]
        public ActionResult GetCompanyMaster(CompanyMaster companyMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = companyMaster.CompanyMaster_Get();
                dt.TableName = "CompanyLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageCompanyMaster(CompanyMaster companyMaster)
        {
            try
            {
                CompanyMaster objCompanyMaster = companyMaster.CompanyMaster_InsertUpdate(companyMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                companyMaster.AppToken = AppToken;
                companyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objCompanyMaster != null)
                {
                    if (objCompanyMaster.CompanyId > 0)
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
            return View("~/Views/Admin/Masters/CompanyMaster.cshtml", companyMaster);
        }
        #endregion
    }
}