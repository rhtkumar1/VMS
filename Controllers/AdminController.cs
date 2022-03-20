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
                return View("~/Views/Shared/DashBoard/DashBoard.cshtml", dashBoard);
            }
            else
            {
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public ActionResult GetOrderDashBoardStatus(DashBoard dashBoard, int partyId, string date)
        {
            
                DataTable dt = new DataTable();
                try
                {
                    dashBoard.PartyId = partyId;
                    dashBoard.Date = date;
                    dt = dashBoard.OrderDashboard_Get();
                    dt.TableName = "OrderDashBoard";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return Content(JsonConvert.SerializeObject(dt));
            
        }
    }
}