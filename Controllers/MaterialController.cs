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
            return View("~/Views/Admin/Masters/MaterialPurchase.cshtml", materialPurchase);
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
                        ViewBag.Msg = objMaterialPurchase.ActionMsg;
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
            return View("~/Views/Admin/Masters/MaterialPurchase.cshtml", (objMaterialPurchase.IsSucceed ? newMaterialPurchase : materialPurchase));
        }

        //[HttpPost]
        //public ActionResult DeleteHSNSACMaster(HSN_SAC_Master hSN_SAC_Master, int HSNSACId)
        //{
        //    try
        //    {
        //        hSN_SAC_Master.HSN_SACID = HSNSACId;
        //        HSN_SAC_Master objHSNSACMaster = hSN_SAC_Master.HSN_SAC_Delete();
        //        AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
        //        hSN_SAC_Master.AppToken = CommonUtility.URLAppToken(AppToken);
        //        hSN_SAC_Master.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
        //        if (objHSNSACMaster != null)
        //        {
        //            if (objHSNSACMaster.HSN_SACID > 0)
        //            {
        //                return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = "Deleted sucessfully !" }));
        //            }
        //            else
        //            {
        //                return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
        //            }
        //        }
        //        else
        //        {
        //            return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = "Something went wronge !" }));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Msg = "some error occurred, please try again..!";
        //    }
        //    return View("~/Views/Admin/Masters/HSNSACMaster.cshtml", hSN_SAC_Master);
        //}
        #endregion


        #region Purchase Order Approval
        public ActionResult PurchaseOrderIndex()
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            AppToken = Request.QueryString["AppToken"].ToString();
            purchaseOrder.AppToken = CommonUtility.URLAppToken(AppToken);
            purchaseOrder.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Masters/OrderApproval.cshtml", purchaseOrder);
        }

        [HttpGet]
        public ActionResult GetOrderDetail(string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder();
                dt = purchaseOrder.PurchaseOrder_GetApproval();
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