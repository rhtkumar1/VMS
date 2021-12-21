using IMS.Models;
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
using System.Web.Script.Serialization;

namespace IMS.Controllers
{
    [SessionAuthentication]
    public class MaterialController : Controller
    {
        string AppToken = "";
        #region Matrial Purchase Master
        public ActionResult MaterialPurchase()
        {
            MaterialPurchase materialPurchase = new MaterialPurchase();
            AppToken = Request.QueryString["AppToken"].ToString();
            materialPurchase.AppToken = CommonUtility.URLAppToken(AppToken);
            materialPurchase.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/MaterialPurchase.cshtml", materialPurchase);
        }

        [HttpGet]
        public ActionResult GetMaterialPurchase(MaterialPurchase materialPurchase, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = materialPurchase.MaterialPurchase_Get();
                dt.TableName = "MaterialPurchase";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult GetHSN_Detail(int Item_Id, int Office_Id, int P_State_Id, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                MaterialPurchase materialPurchase = new MaterialPurchase();
                dt = materialPurchase.GetHSN_Detail(Item_Id, Office_Id, P_State_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetState(int PartyId, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                MaterialPurchase materialPurchase = new MaterialPurchase();
                dt = materialPurchase.GetState(PartyId);
                dt.TableName = "State";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetInvoice(int Party_Id, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                MaterialPurchase materialPurchase = new MaterialPurchase();
                dt = materialPurchase.GetInvoice(Party_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetMatrialPurchase(int Purchase_Id, string AppToken = "")
        {
            DataSet ds = new DataSet();
            try
            {
                MaterialPurchase materialPurchase = new MaterialPurchase();
                ds = materialPurchase.GetMatrialPurchase(Purchase_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        [HttpPost]
        public ActionResult DeleteMatrialPurchase(int Purchase_Id, string AppToken = "")
        {
            DataSet ds = new DataSet();
            try
            {
                MaterialPurchase materialPurchase = new MaterialPurchase();
                materialPurchase = materialPurchase.MaterialPurchase_Delete(Purchase_Id);
                if (materialPurchase.PurchaseId > 0 && Convert.ToBoolean(materialPurchase.IsSucceed))
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = materialPurchase.ActionMsg}));
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult ManageMaterialPurchase(MaterialPurchase materialPurchase)
        {
            MaterialPurchase objMaterialPurchase = new MaterialPurchase();
            try
            {
                materialPurchase.MaterialPurchaseMappings = JsonConvert.DeserializeObject<List<MaterialPurchaseMapping>>(materialPurchase.PurchaseLine);
                objMaterialPurchase = materialPurchase.MaterialPurchase_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                materialPurchase.AppToken = CommonUtility.URLAppToken(AppToken);
                materialPurchase.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objMaterialPurchase != null)
                {
                    // In case of record successfully added or updated
                    if (objMaterialPurchase.IsSucceed)
                    {
                        ViewBag.Success = objMaterialPurchase.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objMaterialPurchase.IsSucceed && objMaterialPurchase.PurchaseId != -1)
                    {
                        ViewBag.Msg = objMaterialPurchase.ActionMsg;
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
            MaterialPurchase newMaterialPurchase = new MaterialPurchase();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newMaterialPurchase.AppToken = CommonUtility.URLAppToken(AppToken);
            newMaterialPurchase.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/MaterialPurchase.cshtml", (objMaterialPurchase.IsSucceed ? newMaterialPurchase : materialPurchase));
        }

        #endregion

        #region Material Sales
        public ActionResult MaterialSales()
        {
            MaterialSales materialSales = new MaterialSales();
            AppToken = Request.QueryString["AppToken"].ToString();
            materialSales.AppToken = CommonUtility.URLAppToken(AppToken);
            materialSales.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/MaterialSales.cshtml", materialSales);
        }

        [HttpGet]
        public ActionResult GetStateSales(int PartyId,int OfficeId, string AppToken = "")
        {
            DataSet ds = new DataSet();
            try
            {
                MaterialSales materialSales = new MaterialSales();
                ds = materialSales.MaterialSales_GetGST_State(PartyId, OfficeId);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        [HttpGet]
        public ActionResult GetPOData(int POId, int Office_Id, int SupplyState_Id, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                MaterialSales materialSales = new MaterialSales();
                dt = materialSales.MaterialSales_GetPOData(POId, Office_Id, SupplyState_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetInvoiceSales(int Party_Id, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                MaterialSales materialSales = new MaterialSales();
                dt = materialSales.MaterialSales_GetInvoice(Party_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetMatrialSales(int SaleId, string AppToken = "")
        {
            DataSet ds = new DataSet();
            try
            {
                MaterialSales materialSales = new MaterialSales();
                ds = materialSales.MaterialSales_Get(SaleId);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        [HttpPost]
        public ActionResult DeleteMatrialSales(int SaleId, string AppToken = "")
        {
            DataSet ds = new DataSet();
            try
            {
                MaterialSales materialSales = new MaterialSales();
                materialSales = materialSales.MaterialSales_Delete(SaleId);
                if (materialSales.SaleId > 0 && Convert.ToBoolean(materialSales.IsSucceed))
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = materialSales.ActionMsg }));
                }
                else
                {
                    return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult ManageMateriaSales(MaterialSales materialSales)
        {
            MaterialSales objMaterialSales = new MaterialSales();
            try
            {
                materialSales.MaterialSalesMappings = JsonConvert.DeserializeObject<List<MaterialSalesMapping>>(materialSales.SaleLine);
                objMaterialSales = materialSales.MaterialSales_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                materialSales.AppToken = CommonUtility.URLAppToken(AppToken);
                materialSales.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objMaterialSales != null)
                {
                    // In case of record successfully added or updated
                    if (objMaterialSales.IsSucceed)
                    {
                        ViewBag.Success = objMaterialSales.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objMaterialSales.IsSucceed && objMaterialSales.SaleId != -1)
                    {
                        ViewBag.Msg = objMaterialSales.ActionMsg;
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
            MaterialSales newMaterialSales = new MaterialSales();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newMaterialSales.AppToken = CommonUtility.URLAppToken(AppToken);
            newMaterialSales.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/MaterialSales.cshtml", (objMaterialSales.IsSucceed ? newMaterialSales : materialSales));
        }
        #endregion


        #region Purchase Order Approval
        public ActionResult MaterialOrderIndex()
        {
            MaterialOrder materialOrder = new MaterialOrder();
            AppToken = Request.QueryString["AppToken"].ToString();
            materialOrder.AppToken = CommonUtility.URLAppToken(AppToken);
            materialOrder.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/MaterialOrder.cshtml", materialOrder);
        }

        [HttpGet]
        public ActionResult MaterialOrder_GetPendingOrder(string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                MaterialOrder materialOrder = new MaterialOrder();
                dt = materialOrder.MaterialOrder_GetPendingOrder();
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        #endregion
    }
}