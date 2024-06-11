using IMS.Models;
using IMS.Models.CBL;
using IMS.Models.CommonModel;
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

        [HttpGet]
        public ActionResult DeleteMatrialPurchase(int Purchase_Id, string AppToken = "")
        {
            DataSet ds = new DataSet();
            MaterialPurchase materialPurchase = new MaterialPurchase();
            try
            {
                materialPurchase = materialPurchase.MaterialPurchase_Delete(Purchase_Id);
                if (materialPurchase != null)
                {
                    // In case of record successfully added or updated
                    if (materialPurchase.IsSucceed)
                    {
                        ViewBag.Msg = materialPurchase.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!materialPurchase.IsSucceed && materialPurchase.PurchaseId != -1)
                    {
                        ViewBag.Msg = materialPurchase.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            MaterialPurchase newMaterialPurchase = new MaterialPurchase();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newMaterialPurchase.AppToken = CommonUtility.URLAppToken(AppToken);
            newMaterialPurchase.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/MaterialPurchase.cshtml", (materialPurchase.IsSucceed ? newMaterialPurchase : materialPurchase));

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
                ViewBag.Msg = ex.Message.ToString();
            }
            MaterialPurchase newMaterialPurchase = new MaterialPurchase();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newMaterialPurchase.AppToken = CommonUtility.URLAppToken(AppToken);
            newMaterialPurchase.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/MaterialPurchase.cshtml", (objMaterialPurchase.IsSucceed ? newMaterialPurchase : materialPurchase));
        }

        #endregion

        #region Order Creation

        public ActionResult OrderCreation()
        {
            MaterialOrder ObjMaterialOrder = new MaterialOrder();
            AppToken = Request.QueryString["AppToken"].ToString();
            ObjMaterialOrder.AppToken = CommonUtility.URLAppToken(AppToken);
            ObjMaterialOrder.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/OrderCreation.cshtml", ObjMaterialOrder);
        }
        [HttpGet]
        public ActionResult GetParty(int PartyId, string AppToken = "")
        {
            DataTable dt = new DataTable();
            MaterialOrder materialOrder = new MaterialOrder();
            try
            {
                dt = materialOrder.GetParty(PartyId);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult GetItemDetail(int Item_Id, int Party_Id, string AppToken = "")
        {
            DataTable dt = new DataTable();
            MaterialOrder materialOrder = new MaterialOrder();
            try
            {
                dt = materialOrder.GetItemDetail(Item_Id, Party_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpPost]
        public ActionResult ManageOrder(MaterialOrder ObjMaterialOrder)
        {
            MaterialOrder objMaterialOrder = new MaterialOrder();
            try
            {
                ObjMaterialOrder.MaterialOrderLines = JsonConvert.DeserializeObject<List<MaterialOrderLine>>(ObjMaterialOrder.MaterialLine);
                objMaterialOrder = ObjMaterialOrder.MaterialOrder_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                ObjMaterialOrder.AppToken = CommonUtility.URLAppToken(AppToken);
                ObjMaterialOrder.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objMaterialOrder != null)
                {
                    // In case of record successfully added or updated
                    if (objMaterialOrder.IsSucceed)
                    {
                        ViewBag.Msg = objMaterialOrder.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objMaterialOrder.IsSucceed && objMaterialOrder.CompanyId != -1)
                    {
                        ViewBag.Msg = objMaterialOrder.ActionMsg;
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
            MaterialOrder newMaterialOrder = new MaterialOrder();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newMaterialOrder.AppToken = CommonUtility.URLAppToken(AppToken);
            newMaterialOrder.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/OrderCreation.cshtml", (objMaterialOrder.IsSucceed ? newMaterialOrder : ObjMaterialOrder));

        }
        [HttpGet]
        public ActionResult DeleteOrder(int PO_Id)
        {
            MaterialOrder ObjMaterialOrder = new MaterialOrder();
            try
            {
                MaterialOrder objMaterialOrder = ObjMaterialOrder.MaterialOrder_Delete(PO_Id);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                ObjMaterialOrder.AppToken = CommonUtility.URLAppToken(AppToken);
                ObjMaterialOrder.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objMaterialOrder != null)
                {
                    if (objMaterialOrder.IsSucceed)
                    {
                        return Content(JsonConvert.SerializeObject(new { Status = "Sucess", Msg = objMaterialOrder.ActionMsg }));
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
                return Content(JsonConvert.SerializeObject(new { Status = "Error", Msg = ex.Message.ToString() }));
            }
        }
        [HttpGet]
        public ActionResult GetOrderInvoice(int Party_Id, string PO_No, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                MaterialOrder materialOrder = new MaterialOrder();
                dt = materialOrder.GetOrderInvoice(Party_Id, PO_No);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult SearchInvoice(int PO_Id, string PO_No, string AppToken = "")
        {
            DataSet ds = new DataSet();
            try
            {
                MaterialOrder materialOrder = new MaterialOrder();
                ds = materialOrder.MaterialOrder_Get(PO_Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }
        #endregion

        #region Bill Creation
        public ActionResult BillCreation()
        {
            BillCreation billCreation = new BillCreation();
            AppToken = Request.QueryString["AppToken"].ToString();
            billCreation.AppToken = CommonUtility.URLAppToken(AppToken);
            billCreation.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/BillCreation.cshtml", billCreation);
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
        public ActionResult GetStateSales(int PartyId, int OfficeId, string AppToken = "")
        {
            DataSet ds = new DataSet();
            try
            {
                if (PartyId > 0) // for Party and office basis
                {
                    ds = CommonModuleClass.MaterialSales_GetGST_State(PartyId, OfficeId);
                }
                else if (PartyId == 0 && OfficeId > 0)
                {
                    ds = CommonModuleClass.PartyByOfficeID(OfficeId);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        [HttpGet]
        public ActionResult GetPOData(int POId, int Office_Id, int SupplyState_Id, int Party_Id = 0, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                MaterialSales materialSales = new MaterialSales();
                dt = materialSales.MaterialSales_GetPOData(POId, Office_Id, SupplyState_Id, Party_Id);
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

        [HttpGet]
        public ActionResult DeleteMatrialSales(int SaleId, string AppToken = "")
        {
            DataSet ds = new DataSet();
            MaterialSales materialSales = new MaterialSales();
            try
            {
                materialSales = materialSales.MaterialSales_Delete(SaleId);
                if (materialSales != null)
                {
                    // In case of record successfully added or updated
                    if (materialSales.IsSucceed)
                    {
                        ViewBag.Msg = materialSales.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!materialSales.IsSucceed && materialSales.SaleId != -1)
                    {
                        ViewBag.Msg = materialSales.ActionMsg;
                    }
                    // In case of any error occured
                    else
                    {
                        ViewBag.Msg = "Unknown Error Occured !!!";

                    }
                    ModelState.Clear();
                }
            }
            catch (Exception)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            MaterialSales newMaterialSales = new MaterialSales();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newMaterialSales.AppToken = CommonUtility.URLAppToken(AppToken);
            newMaterialSales.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/MaterialSales.cshtml", (materialSales.IsSucceed ? newMaterialSales : materialSales));
        }
        [HttpGet]
        public ActionResult GetHSN_Detail_Sale(int Item_Id, int Office_Id, int P_State_Id, int Party_Id, string AppToken = "")
        {
            DataTable dt = new DataTable("ItemDetail");
            DataTable dt2 = new DataTable("PODetail");
            try
            {
                MaterialOrder ObjMaterialOrder = new MaterialOrder();
                dt = ObjMaterialOrder.GetHSN_Detail(Item_Id, Office_Id, P_State_Id);
                dt2 = ObjMaterialOrder.GetItemDetail_PartyWise(Item_Id, Party_Id);
            }
            catch (Exception)
            {
                throw;
            }
            DataSet ObjDataset = new DataSet();
            ObjDataset.Tables.Add(dt);
            ObjDataset.Tables[0].TableName = "ItemDetail";
            ObjDataset.Tables.Add(dt2);
            ObjDataset.Tables[1].TableName = "PODetail";
            //return Content(JsonConvert.SerializeObject(dt));
            return Content(JsonConvert.SerializeObject(ObjDataset));
        }

        [HttpGet]
        public ActionResult DeleteMatrialSalesLine(int SaleId, int ItemID, string AppToken = "")
        {
            DataSet ds = new DataSet();
            MaterialSalesMapping MaterialSalesLine = new MaterialSalesMapping();
            ReturnObject objR;
            try
            {
                objR = MaterialSalesLine.MaterialSalesLine_Delete(SaleId, ItemID);
            }
            catch (Exception Ex)
            { throw Ex; }
            return Content(JsonConvert.SerializeObject(objR));
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
                        ViewBag.Msg = objMaterialSales.ActionMsg;
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
                ViewBag.Msg = ex.Message.ToString();
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

        [HttpPost]
        public ActionResult ManageMaterialOrder(MaterialOrder materialOrder)
        {
            MaterialOrder objMaterialOrder = new MaterialOrder();
            try
            {
                if (!string.IsNullOrEmpty(materialOrder.POIds))
                {
                    objMaterialOrder = materialOrder.MaterialOrder_StatusUpdate();
                    AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                    materialOrder.AppToken = CommonUtility.URLAppToken(AppToken);
                    materialOrder.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                    if (objMaterialOrder != null)
                    {
                        // In case of record successfully added or updated
                        if (objMaterialOrder.IsSucceed)
                        {
                            ViewBag.Msg = objMaterialOrder.ActionMsg;
                        }
                        // In case of record already exists
                        else if (!objMaterialOrder.IsSucceed)
                        {
                            ViewBag.Msg = objMaterialOrder.ActionMsg;
                        }
                        // In case of any error occured
                        else
                        {
                            ViewBag.Msg = "Unknown Error Occured !!!";

                        }
                        ModelState.Clear();
                    }
                }
                else
                {
                    ViewBag.Msg = "Please checked at least single item !!!";

                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message.ToString();
            }
            MaterialOrder omaterialOrder = new MaterialOrder();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            omaterialOrder.AppToken = CommonUtility.URLAppToken(AppToken);
            omaterialOrder.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/MaterialOrder.cshtml", (objMaterialOrder.IsSucceed ? omaterialOrder : materialOrder));
        }
        #endregion

        #region Discount DashBoard
        public ActionResult DiscountDashboard()
        {
            DiscountDashboard discountDashboard = new DiscountDashboard();
            AppToken = Request.QueryString["AppToken"].ToString();
            discountDashboard.AppToken = CommonUtility.URLAppToken(AppToken);
            discountDashboard.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/DiscountDashboard.cshtml", discountDashboard);
        }
        [HttpGet]
        public ActionResult GetDiscountDashboard(int PartyId = 0, int SaleId = 0, string FromDate = "", string ToDate = "", string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                DiscountDashboard discountDashboard = new DiscountDashboard();
                dt = discountDashboard.DiscountDashboard_Getdata(PartyId, SaleId, FromDate, ToDate);

            }
            catch (Exception e)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult ManageDiscountDashboard(DiscountDashboard discountDashboard)
        {
            DiscountDashboard objDiscountDashboard = new DiscountDashboard();
            try
            {
                discountDashboard.Sale_Discounts = JsonConvert.DeserializeObject<List<Sale_Discount>>(discountDashboard.DisDash);
                objDiscountDashboard = discountDashboard.DiscountDashboard_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                discountDashboard.AppToken = CommonUtility.URLAppToken(AppToken);
                discountDashboard.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objDiscountDashboard != null)
                {
                    // In case of record successfully added or updated
                    if (objDiscountDashboard.IsSucceed)
                    {
                        ViewBag.Msg = objDiscountDashboard.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objDiscountDashboard.IsSucceed && objDiscountDashboard.SaleId != -1)
                    {
                        ViewBag.Msg = objDiscountDashboard.ActionMsg;
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
            DiscountDashboard newDiscountDashboard = new DiscountDashboard();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newDiscountDashboard.AppToken = CommonUtility.URLAppToken(AppToken);
            newDiscountDashboard.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/DiscountDashboard.cshtml", (objDiscountDashboard.IsSucceed ? newDiscountDashboard : discountDashboard));
        }

        #endregion

        #region Common Method
        [HttpGet]
        public ActionResult SearchParty(string Party, int OfficeId, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = CommonModuleClass.MaterialSales_Get_Party(Party, OfficeId);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult SearchPartyOrderCreation(string Party, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = CommonModuleClass.Get_Party_Order_Creation(Party);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult SearchItem(string Item, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = CommonModuleClass.Material_Get_Item(Item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult SearchOrderNo(string OrderNo, int PartyId, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = CommonModuleClass.MaterialSales_Get_OrderNo(OrderNo, PartyId);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        #endregion

        #region Matrial Get Pass Data
        public ActionResult MaterialGatepass()
        {
            GatePass gatePass = new GatePass();
            AppToken = Request.QueryString["AppToken"].ToString();
            gatePass.AppToken = CommonUtility.URLAppToken(AppToken);
            gatePass.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/MaterialGatepass.cshtml", gatePass);
        }

        [HttpGet]
        public ActionResult GetPassData(string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                GatePass gatePass = new GatePass();
                dt = gatePass.Material_GetPassData();
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        #endregion

        #region Store Clearance
        public ActionResult StoreClearance(int SaleId, string AppToken = "")
        {
            StoreClearance storeClearance = new StoreClearance();
            AppToken = Request.QueryString["AppToken"].ToString();
            storeClearance.AppToken = CommonUtility.URLAppToken(AppToken);
            storeClearance.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            storeClearance.SaleId = SaleId;
            return View("~/Views/Admin/Material/StoreClearance.cshtml", storeClearance);
        }
        [HttpGet]
        public ActionResult Material_Sale_GetStoreData(int SaleId, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                StoreClearance storeClearance = new StoreClearance();
                dt = storeClearance.Material_Sale_GetStoreData(SaleId);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        [HttpGet]
        public ActionResult Material_Sale_Item_ForBarcodegun(int SaleId, string ItemId, int officeID, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                StoreClearance storeClearance = new StoreClearance();
                dt = storeClearance.Material_Sale_Item_ForBarcodegun(SaleId, ItemId, officeID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult Material_GatePass_GetRecord(int SaleId, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                StoreClearance storeClearance = new StoreClearance();
                dt = storeClearance.Material_GatePass_GetRecord(SaleId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult MaterialStoreClearance_InsertUpdate(StoreClearance storeClearance)
        {
            StoreClearance objStoreClearance = new StoreClearance();
            try
            {
                storeClearance.StoreClearanceMappings = JsonConvert.DeserializeObject<List<StoreClearanceMapping>>(storeClearance.StoreData);
                objStoreClearance = storeClearance.MaterialStoreClearance_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                storeClearance.AppToken = CommonUtility.URLAppToken(AppToken);
                storeClearance.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objStoreClearance != null)
                {
                    // In case of record successfully added or updated
                    if (objStoreClearance.IsSucceed)
                    {
                        ViewBag.Msg = objStoreClearance.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objStoreClearance.IsSucceed && objStoreClearance.SaleId != -1)
                    {
                        ViewBag.Msg = objStoreClearance.ActionMsg;
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
            GatePass gatePass = new GatePass();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            gatePass.AppToken = CommonUtility.URLAppToken(AppToken);
            gatePass.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/MaterialGatepass.cshtml", gatePass);
        }
        #endregion

        #region StoreTransfer
        public ActionResult StoreTransfer()
        {
            StoreTransfer ObjStoreTransfer = new StoreTransfer();
            AppToken = Request.QueryString["AppToken"].ToString();
            ObjStoreTransfer.AppToken = CommonUtility.URLAppToken(AppToken);
            ObjStoreTransfer.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/StoreTransfer.cshtml", ObjStoreTransfer);
        }
        [HttpPost]
        public ActionResult StoreTransfer(StoreTransfer ObjStoreTransfer)
        {
            StoreTransfer objStoreTransfer = new StoreTransfer();
            try
            {
                ObjStoreTransfer.StoreTransferLines = JsonConvert.DeserializeObject<List<StoreTransferLine>>(ObjStoreTransfer.StoreLine);
                objStoreTransfer = ObjStoreTransfer.StoreOrder_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                objStoreTransfer.AppToken = CommonUtility.URLAppToken(AppToken);
                objStoreTransfer.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objStoreTransfer != null)
                {
                    // In case of record successfully added or updated
                    if (objStoreTransfer.IsSucceed)
                    {
                        ViewBag.Msg = objStoreTransfer.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objStoreTransfer.IsSucceed && objStoreTransfer.CompanyId != -1)
                    {
                        ViewBag.Msg = objStoreTransfer.ActionMsg;
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
            StoreTransfer newStoreTransfer = new StoreTransfer();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newStoreTransfer.AppToken = CommonUtility.URLAppToken(AppToken);
            newStoreTransfer.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/StoreTransfer.cshtml", (objStoreTransfer.IsSucceed ? newStoreTransfer : objStoreTransfer));

        }

        public ActionResult GetItemDetailStoreTransfer(int Item_Id, int Party_Id, string AppToken = "")
        {
            DataTable dt = new DataTable();
            StoreTransfer storeTransfer = new StoreTransfer();
            try
            {
                dt = storeTransfer.GetItemDetail(Item_Id, Party_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }
        #endregion

        [HttpGet]
        public ActionResult GetItem_Detail(int Item_Id, int Office_Id, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = new StoreTransfer().GetItem_Detail(Item_Id, Office_Id);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        #region Consignment
        public ActionResult ConsignmentIndex()
        {
            Consignment consignment = new Consignment();
            AppToken = Request.QueryString["AppToken"].ToString();
            consignment.AppToken = CommonUtility.URLAppToken(AppToken);
            consignment.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/Consignment.cshtml", consignment);
        }
        public ActionResult ConsignmentDashboard()
        {
            Consignment consignment = new Consignment();
            AppToken = Request.QueryString["AppToken"].ToString();
            consignment.AppToken = CommonUtility.URLAppToken(AppToken);
            consignment.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/ConsignmentDashboard.cshtml", consignment);
        }


        //[HttpGet]
        //public ActionResult Consignment_Get(string AppToken = "")
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        Consignment consignment = new Consignment();
        //        dt = consignment.Consignment_Get();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return Content(JsonConvert.SerializeObject(dt));
        //}

        [HttpGet]
        public ActionResult Consignment_Get_Bydate(DateTime fromdate, DateTime todate, string GR_No, int? GR_OfficeId, int? Vehicle_Id, int? Billing_OfficeId, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                Consignment consignment = new Consignment();
                dt = consignment.Consignment_Get_BydateFilter(fromdate, todate, GR_No, GR_OfficeId, Vehicle_Id, Billing_OfficeId);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        //[HttpGet]
        //public ActionResult GetConsigmentData(int GR_Id = 0, string appToken = "", string sMsg = "")
        //{
        //    try
        //    {
        //        if (sMsg != null && sMsg != "") { ViewBag.Msg = sMsg; }
        //        Consignment consignment = new Consignment();
        //        //        itemMaster.BarCodeLocation = Server.MapPath("~/ItemBarCode") + "\\";
        //        if (GR_Id > 0)
        //        {
        //            consignment = consignment.GetConsigmentData_By_Id(GR_Id);
        //        }
        //        AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
        //        consignment.AppToken = CommonUtility.URLAppToken(AppToken != null ? AppToken : appToken);
        //        consignment.AuthMode = CommonUtility.GetAuthMode(AppToken != null ? AppToken : appToken).ToString();
        //        return View("~/Views/Admin/Material/Consignment.cshtml", consignment);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //[HttpGet]
        //public ActionResult GetConsigmentData(int GR_Id, string AppToken = "")
        //{
        //    //DataSet ds = new DataSet();
        //    //try
        //    //{
        //    //    Consignment consignment = new Consignment();
        //    //    ds = consignment.GetConsigmentData_By_Id(GR_Id);
        //    //}
        //    //catch (Exception)
        //    //{
        //    //    throw;
        //    //}
        //    return View("~/Views/Admin/Material/Consignment.cshtml", Content(JsonConvert.SerializeObject(ds)));
        //    //return Content(JsonConvert.SerializeObject(ds));
        //}


        [HttpPost]
        public ActionResult ManageConsignment(Consignment consignment)
        {
            Consignment objConsignment = new Consignment();
            try
            {
                consignment.ConsignmentLines = JsonConvert.DeserializeObject<List<ConsignmentLine>>(consignment.CSGLine);
                objConsignment = consignment.Consignment_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                consignment.AppToken = CommonUtility.URLAppToken(AppToken);
                consignment.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objConsignment != null)
                {
                    // In case of record successfully added or updated
                    if (objConsignment.IsSucceed)
                    {
                        ViewBag.Msg = objConsignment.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objConsignment.IsSucceed)
                    {
                        ViewBag.Msg = objConsignment.ActionMsg;
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
            Consignment oconsignment = new Consignment();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            oconsignment.AppToken = CommonUtility.URLAppToken(AppToken);
            oconsignment.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/Consignment.cshtml", (objConsignment.IsSucceed ? oconsignment : consignment));
        }

        [HttpPost]
        public ActionResult DeleteConsignment(Consignment consignment, int gr_Id)
        {
            try
            {
                //financialMaster.FinancialId = financialId;
                Consignment objConsignment = consignment.Consignment_Delete(consignment);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                consignment.AppToken = CommonUtility.URLAppToken(AppToken);
                consignment.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objConsignment != null)
                {
                    if (objConsignment.GR_Id > 0)
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
            return View("~/Views/Admin/Material/ConsignmentDashboard.cshtml", consignment);
        }

        
        #endregion

    }
}