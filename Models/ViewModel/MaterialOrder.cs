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
    public class MaterialOrder
    {

        public int POId { get; set; }
        public string PONo { get; set; }
        public string PODate { get; set; }
        public int PartyId { get; set; }
        public SelectList PartyLists { get; set; }
        public decimal LedgerBalance { get; set; }
        public decimal CashBalance { get; set; }
        public decimal OverdueAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string PartyName { get; set; }
        public string POStatus { get; set; }
        public int LineId { get; set; }
        public int ItemId { get; set; }
        public SelectList Item_Lists { get; set; }
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
        public string MaterialLine { get; set; }
        public bool IsActive { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int MENU_Id { get; set; }
        public int Office_Id { get; set; }
        public SelectList OfficeLists { get; set; }



        public MaterialOrder()
        {
            Loginid = CommonUtility.GetLoginID();
            MENU_Id = CommonUtility.GetActiveMenuID();
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
        }



        public MaterialOrder MaterialOrder_InsertUpdate()
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
                SqlParameters.Add(new SqlParameter("@Material_Line", MaterialLine));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@MENU_Id", MENU_Id));
                SqlParameters.Add(new SqlParameter("@Office_Id", "1"));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Order_Insertupdate", CommandType.StoredProcedure, SqlParameters);
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

        public DataTable MaterialOrder_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", POId));
                dt = DBManager.ExecuteDataTable("Material_Order_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialOrder_GetOrderNo()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@PO_No", PONo));
                dt = DBManager.ExecuteDataTable("Material_Order_GetOrder", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialOrder_GetParty()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party", PartyName));
                dt = DBManager.ExecuteDataTable("Material_Material_GetParty", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialOrder_GetPartyDetail()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                dt = DBManager.ExecuteDataTable("Material_Order_GetPartyDetail", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialOrder_GetPendingOrder()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Material_Order_GetPendingOrder", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }



        public MaterialOrder MaterialOrder_Delete()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", POId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Order_Delete", CommandType.StoredProcedure, SqlParameters);
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