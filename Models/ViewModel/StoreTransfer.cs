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
        public string Date { get; set; }
        public string Remarks { get; set; }
        public string StoreLine { get; set; }
        public string RefrenceNumber { get; set; }

        public SelectList Item_Lists { get; set; }
        public SelectList UnitLists { get; set; }
        public int AvailableQty { get; set; }
        public decimal LastPrice { get; set; }
        public int OrderQty { get; set; }
       

        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public bool IsActive { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int MENU_Id { get; set; }
        public List<StoreTransferLine> StoreTransferLines { get; set; }

        public StoreTransfer()
        {
            FromOffice_Id = CommonUtility.GetDefault_OfficeID();
            Loginid = CommonUtility.GetLoginID();
            MENU_Id = CommonUtility.GetActiveMenuID();
            ToOfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1 AND Office_Id !=" + FromOffice_Id.ToString()), "Id", "Value");
            FromOfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            StoreTransferLines = new List<StoreTransferLine>();
            Date = DateTime.Now.ToString("dd-MM-yyyy");
        }
        public StoreTransfer StoreOrder_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in StoreTransferLines)
                {
                    sb.AppendLine(@"<listnode Item_Id=""" + item.ItemId + @""" AvailableQty=""" + item.AvailableQty + @"""   
                                    UnitId=""" + item.UnitId + @""" OrderQty=""" + item.TransferQty + @""" Amount=""" + item.TransferAmount + @""" />");
                }
                StoreLine = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@TransfarID", FromOffice_Id));
                SqlParameters.Add(new SqlParameter("@RefrenceNumber", RefrenceNumber));
                SqlParameters.Add(new SqlParameter("@FromOffice_ID", FromOffice_Id));
                SqlParameters.Add(new SqlParameter("@ToOffice_ID", ToOffice_Id));
                SqlParameters.Add(new SqlParameter("@TransationDate", Date));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@USERID", Loginid));
                SqlParameters.Add(new SqlParameter("@Material_Line", StoreLine));
                SqlParameters.Add(new SqlParameter("@Fin_Id", CommonUtility.GetFYID()));
                SqlParameters.Add(new SqlParameter("@Company_Id", CommonUtility.GetCompanyID()));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("Store_Transfer_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            {
                IsSucceed = false;
                ActionMsg = ex.Message;
                throw ex; }
            return this;

    }

        public DataTable GetItemDetail(int Item_Id, int Party_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Item_Id", Item_Id));
                SqlParameters.Add(new SqlParameter("@Party_Id", Party_Id));
                SqlParameters.Add(new SqlParameter("@OfficeID", CommonUtility.GetDefault_OfficeID()));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Order_GetItemDetail", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable GetItem_Detail(int Item_Id, int Office_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Item_Id", Item_Id));
                SqlParameters.Add(new SqlParameter("@Office_Id", Office_Id));              
                dt = DBManager.ExecuteDataTableWithParameter("Item_Master_Stock", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
    }

    public class StoreTransferLine
    {
        public string ItemId { get; set; }
        public string UnitId { get; set; }
        public string AvailableQty { get; set; }
        public string TransferQty { get; set; }
        public string TransferAmount { get; set; }
    }
}