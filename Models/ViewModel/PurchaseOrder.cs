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
    public class PurchaseOrder
    {

        public int POId { get; set; }
        public string PONo { get; set; }
        public int PartyId { get; set; }
        public decimal LedgerBalance { get; set; }
        public decimal CashBalance { get; set; }
        public decimal OverdueAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PartyName { get; set; }
        public string POStatus { get; set; }
        public int LineId { get; set; }
        public int ItemId { get; set; }
        public int AvailableQty { get; set; }
        public decimal LastRate { get; set; }
        public decimal LastDiscount { get; set; }
        public decimal LastPrice { get; set; }
        public int OrderQty { get; set; }
        public decimal OrderRate { get; set; }
        public decimal Amount { get; set; }
        public string LineRemarks { get; set; }
        public int IsUpdate { get; set; }
        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public string Remarks { get; set; }
        public string PurchaseLine { get; set; }
        public bool IsActive { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int MENU_Id { get; set; }




        public PurchaseOrder()
        {
            Loginid = CommonUtility.GetLoginID();
            MENU_Id = CommonUtility.GetActiveMenuID();
        }



        public PurchaseOrder PurchaseOrder_InsertUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", POId));
                SqlParameters.Add(new SqlParameter("@PO_No", PONo));
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@LedgerBalance", LedgerBalance));
                SqlParameters.Add(new SqlParameter("@CashBalance", CashBalance));
                SqlParameters.Add(new SqlParameter("@OverdueAmount", OverdueAmount));
                SqlParameters.Add(new SqlParameter("@TotalAmount", TotalAmount));
                SqlParameters.Add(new SqlParameter("@Fin_Id", FinId));
                SqlParameters.Add(new SqlParameter("@Company_Id", CompanyId));
                SqlParameters.Add(new SqlParameter("@PO_Status", POStatus));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Purchase_Line", PurchaseLine));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@MENU_Id", MENU_Id));
                SqlParameters.Add(new SqlParameter("@Office_Id", "1"));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("Purchase_Order_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    POId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        public DataTable PurchaseOrder_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", POId));
                dt = DBManager.ExecuteDataTable("Purchase_Order_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable PurchaseOrder_GetPO()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@PO_No", PONo));
                dt = DBManager.ExecuteDataTable("Purchase_Order_GetPurchaseOrder", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable PurchaseOrder_GetParty()
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

        public DataTable PurchaseOrder_GetPartyDetail()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                dt = DBManager.ExecuteDataTable("Purchase_Order_GetPartyDetail", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }



        public PurchaseOrder PurchaseOrder_Delete()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", POId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Purchase_Order_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    POId = Convert.ToInt32(dr[0]);
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