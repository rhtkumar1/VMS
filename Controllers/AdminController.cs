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
    //[SessionAuthentication]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult DashBoard()
        {
            if (Session["UserName"] != null)
            {
                DashBoard dashBoard = new DashBoard();
                dashBoard.AppToken = URLEncryption.Encrypt(string.Format("MID={0};AuthMode={1}", "40006", "3"));
                return View("~/Views/Shared/DashBoard/DashBoard.cshtml", dashBoard);
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public ActionResult GetOrderDashBoardStatus(DashBoard dashBoard, int partyId, string fromDate, string toDate )
        {

            DataTable dt = new DataTable();
            try
            {
                dashBoard.PartyId = partyId;
                dashBoard.FromDate = fromDate;
                dashBoard.ToDate = toDate;
                dt = dashBoard.OrderDashboard_Get();
                dt.TableName = "OrderDashBoard";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Content(JsonConvert.SerializeObject(dt));

        }

        public ActionResult ChangePassword()
        {
            UserMaster userMaster = new UserMaster();
            return View("~/Views/Admin/Masters/ChangePassword.cshtml", userMaster);
        }

        [HttpPost]
        public ActionResult ManageChangePassword(UserMaster userMaster)
        {
            UserMaster objUserMaster = new UserMaster();
            try
            {
                if (userMaster.ChangePassword == userMaster.ConfirmPassword)
                {
                    objUserMaster = userMaster.ManagePassword(userMaster);

                    if (objUserMaster != null)
                    {
                        // In case of record successfully added or updated
                        if (objUserMaster.IsSucceed)
                        {
                            ViewBag.Msg = objUserMaster.ActionMsg;
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
                    ViewBag.Msg = "Passwords do not match. Please check current password and confirm password..!";
                }

            }
            catch (Exception ex)
            {
                ViewBag.Msg = "Unknown Error Occured !!!";
            }
            // to reset fields only in case of added or updated.
            UserMaster newUserMaster = new UserMaster();
            return View("~/Views/Admin/Masters/ChangePassword.cshtml", (objUserMaster.IsSucceed ? newUserMaster : userMaster));
        }
    }
}