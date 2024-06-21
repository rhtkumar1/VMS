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
using System.IO;
using System.Web;
using System.Web.Mvc;
using static IMS.Models.ViewModel.BillCreation;
using static IMS.Models.ViewModel.TripCreation;
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

        #region AdvanceExpenseType
        public ActionResult AdvanceExpenseType()
        {
            AdvanceExpenseType advanceExpenseType = new AdvanceExpenseType();
            AppToken = Request.QueryString["AppToken"].ToString();
            advanceExpenseType.AppToken = CommonUtility.URLAppToken(AppToken);
            advanceExpenseType.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/AdvanceExpenseType.cshtml", advanceExpenseType);
        }

        [HttpPost]
        public ActionResult ManageAdvanceExpenseType(AdvanceExpenseType advanceExpenseType)
        {
            AdvanceExpenseType objadvanceExpenseType = new AdvanceExpenseType();
            try
            {
                objadvanceExpenseType = advanceExpenseType.AdvanceExpenseType_InsertUpdate(advanceExpenseType);
                AppToken = (Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"]);
                advanceExpenseType.AppToken = CommonUtility.URLAppToken(AppToken);
                advanceExpenseType.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objadvanceExpenseType != null)
                {
                    // In case of record successfully added or updated
                    if (objadvanceExpenseType.IsSucceed)
                    {
                        ViewBag.Msg = objadvanceExpenseType.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objadvanceExpenseType.IsSucceed && objadvanceExpenseType.Id != -1)
                    {
                        ViewBag.Msg = objadvanceExpenseType.ActionMsg;
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
            AdvanceExpenseType newadvanceExpenseType  = new AdvanceExpenseType();
            //StateMaster newStateMaster = new StateMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newadvanceExpenseType.AppToken = CommonUtility.URLAppToken(AppToken);
            newadvanceExpenseType.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/AdvanceExpenseType.cshtml", (objadvanceExpenseType.IsSucceed ? newadvanceExpenseType : advanceExpenseType));
        }

        [HttpGet]
        public ActionResult GetAdvanceExpenseType(AdvanceExpenseType advanceExpenseType, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = advanceExpenseType.AdvanceExpenseType_Get();
                dt.TableName = "StateLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpPost]
        public ActionResult DeleteAdvanceExpenseType(AdvanceExpenseType advanceExpenseType, int id)
        {
            try
            {
                advanceExpenseType.Id = id;
                AdvanceExpenseType objAdvanceExpenseType = advanceExpenseType.AdvanceExpenseType_Delete(advanceExpenseType);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                advanceExpenseType.AppToken = CommonUtility.URLAppToken(AppToken);
                advanceExpenseType.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objAdvanceExpenseType != null)
                {
                    if (objAdvanceExpenseType.Id > 0)
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
            return View("~/Views/Admin/Material/AdvanceExpenseType.cshtml", advanceExpenseType);
        }

        #endregion

        #region TripCreation
        public ActionResult TripCreation()
        {
            TripCreation tripCreation = new TripCreation();
            AppToken = Request.QueryString["AppToken"].ToString();
            tripCreation.AppToken = CommonUtility.URLAppToken(AppToken);
            tripCreation.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/TripCreation.cshtml", tripCreation);
        }
        [HttpGet]
        public ActionResult GetGRDetails(int Vehicle_Id = 0, string loaddate = "",string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                TripCreation tripCreation = new TripCreation();
                dt = tripCreation.GRDetails_Getdata(Vehicle_Id, loaddate);

            }
            catch (Exception e)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }


        [HttpPost]
        public ActionResult ManageTripCreation(TripCreation tripCreation)
        {
            TripCreation objTripCreation = new TripCreation();
            try
            {
                tripCreation.TripCreationLineList = JsonConvert.DeserializeObject<List<TripCreationLine>>(tripCreation.SaleLine);
                objTripCreation = tripCreation.TripCreation_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                tripCreation.AppToken = CommonUtility.URLAppToken(AppToken);
                tripCreation.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objTripCreation != null)
                {
                    // In case of record successfully added or updated
                    if (objTripCreation.IsSucceed)
                    {
                        ViewBag.Msg = objTripCreation.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objTripCreation.IsSucceed && objTripCreation.Id != -1)
                    {
                        ViewBag.Msg = objTripCreation.ActionMsg;
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
            TripCreation newTripCreation = new TripCreation();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newTripCreation.AppToken = CommonUtility.URLAppToken(AppToken);
            newTripCreation.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/TripCreation.cshtml", (newTripCreation.IsSucceed ? newTripCreation : tripCreation));
        }

        //get filter record not done proper pending procedure
        //[HttpGet]
        //public ActionResult GetTripCreation(int Vehicleno = 0, int DriverId = 0,string tripno = "", string FromDate = "", string ToDate = "", string AppToken = "")
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        TripCreation tripCreation = new TripCreation();
        //        dt = tripCreation.TripCreation_Getdata(Vehicleno, DriverId, tripno, FromDate, ToDate);

        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //    return Content(JsonConvert.SerializeObject(dt));
        //}

        #endregion

        #region RateType

        public ActionResult RateType()
        {
            RateType rateType = new RateType();
            AppToken = Request.QueryString["AppToken"].ToString();
            rateType.AppToken = CommonUtility.URLAppToken(AppToken);
            rateType.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/RateType.cshtml", rateType);
        }

        [HttpPost]
        public ActionResult ManageRateType(RateType rateType)
        {
            RateType objrateType = new RateType();
            try
            {
                objrateType = rateType.RateType_InsertUpdate(rateType);
                AppToken = (Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"]);
                rateType.AppToken = CommonUtility.URLAppToken(AppToken);
                rateType.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objrateType != null)
                {
                    // In case of record successfully added or updated
                    if (objrateType.IsSucceed)
                    {
                        ViewBag.Msg = objrateType.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objrateType.IsSucceed && objrateType.Id != -1)
                    {
                        ViewBag.Msg = objrateType.ActionMsg;
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
            RateType newrateType = new RateType();
            //StateMaster newStateMaster = new StateMaster();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newrateType.AppToken = CommonUtility.URLAppToken(AppToken);
            newrateType.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/RateType.cshtml", (objrateType.IsSucceed ? newrateType : rateType));
        }

        [HttpGet]
        public ActionResult GetRateType(RateType rateType, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                dt = rateType.RateType_Get();
                dt.TableName = "StateLists";
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }


        [HttpPost]
        public ActionResult DeleteRateType(RateType rateType, int id)
        {
            try
            {
                rateType.Id = id;
                RateType objRateType = rateType.RateType_Delete(rateType);
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                rateType.AppToken = CommonUtility.URLAppToken(AppToken);
                rateType.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objRateType != null)
                {
                    if (objRateType.Id > 0)
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
            return View("~/Views/Admin/Material/RateType.cshtml", rateType);
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

        [HttpGet]
        public ActionResult GetBillcreationdata(int Client_Id,int HSN,int OfficeId, string AppToken = "")
        {
            DataSet ds = new DataSet();
            try
            {
                BillCreation billCreation = new BillCreation();
                ds = billCreation.Getbillcreationdata(Client_Id, HSN, OfficeId);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        [HttpPost]
        public ActionResult ManageBillCreation(BillCreation billCreation)
        {
            BillCreation objBillCreation = new BillCreation();
            try
            {
               // billCreation.GoodRecieptBillLineList = JsonConvert.DeserializeObject<List<GoodRecieptBillLine>>(billCreation.SaleLine);
                objBillCreation = billCreation.BillCreation_InsertUpdate();
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                billCreation.AppToken = CommonUtility.URLAppToken(AppToken);
                billCreation.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objBillCreation != null)
                {
                    // In case of record successfully added or updated
                    if (objBillCreation.IsSucceed)
                    {
                        ViewBag.Msg = objBillCreation.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objBillCreation.IsSucceed && objBillCreation.Bill_Id != -1)
                    {
                        ViewBag.Msg = objBillCreation.ActionMsg;
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
            BillCreation newBillCreation = new BillCreation();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newBillCreation.AppToken = CommonUtility.URLAppToken(AppToken);
            newBillCreation.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/BillCreation.cshtml", (newBillCreation.IsSucceed ? newBillCreation : billCreation));
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
        public ActionResult Consignment(int GRId=0, string AppToken = "")
        {
            Consignment consignment = new Consignment();
            AppToken = Request.QueryString["AppToken"].ToString();
            consignment.AppToken = CommonUtility.URLAppToken(AppToken);
            consignment.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/Consignment.cshtml", consignment);
        }
        public ActionResult ConsignmentDashboard(string sMsg = "", string appToken = "")
        {
            if (sMsg != null && sMsg != "") { ViewBag.Msg = sMsg; }
            Consignment consignment = new Consignment();
            AppToken = Request.QueryString["AppToken"].ToString();
            consignment.AppToken = CommonUtility.URLAppToken(AppToken);
            consignment.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/ConsignmentDashboard.cshtml", consignment);
        }


        [HttpGet]
        public ActionResult GetConsignment(string fromdate, string todate, string GR_No, int GR_OfficeId, int Vehicle_Id, int Billing_OfficeId, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                Consignment consignment = new Consignment();
                dt = consignment.GetConsignment(fromdate, todate, GR_No, GR_OfficeId, Vehicle_Id, Billing_OfficeId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetConsigmentDetail(int GR_Id = 0, string appToken = "", string sMsg = "")
        {
            try
            {
                if (sMsg != null && sMsg != "") { ViewBag.Msg = sMsg; }
                Session["GR_ID"] = "";
                Consignment consignment = new Consignment();
                Session["GR_ID"] = GR_Id;
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                consignment.AppToken = CommonUtility.URLAppToken(AppToken != null ? AppToken : appToken);
                consignment.AuthMode = CommonUtility.GetAuthMode(AppToken != null ? AppToken : appToken).ToString();
                return View("~/Views/Admin/Material/Consignment.cshtml", consignment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        public ActionResult GetConsignmentData_ByGRId(string appToken = "", string sMsg = "")
        {
            DataSet ds = new DataSet();
            Consignment consignment = new Consignment();
            try
            {
                int GR_ID = 0;
                if (!string.IsNullOrEmpty(Session["GR_ID"] as string)) {
                    GR_ID = Convert.ToInt32(Session["GR_ID"]);
                }
                ds = consignment.GetConsignmentData_ByGRId(GR_ID);
                
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(ds));
        }

        [HttpGet]
        public ActionResult GetStationaryGRNo(int GROffice_Id, string AppToken = "")
        {
            string val = "";
            try
            {
                Consignment consignment = new Consignment();
                val = consignment.GetStationaryGRNo(GROffice_Id);
            }
            catch (Exception)
            {
                throw;
            }
            
            return View("~/Views/Admin/Material/Consignment.cshtml", Content(JsonConvert.SerializeObject(val)));
            //return Content(JsonConvert.SerializeObject(ds));
        }


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

        #region Trip Advance
        public ActionResult TripAdvance()
        {
            TripAdvance tripAdavance = new TripAdvance();
            AppToken = Request.QueryString["AppToken"].ToString();
            tripAdavance.AppToken = CommonUtility.URLAppToken(AppToken);
            tripAdavance.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            return View("~/Views/Admin/Material/TripAdvance.cshtml", tripAdavance);
        }


        [HttpPost]
        public ActionResult ManageTripAdvance(TripAdvance tripAdvance, HttpPostedFileBase IMAGE)
        {
            TripAdvance objTripAdvance = new TripAdvance();
            try
            {
                if(IMAGE !=null)
                {
                    var fileName = Path.GetFileName(IMAGE.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(IMAGE.FileName);
                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                                                                              // string myfile = name + "_" + tbl.Id + ext; //appending the name with id  
                                                                              //                                           // store the file inside ~/project folder(Img)  

                    var path = Path.Combine(Server.MapPath("~/Images/TripAdvance/"), fileName);
                    // tripAdvance.IMAGE = path;


                    // var myFilePathAppsetting = System.Web.Configuration.WebConfigurationManager.AppSettings["myFilePath"].ToString();
                    tripAdvance.IMAGE = fileName;
                    IMAGE.SaveAs(path);
                }
                else if(tripAdvance.Id > 0 && IMAGE == null) //update
                {
                    tripAdvance.IMAGE = tripAdvance.IMAGEPath;
                }
                else if(IMAGE == null)//Insert
                {
                    tripAdvance.IMAGE = "";
                }
                
               
                
                // tripAdvance.TripCreationLineList = JsonConvert.DeserializeObject<List<TripCreationLine>>(tripCreation.SaleLine);

                objTripAdvance = tripAdvance.TripAdvance_InsertUpdate(tripAdvance);
                
                
                AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
                tripAdvance.AppToken = CommonUtility.URLAppToken(AppToken);
                tripAdvance.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
                if (objTripAdvance != null)
                {
                    // In case of record successfully added or updated
                    if (objTripAdvance.IsSucceed)
                    {
                        ViewBag.Msg = objTripAdvance.ActionMsg;
                    }
                    // In case of record already exists
                    else if (!objTripAdvance.IsSucceed && objTripAdvance.Id != -1)
                    {
                        ViewBag.Msg = objTripAdvance.ActionMsg;
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
            TripAdvance newTripAdvance = new TripAdvance();
            AppToken = Request.QueryString["AppToken"] == null ? Request.Form["AppToken"] : Request.QueryString["AppToken"];
            newTripAdvance.AppToken = CommonUtility.URLAppToken(AppToken);
            newTripAdvance.AuthMode = CommonUtility.GetAuthMode(AppToken).ToString();
            // to reset fields only in case of added or updated.
            return View("~/Views/Admin/Material/TripAdvance.cshtml", (newTripAdvance.IsSucceed ? newTripAdvance : tripAdvance));
        }


        [HttpGet]
        public ActionResult GetstationarygByOfficeid(int Office_Id = 0, string loaddate = "", string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                TripAdvance tripAdvance = new TripAdvance();
                dt = tripAdvance.GetstationarygByOfficeid(Office_Id);

            }
            catch (Exception e)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetAdvanceTrip(string vehicleNo, string DriverName, string AppToken = "")
        {
            DataTable dt = new DataTable();
            try
            {
                TripAdvance tripAdvance = new TripAdvance();
                dt = tripAdvance.GetAdvanceTrip(vehicleNo, DriverName);

            }
            catch (Exception e)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult DeleteTripAdvance(int TripAdvanceId,  string AppToken = "")
        {
            DataSet ds = new DataSet();
            TripAdvance tripAdvance = new TripAdvance();
            ReturnObject objR;
            try
            {
                objR = tripAdvance.TripAdvance_Delete(TripAdvanceId);
            }
            catch (Exception Ex)
            { throw Ex; }
            return Content(JsonConvert.SerializeObject(objR));
        }


        [HttpGet]
        public ActionResult EditTripAdvance(int TripAdvanceId, string AppToken = "")
        {
            DataSet ds = new DataSet();
            TripAdvance tripAdvance = new TripAdvance();
            TripAdvance objtripAdvance = new TripAdvance();
            try
            {
                objtripAdvance = tripAdvance.TripAdvance_Edit(TripAdvanceId);
            }
            catch (Exception Ex)
            { throw Ex; }
            return Json(objtripAdvance, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SearchVehicleNo(string VehicleNo,  string AppToken = "")
        {
            TripAdvance tripAdvance = new TripAdvance();
            DataTable dt = new DataTable();
            try
            {
                dt = tripAdvance.TripAdvance_Get_VehicleNo(VehicleNo);
            }
            catch (Exception)
            {
                throw;
            }
            return Content(JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult SearchDriverName(string DriverName, string AppToken = "")
        {
            TripAdvance tripAdvance = new TripAdvance();
            DataTable dt = new DataTable();
            try
            {
                dt = tripAdvance.TripAdvance_Get_DriverName(DriverName);
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