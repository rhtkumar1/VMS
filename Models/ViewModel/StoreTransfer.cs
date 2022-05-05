using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class StoreTransfer
    {
        public int TransfarID { get; set; }
        public int ToOffice_Id { get; set; }
        public SelectList ToOfficeLists { get; set; }
        public int FromOffice_Id { get; set; }
        public SelectList FromOfficeLists { get; set; }
        public List<StoreTransferList> StoreTransferList { get; set; }
        public string Date { get; set; }
        public string Remarks { get; set; }
        public string StoreData { get; set; }
        public string RefrenceNumber { get; set; }

        public SelectList Item_Lists { get; set; }
        public SelectList UnitLists { get; set; }
        public int AvailableQty { get; set; }
        public decimal LastPrice { get; set; }
        public int OrderQty { get; set; }


        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public string MaterialLine { get; set; }
        public bool IsActive { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int MENU_Id { get; set; }

        public StoreTransfer()
        {
            ToOffice_Id = CommonUtility.GetDefault_OfficeID();
            Loginid = CommonUtility.GetLoginID();
            MENU_Id = CommonUtility.GetActiveMenuID();
            ToOfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            FromOfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1 AND Office_Id !=" + ToOffice_Id.ToString()), "Id", "Value");
            Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            StoreTransferList = new List<StoreTransferList>();
            
        }

        //StoreTransfar_InsertUpdate

        //public StoreTransfer InsertUpdate()
        //{
        //    try
        //    {
        //        var sb = new System.Text.StringBuilder();
        //        foreach (var item in StoreTransferList)
        //        {
        //            sb.AppendLine(@"<listnode Line_Id=""" + Convert.ToString(item.Line_Id) + @"""  SaleLine_Id=""" + Convert.ToString(item.SaleLine_Id) + @""" 
        //                           Item_Id=""" + Convert.ToString(item.Item_Id) + @"""  Quantity=""" + Convert.ToString(item.Quantity) + @"""
        //                           Unit_Id=""" + Convert.ToString(item.Sale_Unit) + @"""  GatePass_Office_Id=""" + Convert.ToString(item.GatePass_Office_Id) + @""" />");
        //        }
        //        StoreData = "<Line>" + sb + "</Line>";
        //        // GatePass_Id = 0;
        //        List<SqlParameter> SqlParameters = new List<SqlParameter>();
        //        SqlParameters.Add(new SqlParameter("@TransfarID", TransfarID));
        //        SqlParameters.Add(new SqlParameter("@RefrenceNumber", RefrenceNumber));
        //        SqlParameters.Add(new SqlParameter("@FromOffice_ID", FromOffice_Id));
        //        SqlParameters.Add(new SqlParameter("@ToOffice_ID", ToOffice_Id));
        //        SqlParameters.Add(new SqlParameter("@TransationDate", Loginid));
        //        SqlParameters.Add(new SqlParameter("@Remarks", Remarks));




        //        SqlParameters.Add(new SqlParameter("@USERID", Loginid));
        //        SqlParameters.Add(new SqlParameter("@Material_Line", GatePass_Id));
        //        DataTable dt = DBManager.ExecuteDataTableWithParameter("StoreTransfar_InsertUpdate", CommandType.StoredProcedure, SqlParameters);
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            TransfarID = Convert.ToInt32(dr[0]);
        //            IsSucceed = Convert.ToBoolean(dr[1]);
        //            ActionMsg = dr[2].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }

        //    return this;
        //}

    }

    public class StoreTransferList
    {
        public int ItemId { get; set; }
        public int AvailableQty { get; set; }
        public decimal LastPrice { get; set; }
        public int OrderQty { get; set; }


    }
}