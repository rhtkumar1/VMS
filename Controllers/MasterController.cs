﻿using IMS.Models;
using IMS.Models.CBL;
using IMS.Models.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Web;
using System.Web.Mvc;
//using System.Web.Script.Serialization;

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
            financialMaster.AppToken = CommonUtility.URLAppToken(AppToken);
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
            FinancialMaster objFinancialMaster = new FinancialMaster();
            try
            {
                //objFinancialMaster= financialMaster.FinancialMaster_InsertUpdate(financialMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                financialMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                financialMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objFinancialMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objFinancialMaster.IsSucceed)
                    {
                        ViewBag.Msg = objFinancialMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objFinancialMaster.IsSucceed && objFinancialMaster.FinancialId != -1)
                    {
                        ViewBag.Msg = objFinancialMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            FinancialMaster newFinancialMaster = new FinancialMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newFinancialMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newFinancialMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/FinancialMaster.cshtml", (objFinancialMaster.IsSucceed ? newFinancialMaster : financialMaster));
        }

        [HttpPost]
        public ActionResult DeleteFinancialMaster(FinancialMaster financialMaster, int financialId)
        {
            try
            {
                //financialMaster.FinancialId = financialId;
                FinancialMaster objFinancialMaster = financialMaster.FinancialMaster_Delete(financialMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                financialMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                financialMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objFinancialMaster != null)
                {
                    if (objFinancialMaster.FinancialId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/FinancialMaster.cshtml", financialMaster);
        }
        #endregion

        #region Vehicle_Master

        public ActionResult VehicleIndex()
        {
            VehicleMaster vehicleMaster = new VehicleMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            vehicleMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            vehicleMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/VehicleMaster.cshtml", vehicleMaster);
        }

        [HttpPost]
        public ActionResult ManageVehicleMaster(VehicleMaster vehicleMaster)
        {


            VehicleMaster objvehicleMaster = new VehicleMaster();
            try
            {
                if(vehicleMaster.UploadFile.FileName != null)
                {
                    string strdatetime = DateTime.Now.ToString("ddMMyyyyHHMMss");
                    string path = "\\UploadFile\\" + strdatetime + vehicleMaster.UploadFile.FileName;
                    vehicleMaster.UploadFile.SaveAs(Server.MapPath("~") + path);
                    vehicleMaster.Vehicle_Img = path;
                }
                

                objvehicleMaster = vehicleMaster.VehicleMaster_InsertUpdate(vehicleMaster);
                AppToken = (Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"]);
                vehicleMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                vehicleMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objvehicleMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objvehicleMaster.IsSucceed)
                    {
                        ViewBag.Msg = objvehicleMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objvehicleMaster.IsSucceed && objvehicleMaster.Id != -1)
                    {
                        ViewBag.Msg = objvehicleMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            VehicleMaster newvehicleMaster = new VehicleMaster();
            //StateMaster newStateMaster = new StateMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newvehicleMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newvehicleMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/VehicleMaster.cshtml", (objvehicleMaster.IsSucceed ? newvehicleMaster : vehicleMaster));
        }

        [HttpGet]
        public ActionResult GetVehicleMaster(VehicleMaster vehicleMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = vehicleMaster.VehicleMaster_Get();
                dt.TableName = "StateLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult DeleteVehicleMaster(VehicleMaster vehicleMaster, int id)
        {
            try
            {
                vehicleMaster.Id = id;
                VehicleMaster objVehicleMaster = vehicleMaster.VehicleMaster_Delete(vehicleMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                vehicleMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                vehicleMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objVehicleMaster != null)
                {
                    if (objVehicleMaster.Id > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/VehicleMaster.cshtml", vehicleMaster);
        }

        #endregion

        #region Vehicle_Model_Master
        public ActionResult VehicleModelIndex()
        {
            VehicleModelMaster vehicleModelMaster = new VehicleModelMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            vehicleModelMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            vehicleModelMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/VehicleModelMaster.cshtml", vehicleModelMaster);
        }

        [HttpPost]
        public ActionResult ManageVehicleModelMaster(VehicleModelMaster vehicleModelMaster)
        {
            VehicleModelMaster objvehicleModelMaster = new VehicleModelMaster();
            try
            {
                objvehicleModelMaster = vehicleModelMaster.VehicleModelMaster_InsertUpdate(vehicleModelMaster);
                AppToken = (Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"]);
                vehicleModelMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                vehicleModelMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objvehicleModelMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objvehicleModelMaster.IsSucceed)
                    {
                        ViewBag.Msg = objvehicleModelMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objvehicleModelMaster.IsSucceed && objvehicleModelMaster.Id != -1)
                    {
                        ViewBag.Msg = objvehicleModelMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            VehicleModelMaster newvehicleModelMaster = new VehicleModelMaster();
            //StateMaster newStateMaster = new StateMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newvehicleModelMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newvehicleModelMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/VehicleModelMaster.cshtml", (objvehicleModelMaster.IsSucceed ? newvehicleModelMaster : vehicleModelMaster));
        }

        [HttpGet]
        public ActionResult GetVehicleModelMaster(VehicleModelMaster vehicleModelMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = vehicleModelMaster.VehicleModelMaster_Get();
                dt.TableName = "StateLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult DeleteVehicleModelMaster(VehicleModelMaster vehicleModelMaster, int id)
        {
            try
            {
                vehicleModelMaster.Id = id;
                VehicleModelMaster objVehicleModelMaster = vehicleModelMaster.VehicleModelMaster_Delete(vehicleModelMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                vehicleModelMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                vehicleModelMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objVehicleModelMaster != null)
                {
                    if (objVehicleModelMaster.Id > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/VehicleModelMaster.cshtml", vehicleModelMaster);
        }
        #endregion

        #region State Master
        public ActionResult StateIndex()
        {
            StateMaster stateMaster = new StateMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            stateMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            stateMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/StateMaster.cshtml", stateMaster);
        }
        [HttpGet]
        public ActionResult GetStateMaster(StateMaster stateMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = stateMaster.StateMaster_Get();
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
            StateMaster objStateMaster = new StateMaster();
            try
            {
                objStateMaster = stateMaster.StateMaster_InsertUpdate(stateMaster);
                AppToken = (Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"]);
                stateMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                stateMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objStateMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objStateMaster.IsSucceed)
                    {
                        ViewBag.Msg = objStateMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objStateMaster.IsSucceed && objStateMaster.StateId != -1)
                    {
                        ViewBag.Msg = objStateMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            StateMaster newStateMaster = new StateMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newStateMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newStateMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/StateMaster.cshtml", (objStateMaster.IsSucceed ? newStateMaster : stateMaster));
        }

        [HttpPost]
        public ActionResult DeleteStateMaster(StateMaster stateMaster, int stateId)
        {
            try
            {
                stateMaster.StateId = stateId;
                StateMaster objStateMaster = stateMaster.StateMaster_Delete(stateMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                stateMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                stateMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objStateMaster != null)
                {
                    if (objStateMaster.StateId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/StateMaster.cshtml", stateMaster);
        }
        #endregion

        #region Location Master
        public ActionResult LocationIndex()
        {
            LocationMaster locationMaster = new LocationMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            locationMaster.AppToken = CommonUtility.URLAppToken(AppToken);
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
            LocationMaster objLocationMaster = new LocationMaster();
            try
            {
                //locationMaster.Loginid = SyssoftechSession
                objLocationMaster = locationMaster.LocationMaster_InsertUpdate(locationMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                locationMaster.AppToken = AppToken;
                locationMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objLocationMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objLocationMaster.IsSucceed)
                    {
                        ViewBag.Msg = objLocationMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objLocationMaster.IsSucceed && objLocationMaster.LocationId != -1)
                    {
                        ViewBag.Msg = objLocationMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            LocationMaster newLocationMaster = new LocationMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newLocationMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newLocationMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/LocationMaster.cshtml", (objLocationMaster.IsSucceed ? newLocationMaster : locationMaster));
        }

        [HttpPost]
        public ActionResult DeleteLocationMaster(LocationMaster locationMaster, int locationId)
        {
            try
            {
                locationMaster.LocationId = locationId;
                LocationMaster objLocationMaster = locationMaster.LocationMaster_Delete(locationMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                locationMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                locationMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objLocationMaster != null)
                {
                    if (objLocationMaster.LocationId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            StateMaster newStateMaster = new StateMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            locationMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            locationMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/LocationMaster.cshtml", locationMaster);
        }
        #endregion

        #region Company Master
        public ActionResult CompanyIndex()
        {
            CompanyMaster companyMaster = new CompanyMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            companyMaster.AppToken = CommonUtility.URLAppToken(AppToken);
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
            CompanyMaster objCompanyMaster = new CompanyMaster();
            try
            {
                objCompanyMaster = companyMaster.CompanyMaster_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                companyMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                companyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objCompanyMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objCompanyMaster.IsSucceed)
                    {
                        ViewBag.Msg = objCompanyMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objCompanyMaster.IsSucceed && objCompanyMaster.CompanyId != -1)
                    {
                        ViewBag.Msg = objCompanyMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            CompanyMaster newCompanyMaster = new CompanyMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newCompanyMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newCompanyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/CompanyMaster.cshtml", (objCompanyMaster.IsSucceed ? newCompanyMaster : companyMaster));

        }

        [HttpPost]
        public ActionResult DeleteCompanyMaster(CompanyMaster companyMaster, int companyId)
        {
            try
            {
                companyMaster.CompanyId = companyId;
                CompanyMaster objCompanyMaster = companyMaster.CompanyMaster_Delete();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                companyMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                companyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objCompanyMaster != null)
                {
                    if (objCompanyMaster.IsSucceed)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = objCompanyMaster.ActionMsg }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Unknown Error Occured !!!" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Unknown Error Occured !!!" }));
                }
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Unknown Error Occured !!!" }));
            }
        }
        #endregion

        #region Office Master
        public ActionResult OfficeIndex()
        {
            OfficeMaster officeMaster = new OfficeMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            officeMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            officeMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/OfficeMaster.cshtml", officeMaster);
        }
        [HttpGet]

        public ActionResult GetOfficeMaster(OfficeMaster officeMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = officeMaster.OfficeMaster_Get();
                dt.TableName = "OfficeLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageOfficeMaster(OfficeMaster officeMaster)
        {
            OfficeMaster objOfficeMaster = new OfficeMaster();
            try
            {
                objOfficeMaster = officeMaster.OfficeMaster_InsertUpdate(officeMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                officeMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                officeMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objOfficeMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objOfficeMaster.IsSucceed)
                    {
                        ViewBag.Msg = objOfficeMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objOfficeMaster.IsSucceed && objOfficeMaster.OfficeId != -1)
                    {
                        ViewBag.Msg = objOfficeMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            OfficeMaster newOfficeMaster = new OfficeMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newOfficeMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newOfficeMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/OfficeMaster.cshtml", (objOfficeMaster.IsSucceed ? newOfficeMaster : officeMaster));
        }

        [HttpPost]
        public ActionResult DeleteOfficeMaster(OfficeMaster officeMaster, int officeId)
        {
            try
            {
                officeMaster.OfficeId = officeId;
                OfficeMaster objOfficeMaster = officeMaster.OfficeMaster_Delete(officeMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                officeMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                officeMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objOfficeMaster != null)
                {
                    if (objOfficeMaster.OfficeId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/OfficeMaster.cshtml", officeMaster);
        }
        #endregion

        #region Role
        public ActionResult RoleIndex()
        {
            RoleMaster roleMaster = new RoleMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            roleMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            roleMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/RoleMaster.cshtml", roleMaster);
        }

        [HttpGet]
        public ActionResult GetRoleMaster(string AppToken = "")
        {
            DataTable dt = new DataTable();
            RoleMaster roleMaster = new RoleMaster();
            try
            {
                dt = roleMaster.RoleMaster_Get();
                dt.TableName = "RoleLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public ActionResult ManageRoleMaster(RoleMaster roleMaster)
        {
            RoleMaster objRoleMaster = new RoleMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            try
            {
                roleMaster.ObjRoleMenuMapping = JsonConvert.DeserializeObject<List<RoleMenuMapping>>(roleMaster.MenuMapping);
                objRoleMaster = roleMaster.RoleMaster_InsertUpdate(roleMaster);
                roleMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                roleMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objRoleMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objRoleMaster.IsSucceed)
                    {
                        ModelState.Clear();
                        return RedirectToAction("RoleIndex", new { sMsg = objRoleMaster.ActionMsg, appToken = roleMaster.AppToken, });
                    }
                    // In case of record already exists
                    else if (!objRoleMaster.IsSucceed && objRoleMaster.RoleId != -1)
                    {
                        if (roleMaster.RoleId > 0)
                        {
                            return RedirectToAction("RoleMaster", new { iRoleId = roleMaster.RoleId, appToken = roleMaster.AppToken, sMsg = objRoleMaster.ActionMsg });
                        }
                        else
                        {
                            return RedirectToAction("RoleMaster", new { appToken = roleMaster.AppToken, sMsg = objRoleMaster.ActionMsg });
                        }
                    }
                    // In case of any error occured
                    else
                    {
                        return RedirectToAction("RoleMaster", new { iRoleId = roleMaster.RoleId, appToken = roleMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                    }
                }
                else
                {
                    if (roleMaster.RoleId > 0)
                    {
                        return RedirectToAction("RoleMaster", new { iRoleId = roleMaster.RoleId, appToken = roleMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                    }
                    else
                    {
                        return RedirectToAction("RoleMaster", new { appToken = roleMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
                // to reset fields only in case of added or updated.
                if (roleMaster.RoleId > 0)
                {
                    return RedirectToAction("RoleMaster", new { iRoleId = roleMaster.RoleId, appToken = roleMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                }
                else
                {
                    return RedirectToAction("RoleMaster", new { appToken = roleMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                }
            }
        }
        [HttpGet]
        public ActionResult RoleMaster(int iRoleId = 0, string appToken = "", string sMsg = "")
        {
            try
            {
                if (sMsg != null && sMsg != "") { ViewBag.Msg = sMsg; }
                RoleMaster roleMaster = new RoleMaster();
                if (iRoleId > 0)
                {
                    roleMaster = roleMaster.RoleMaster_Get_By_Id(iRoleId);
                }
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                roleMaster.AppToken = CommonUtility.URLAppToken(AppToken != null ? AppToken : appToken);
                roleMaster.AuthMode = CommonUtility.GetAuthMode(AppToken != null ? AppToken : appToken).ToString();
                return View("~/Views/Admin/Masters/ManageRoleMaster.cshtml", roleMaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult DeleteRoleMaster(RoleMaster roleMaster, int roleId)
        {
            try
            {
                roleMaster.RoleId = roleId;
                RoleMaster objRoleMaster = roleMaster.RoleMaster_Delete(roleMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                roleMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                roleMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objRoleMaster != null)
                {
                    if (objRoleMaster.RoleId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/RoleMaster.cshtml", roleMaster);
        }
        #endregion

        #region Party Master
        public ActionResult PartyIndex()
        {
            PartyMaster partyMaster = new PartyMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            partyMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            partyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/PartyMaster.cshtml", partyMaster);
        }

        [HttpGet]
        public ActionResult GetPartyMaster(PartyMaster partyMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = partyMaster.PartyMaster_Get();
                dt.TableName = "PartyLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult ManagePartyMaster(int partyId = 0, string appToken = "")
        {
            PartyMaster partyMaster = new PartyMaster();
            try
            {
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                if (partyId > 0)
                {
                    partyMaster = partyMaster.GetPartyById(partyId);
                }
                partyMaster.AppToken = CommonUtility.URLAppToken(AppToken == null ? appToken : AppToken);
                partyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken == null ? appToken : AppToken).ToString();
            }
            catch (Exception ex)
            {
            }
            return View("~/Views/Admin/Masters/ManagePartyMaster.cshtml", partyMaster);
        }
        [HttpPost]
        public ActionResult ManagePartyMaster(PartyMaster partyMaster)
        {
            PartyMaster objPartyMaster = new PartyMaster();
            try
            {
                //JArray array = JArray.Parse(itemMaster.ItemMasterValues);
                partyMaster.PartyAndGstMapping = JsonConvert.DeserializeObject<List<PartyAndGstMapping>>(partyMaster.PartyMapping);
                objPartyMaster = partyMaster.PartyMaster_InsertUpdate(partyMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                partyMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                partyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objPartyMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objPartyMaster.IsSucceed)
                    {
                        ViewBag.Msg = objPartyMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objPartyMaster.IsSucceed && objPartyMaster.OfficeId != -1)
                    {
                        ViewBag.Msg = objPartyMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            PartyMaster newPartyMaster = new PartyMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newPartyMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newPartyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/PartyMaster.cshtml", (objPartyMaster.IsSucceed ? newPartyMaster : partyMaster));
        }

        [HttpPost]
        public ActionResult DeletePartyMaster(PartyMaster partyMaster, int partyId)
        {
            try
            {
                partyMaster.PartyId = partyId;
                PartyMaster objPartyMaster = partyMaster.PartyMaster_Delete(partyMaster);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                partyMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                partyMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objPartyMaster != null)
                {
                    if (objPartyMaster.PartyId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/PartyMaster.cshtml", partyMaster);
        }
        #endregion

        #region User Master
        public ActionResult UserIndex()
        {
            UserMaster ouser = new UserMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            ouser.AppToken = CommonUtility.URLAppToken(AppToken);
            ouser.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/UserMaster.cshtml", ouser);
        }

        [HttpGet]
        public ActionResult GetUserMaster(int userId = 0, string appToken = "")
        {
            try
            {
                UserMaster ouser = new UserMaster();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                ouser.AppToken = CommonUtility.URLAppToken(AppToken);
                ouser.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                List<UserMaster> userMaster = ouser.UserMaster_Get(CommonUtility.URLAppToken(appToken), CommonUtility.GetAuthMode(appToken).ToString());
                return Content(JsonConvert.SerializeObject(userMaster));
            }
            catch (Exception)
            {
                return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
            }
        }
        [HttpGet]
        public ActionResult ManageUserMaster(int userId = 0, string appToken = "")
        {
            try
            {
                UserMaster ouser = new UserMaster();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                if (userId > 0)
                {
                    ouser = ouser.GetUserById(userId);
                }
                ouser.AppToken = CommonUtility.URLAppToken(AppToken == null ? appToken : AppToken);
                ouser.AuthMode = CommonUtility.GetAuthMode(AppToken == null ? appToken : AppToken).ToString();
                return View("~/Views/Admin/Masters/ManageUserMaster.cshtml", ouser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ManageUserMaster(UserMaster userMaster)
        {
            try
            {
                int userid = userMaster.User_Id;
                UserMaster oUserMaster = userMaster.UserMaster_InsertUpdate(userMaster);
                // In case of record successfully added or updated
                if (oUserMaster.IsSucceed)
                {
                    ViewBag.Msg = oUserMaster.ActionMsg;
                }
                // In case of record already exists
                else if (!oUserMaster.IsSucceed && oUserMaster.User_Id != -1)
                {
                    ViewBag.Err = oUserMaster.ActionMsg;
                }
                // In case of any error occured
                else
                {
                    ViewBag.Err = "Unknown Error Occured !!!";

                }
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Err = "some error occurred, please try again..!";
            }
            userMaster.AppToken = CommonUtility.URLAppToken(userMaster.AppToken);
            userMaster.AuthMode = CommonUtility.GetAuthMode(userMaster.AppToken).ToString();
            return View("~/Views/Admin/Masters/ManageUserMaster.cshtml", userMaster);
        }

        [HttpPost]
        public ActionResult DeleteUserMaster(int userId, string appToken)
        {
            try
            {
                UserMaster userMaster = new UserMaster();
                userMaster.AppToken = CommonUtility.URLAppToken(appToken);
                userMaster.AuthMode = CommonUtility.GetAuthMode(appToken).ToString();
                bool iStatus = userMaster.UserMaster_Delete(userId);
                if (iStatus)
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Success", Msg = "Deleted sucessfully !" }));
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
                return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
            }
        }
        #endregion

        #region Item Master
        public ActionResult ItemIndex(string sMsg = "", string appToken = "")
        {
            if (sMsg != null && sMsg != "") { ViewBag.Msg = sMsg; }
            ItemMaster itemMaster = new ItemMaster();
            itemMaster.BarCodeLocation = Server.MapPath("~/ItemBarCode") + "\\";
            AppToken = Request.QueryString["AppToken"].ToString();
            itemMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            itemMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/ItemMaster.cshtml", itemMaster);
        }

        [HttpGet]
        public ActionResult GetItemMaster(ItemMaster itemMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = itemMaster.ItemMaster_Get();
                dt.TableName = "ItemLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult ItemMaster(int item_Id = 0, string appToken = "", string sMsg = "")
        {
            try
            {
                if (sMsg != null && sMsg != "") { ViewBag.Msg = sMsg; }
                ItemMaster itemMaster = new ItemMaster();
                itemMaster.BarCodeLocation = Server.MapPath("~/ItemBarCode") + "\\";
                if (item_Id > 0)
                {
                    itemMaster = itemMaster.ItemMaster_Get_By_Id(item_Id);
                }
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                itemMaster.AppToken = CommonUtility.URLAppToken(AppToken != null ? AppToken : appToken);
                itemMaster.AuthMode = CommonUtility.GetAuthMode(AppToken != null ? AppToken : appToken).ToString();
                return View("~/Views/Admin/Masters/ManageItemMaster.cshtml", itemMaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ManageItemMaster(ItemMaster itemMaster)
        {
            ItemMaster objItemMaster = new ItemMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            try
            {
                itemMaster.PartyAndLocationMapping = JsonConvert.DeserializeObject<List<PartyAndLocationMapping>>(itemMaster.ItemMapping);
                itemMaster.BarCodeLocation = Server.MapPath("~/ItemBarCode") + "\\";
                objItemMaster = itemMaster.ItemMaster_InsertUpdate();
                itemMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                itemMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objItemMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objItemMaster.IsSucceed)
                    {
                        ModelState.Clear();
                        return RedirectToAction("ItemIndex", new { sMsg = objItemMaster.ActionMsg, appToken = itemMaster.AppToken, });
                    }
                    // In case of record already exists
                    else if (!objItemMaster.IsSucceed && objItemMaster.ItemId != -1)
                    {
                        if (itemMaster.ItemId > 0)
                        {
                            return RedirectToAction("ItemMaster", new { item_Id = itemMaster.ItemId, appToken = itemMaster.AppToken, sMsg = objItemMaster.ActionMsg });
                        }
                        else
                        {
                            return RedirectToAction("ItemMaster", new { appToken = itemMaster.AppToken, sMsg = objItemMaster.ActionMsg });
                        }
                    }
                    // In case of any error occured
                    else
                    {
                        return RedirectToAction("ItemMaster", new { item_Id = itemMaster.ItemId, appToken = itemMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                    }
                }
                else
                {
                    if (itemMaster.ItemId > 0)
                    {
                        return RedirectToAction("ItemMaster", new { item_Id = itemMaster.ItemId, appToken = itemMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                    }
                    else
                    {
                        return RedirectToAction("ItemMaster", new { appToken = itemMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
                // to reset fields only in case of added or updated.
                if (itemMaster.ItemId > 0)
                {
                    return RedirectToAction("ItemMaster", new { item_Id = itemMaster.ItemId, appToken = itemMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                }
                else
                {
                    return RedirectToAction("ItemMaster", new { appToken = itemMaster.AppToken, sMsg = "Unknown Error Occured !!!" });
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteItemMaster(ItemMaster itemMaster, int itemId)
        {
            try
            {
                itemMaster.ItemId = itemId;
                itemMaster.BarCodeLocation = Server.MapPath("~/ItemBarCode") + "\\";
                ItemMaster objItemMaster = itemMaster.ItemMaster_Delete();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                itemMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                itemMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objItemMaster != null)
                {
                    if (objItemMaster.ItemId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/ItemMaster.cshtml", itemMaster);
        }
        #endregion

        #region HSNSAC Master
        public ActionResult HSNSACIndex()
        {
            HSN_SAC_Master hSN_SAC_Master = new HSN_SAC_Master();
            AppToken = Request.QueryString["AppToken"].ToString();
            hSN_SAC_Master.AppToken = CommonUtility.URLAppToken(AppToken);
            hSN_SAC_Master.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/HSNSACMaster.cshtml", hSN_SAC_Master);
        }

        [HttpGet]
        public ActionResult GetHSNSACMaster(HSN_SAC_Master hSN_SAC_Master, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = hSN_SAC_Master.HSN_SAC_Get();
                dt.TableName = "HSNSACLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageHSNSACMaster(HSN_SAC_Master hSN_SAC_Master)
        {
            HSN_SAC_Master objHSNSACMaster = new HSN_SAC_Master();
            try
            {
                objHSNSACMaster = hSN_SAC_Master.HSNSAC_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                hSN_SAC_Master.AppToken = CommonUtility.URLAppToken(AppToken);
                hSN_SAC_Master.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objHSNSACMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objHSNSACMaster.IsSucceed)
                    {
                        ViewBag.Msg = objHSNSACMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objHSNSACMaster.IsSucceed && objHSNSACMaster.HSN_SACID != -1)
                    {
                        ViewBag.Msg = objHSNSACMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            HSN_SAC_Master newHSNSACMaster = new HSN_SAC_Master();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newHSNSACMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newHSNSACMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/HSNSACMaster.cshtml", (objHSNSACMaster.IsSucceed ? newHSNSACMaster : hSN_SAC_Master));
        }

        [HttpPost]
        public ActionResult DeleteHSNSACMaster(HSN_SAC_Master hSN_SAC_Master, int HSNSACId)
        {
            try
            {
                hSN_SAC_Master.HSN_SACID = HSNSACId;
                HSN_SAC_Master objHSNSACMaster = hSN_SAC_Master.HSN_SAC_Delete();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                hSN_SAC_Master.AppToken = CommonUtility.URLAppToken(AppToken);
                hSN_SAC_Master.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objHSNSACMaster != null)
                {
                    if (objHSNSACMaster.HSN_SACID > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/HSNSACMaster.cshtml", hSN_SAC_Master);
        }
        #endregion

        #region Unit Master
        public ActionResult UnitIndex()
        {
            UnitMaster unitMaster = new UnitMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            unitMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            unitMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/UnitMaster.cshtml", unitMaster);
        }

        [HttpGet]
        public ActionResult GetUnitMaster(UnitMaster unitMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = unitMaster.UnitMaster_Get();
                dt.TableName = "UnitLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageUnitMaster(UnitMaster unitMaster)
        {
            UnitMaster objUnitMaster = new UnitMaster();
            try
            {
                objUnitMaster = unitMaster.UnitMaster_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                unitMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                unitMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objUnitMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objUnitMaster.IsSucceed)
                    {
                        ViewBag.Msg = objUnitMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objUnitMaster.IsSucceed && objUnitMaster.UnitId != -1)
                    {
                        ViewBag.Msg = objUnitMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            UnitMaster newUnitMaster = new UnitMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newUnitMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newUnitMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/UnitMaster.cshtml", (objUnitMaster.IsSucceed ? newUnitMaster : unitMaster));
        }

        [HttpPost]
        public ActionResult DeleteUnitMaster(UnitMaster unitMaster, int UnitId)
        {
            try
            {
                unitMaster.UnitId = UnitId;
                UnitMaster objUnitMaster = unitMaster.UnitMaster_Delete();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                unitMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                unitMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objUnitMaster != null)
                {
                    if (objUnitMaster.UnitId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/UnitMaster.cshtml", unitMaster);
        }
        #endregion

        #region Unit Conversion Master
        public ActionResult UnitConverson()
        {
            UnitConversonFactor unitConversion = new UnitConversonFactor();
            AppToken = Request.QueryString["AppToken"].ToString();
            unitConversion.AppToken = CommonUtility.URLAppToken(AppToken);
            unitConversion.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/UnitConversion.cshtml", unitConversion);
        }

        [HttpGet]
        public ActionResult GetUnitConversionMaster(UnitConversonFactor unitConversonFactor, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = unitConversonFactor.UnitConversonFactor_Get();
                dt.TableName = "UnitConversonLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageUnitConversonMaster(UnitConversonFactor unitConversonFactor)
        {
            UnitConversonFactor objUnitConversonFactor = new UnitConversonFactor();
            try
            {
                objUnitConversonFactor = unitConversonFactor.UnitConversonFactor_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                unitConversonFactor.AppToken = CommonUtility.URLAppToken(AppToken);
                unitConversonFactor.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objUnitConversonFactor != null)
                {
                    // In case of record successfully added or updated
                    if (objUnitConversonFactor.IsSucceed)
                    {
                        ViewBag.Msg = objUnitConversonFactor.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objUnitConversonFactor.IsSucceed && objUnitConversonFactor.ConversionId != -1)
                    {
                        ViewBag.Msg = objUnitConversonFactor.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            UnitConversonFactor newUnitConversonFactor = new UnitConversonFactor();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newUnitConversonFactor.AppToken = CommonUtility.URLAppToken(AppToken);
            newUnitConversonFactor.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/UnitConversion.cshtml", (objUnitConversonFactor.IsSucceed ? newUnitConversonFactor : unitConversonFactor));
        }

        [HttpPost]
        public ActionResult DeleteUnitConversionMaster(UnitConversonFactor unitConversonFactor, int ConversionId)
        {
            try
            {
                unitConversonFactor.ConversionId = ConversionId;
                UnitConversonFactor objUnitConversonFactor = unitConversonFactor.UnitConversonFactor_Delete();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                unitConversonFactor.AppToken = CommonUtility.URLAppToken(AppToken);
                unitConversonFactor.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objUnitConversonFactor != null)
                {
                    if (objUnitConversonFactor.ConversionId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/UnitConversion.cshtml", unitConversonFactor);
        }
        #endregion

        #region Group Master
        public ActionResult GroupIndex()
        {
            GroupMaster groupMaster = new GroupMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            groupMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            groupMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/GroupMaster.cshtml", groupMaster);
        }

        [HttpGet]
        public ActionResult GetGroupMaster(GroupMaster groupMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = groupMaster.GroupMaster_Get();
                dt.TableName = "GroupLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageGroupMaster(GroupMaster groupMaster)
        {
            GroupMaster objGroupMaster = new GroupMaster();
            try
            {
                objGroupMaster = groupMaster.GroupMaster_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                groupMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                groupMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objGroupMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objGroupMaster.IsSucceed)
                    {
                        ViewBag.Msg = objGroupMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objGroupMaster.IsSucceed && objGroupMaster.GroupId != -1)
                    {
                        ViewBag.Msg = objGroupMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            GroupMaster newGroupMaster = new GroupMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newGroupMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newGroupMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/GroupMaster.cshtml", (objGroupMaster.IsSucceed ? newGroupMaster : groupMaster));
        }

        [HttpPost]
        public ActionResult DeleteGroupMaster(GroupMaster groupMaster, int GroupId)
        {
            try
            {
                groupMaster.GroupId = GroupId;
                GroupMaster objGroupMaster = groupMaster.GroupMaster_Delete();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                groupMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                groupMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objGroupMaster != null)
                {
                    if (objGroupMaster.GroupId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/GroupMaster.cshtml", groupMaster);
        }
        #endregion

        #region ItemGroup Master
        public ActionResult ItemGroupIndex()
        {
            ItemGroupMaster itemGroupMaster = new ItemGroupMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            itemGroupMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            itemGroupMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/ItemGroupMaster.cshtml", itemGroupMaster);
        }

        [HttpGet]
        public ActionResult GetItemGroupMaster(ItemGroupMaster itemGroupMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = itemGroupMaster.ItemGroupMaster_Get();
                dt.TableName = "ItemGroupLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageItemGroupMaster(ItemGroupMaster itemGroupMaster)
        {
            ItemGroupMaster objItemGroupMaster = new ItemGroupMaster();
            try
            {
                objItemGroupMaster = itemGroupMaster.ItemGroupMaster_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                itemGroupMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                itemGroupMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objItemGroupMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objItemGroupMaster.IsSucceed)
                    {
                        ViewBag.Msg = objItemGroupMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objItemGroupMaster.IsSucceed && objItemGroupMaster.GroupId != -1)
                    {
                        ViewBag.Msg = objItemGroupMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            ItemGroupMaster newItemGroupMaster = new ItemGroupMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newItemGroupMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newItemGroupMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/ItemGroupMaster.cshtml", (objItemGroupMaster.IsSucceed ? newItemGroupMaster : itemGroupMaster));
        }

        [HttpPost]
        public ActionResult DeleteItemGroupMaster(ItemGroupMaster itemGroupMaster, int GroupId)
        {
            try
            {
                itemGroupMaster.GroupId = GroupId;
                ItemGroupMaster objItemGroupMaster = itemGroupMaster.ItemGroupMaster_Delete();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                itemGroupMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                itemGroupMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objItemGroupMaster != null)
                {
                    if (objItemGroupMaster.GroupId > 0)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
                    }
                    else
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                    }
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
            }
            return View("~/Views/Admin/Masters/ItemGroupMaster.cshtml", itemGroupMaster);
        }
        #endregion

        #region Stationery Master
        public ActionResult StationeryIndex()
        {
            StationeryMaster StationeryMaster = new StationeryMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            StationeryMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            StationeryMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/StationeryMaster.cshtml", StationeryMaster);
        }

        [HttpPost]
        public ActionResult ManageStationeryMaster(StationeryMaster Stationery)
        {
            try
            {

                Stationery.Stationery_Master_InsertUpdate();
                if (Stationery != null)
                {
                    // In case of record successfully added or updated
                    if (Stationery.IsSucceed)
                    {
                        ViewBag.Msg = Stationery.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!Stationery.IsSucceed && Stationery.Stationery_ID != -1)
                    {
                        ViewBag.Msg = Stationery.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            StationeryMaster newStationeryMaster = new StationeryMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newStationeryMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newStationeryMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/StationeryMaster.cshtml", (Stationery.IsSucceed ? newStationeryMaster : Stationery));
        }


        [HttpGet]
        public ActionResult GetStationeryMaster(StationeryMaster StationeryMaster, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = StationeryMaster.StationeryMaster_Get();
                dt.TableName = "StationeryLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult DeleteStationeryMaster(int StationeryId, string appToken)
        {
            try
            {
                StationeryMaster StationeryMaster = new StationeryMaster();
                StationeryMaster.AppToken = CommonUtility.URLAppToken(appToken);
                StationeryMaster.AuthMode = CommonUtility.GetAuthMode(appToken).ToString();
                bool iStatus = StationeryMaster.StationeryMaster_Delete(StationeryId);
                if (iStatus)
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Success", Msg = "Deleted sucessfully !" }));
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "some error occurred, please try again..!";
                return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
            }
        }
        #endregion

        #region Order Creation

        #endregion

        #region Order Approval 

        #endregion

        #region Matrial Purchase Master
        public ActionResult ManageVoucher(string appToken = "")
        {
            VoucherMaster voucherMaster = new VoucherMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            voucherMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            voucherMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/ManageVoucher.cshtml", voucherMaster);
        }
        public ActionResult AddVoucher(string appToken, int voucher_Id)
        {
            VoucherMaster voucherMaster = new VoucherMaster();
            AppToken = Request.QueryString["AppToken"].ToString();
            voucherMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            voucherMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            voucherMaster.VoucherId = voucher_Id > 0 ? voucher_Id : 0;
            return View("~/Views/Admin/Masters/CreateVoucher.cshtml", voucherMaster);
        }
        [HttpGet]
        public ActionResult GeVoucher(int voucherId = 0, string AppToken = "")
        {
            DataTable dt = new DataTable();
            VoucherMaster voucherMaster = new VoucherMaster();
            try
            {
                dt = voucherMaster.Voucher_Get_Data(voucherId);
                dt.TableName = "Voucher";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult SaveVoucher(VoucherMaster voucherMaster)
        {
            VoucherMaster objVoucherMaster = new VoucherMaster();
            try
            {
                voucherMaster.VoucherMappings = JsonConvert.DeserializeObject<List<VoucherMapping>>(voucherMaster.VoucherLine);
                objVoucherMaster = voucherMaster.Voucher_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                voucherMaster.AppToken = CommonUtility.URLAppToken(AppToken);
                voucherMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objVoucherMaster != null)
                {
                    // In case of record successfully added or updated
                    if (objVoucherMaster.IsSucceed)
                    {
                        ViewBag.Msg = objVoucherMaster.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objVoucherMaster.IsSucceed && objVoucherMaster.VoucherId != -1)
                    {
                        ViewBag.Msg = objVoucherMaster.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message.ToString();
            }
            VoucherMaster newVoucherMaster = new VoucherMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newVoucherMaster.AppToken = CommonUtility.URLAppToken(AppToken);
            newVoucherMaster.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Masters/CreateVoucher.cshtml", (objVoucherMaster.IsSucceed ? newVoucherMaster : voucherMaster));
        }

        #endregion

        #region BarCode
        [HttpGet]
        public ActionResult LebelPrint(int item_Id = 0, string appToken = "", string sMsg = "")
        {
            try
            {

                BarCodePrint ObjBarCodePrint = new BarCodePrint(item_Id, Server.MapPath("~/ItemBarCode") + "\\");
                ObjBarCodePrint.BarCodeImage = Request.Url.Scheme + "://" + Request.Url.Authority + "/ItemBarCode/" + Convert.ToString(item_Id) + ".jpeg";
                if (sMsg != null && sMsg != "") { ViewBag.Msg = sMsg; }
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                ObjBarCodePrint.AppToken = CommonUtility.URLAppToken(AppToken != null ? AppToken : appToken);
                ObjBarCodePrint.AuthMode = CommonUtility.GetAuthMode(AppToken != null ? AppToken : appToken).ToString();
                return View("~/Views/Admin/Masters/LabelPrint.cshtml", ObjBarCodePrint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}