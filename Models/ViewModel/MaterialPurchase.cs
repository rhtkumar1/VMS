using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using IMS.Models.CommonModel;

namespace IMS.Models.ViewModel
{
    public class MaterialPurchase
    {

        public int PurchaseId { get; set; }
        public string InvoiceNo { get; set; }
        public int OfficeId { get; set; }
        public string PONo { get; set; }
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public int ItemId { get; set; }
        public int StateId { get; set; }
        public int SupplyStateId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public string Remarks { get; set; }
        public string PurchaseLine { get; set; }
        public bool IsActive { get; set; }
        public SelectList OfficeLists { get; set; }
        public SelectList PartyLists { get; set; }
        public SelectList StateLists { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        




        public MaterialPurchase()
        {
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }



        public MaterialPurchase MaterialPurchase_InsertUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Purchase_Id", PurchaseId));
                SqlParameters.Add(new SqlParameter("@Invoice_No", InvoiceNo));
                SqlParameters.Add(new SqlParameter("@Office_Id", OfficeId));
                SqlParameters.Add(new SqlParameter("@PO_No", PONo));
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@SupplyState_Id", SupplyStateId));
                SqlParameters.Add(new SqlParameter("@Transaction_Date", TransactionDate));
                SqlParameters.Add(new SqlParameter("@Purchase_Amount", PurchaseAmount));
                SqlParameters.Add(new SqlParameter("@Fin_Id", FinId));
                SqlParameters.Add(new SqlParameter("@Company_Id", CompanyId));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Purchase_Line", PurchaseLine));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Purchase_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    PurchaseId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        public DataTable MaterialPurchase_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Purchase_Id", PurchaseId));
                dt = DBManager.ExecuteDataTable("Material_Purchase_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialPurchase_GetInvoice()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@InvoiceNo", InvoiceNo));
                dt = DBManager.ExecuteDataTable("Material_Purchase_GetInvoice", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialPurchase_GetParty()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party", PartyName));
                dt = DBManager.ExecuteDataTable("Material_Purchase_GetParty", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialPurchase_GetGST_Detail()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Item_Id", ItemId));
                SqlParameters.Add(new SqlParameter("@Office_Id", OfficeId));
                SqlParameters.Add(new SqlParameter("@P_State_Id", StateId));
                dt = DBManager.ExecuteDataTable("Material_Purchase_GetHSN_Detail", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialPurchase_GetGST_State()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                dt = DBManager.ExecuteDataTable("Material_Purchase_GetGST_State", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }


        public MaterialPurchase MaterialPurchase_Delete()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Purchase_Id", PurchaseId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Purchase_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    PurchaseId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

    }
}