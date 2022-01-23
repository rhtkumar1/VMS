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
        public List<MaterialOrderLine> MaterialOrderLines { get; set; }
        public string POIds { get; set; }
        public string Status { get; set; }
        public SelectList UnitLists { get; set; }


        public MaterialOrder()
        {
            Office_Id = CommonUtility.GetDefault_OfficeID();
            Loginid = CommonUtility.GetLoginID();
            MENU_Id = CommonUtility.GetActiveMenuID();
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            string PartyListWhereClouse = "And IsActive=1 and Office_id =" + Office_Id.ToString();
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", PartyListWhereClouse), "Id", "Value");
            Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            MaterialOrderLines = new List<MaterialOrderLine>();
        }



        public MaterialOrder MaterialOrder_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in MaterialOrderLines)
                {
                    sb.AppendLine(@"<listnode Line_Id=""" + Convert.ToString(item.Line_Id) + @""" Item_Id=""" + Convert.ToString(item.Item_Id) + @"""   
                                    Available_Qty=""" + Convert.ToString(item.Available_Qty) + @""" Last_Rate=""" + Convert.ToString(item.Last_Rate) + @"""
                                    Last_Discount_1=""" + Convert.ToString(item.Last_Discount_1) + @""" Last_Price=""" + Convert.ToString(item.Last_Price) + @"""   
                                    Order_Qty=""" + Convert.ToString(item.Order_Qty) + @""" Order_Rate=""" + Convert.ToString(item.Order_Rate) + @"""   
                                    Amount=""" + Convert.ToString(item.Amount) + @""" Remarks=""" + Convert.ToString(item.Remarks) + @"""  
                                    IsUpdate=""" + Convert.ToString(IsUpdate) + @""" Last_Discount_2=""" + Convert.ToString(item.Last_Discount_2) + @""" UnitId = """+ Convert.ToString(item.UnitId)+@""" />");
                }
                MaterialLine = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", POId));
                SqlParameters.Add(new SqlParameter("@PO_No", PONo));
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@LedgerBalance", LedgerBalance));
                SqlParameters.Add(new SqlParameter("@CashBalance", CashBalance));
                SqlParameters.Add(new SqlParameter("@OverdueAmount", OverdueAmount));
                SqlParameters.Add(new SqlParameter("@TotalAmount", TotalAmount));
                SqlParameters.Add(new SqlParameter("@Fin_Id", CommonUtility.GetFYID()));
                SqlParameters.Add(new SqlParameter("@Company_Id", CommonUtility.GetCompanyID()));
                SqlParameters.Add(new SqlParameter("@PO_Status", POStatus));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Material_Line", MaterialLine));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@MENU_Id", MENU_Id));
                SqlParameters.Add(new SqlParameter("@Office_Id", CommonUtility.GetDefault_OfficeID()));

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

        public DataTable GetParty(int PartyId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Order_GetPartyDetail", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable GetItemDetail(int Item_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Item_Id", Item_Id));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Order_GetItemDetail", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public MaterialOrder MaterialOrder_Delete(int POId)
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

        public DataTable GetOrderInvoice(int PartyId, string PO_No)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@PO_No", PO_No));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Order_GetOrder", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataSet MaterialOrder_Get(int PO_Id)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", PO_Id));
                ds = DBManager.ExecuteDataSetWithParameter("Material_Order_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return ds;
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

        public MaterialOrder MaterialOrder_StatusUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Ids", POIds));
                SqlParameters.Add(new SqlParameter("@Status", Status));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Order_StatusUpdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    POIds = Convert.ToString(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

    }
    public class MaterialOrderLine
    {
        public string Line_Id { get; set; }
        public string Item_Id { get; set; }
        public string Available_Qty { get; set; }
        public string Last_Rate { get; set; }
        public string Last_Discount_1 { get; set; }
        public string Last_Discount_2 { get; set; }
        public string Last_Price { get; set; }
        public string Order_Qty { get; set; }
        public string Order_Rate { get; set; }
        public string UnitId { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        public string IsUpdate { get; set; }
    }
}