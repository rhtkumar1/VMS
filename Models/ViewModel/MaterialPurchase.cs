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
        public string Purchase_Ref { get; set; }
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public int ItemId { get; set; }
        public int StateId { get; set; }
        public int SupplyStateId { get; set; }
        public string TransactionDate { get; set; }
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
        public SelectList HSN_SAC_Lists { get; set; }
        public SelectList Item_Lists { get; set; }
        public List<MaterialPurchaseMapping> MaterialPurchaseMappings { get; set; }
        public List<MaterialPurchaseMapping> MaterialMappingList { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int IsUpdateMaterialPurchase{ get; set; }
        public SelectList UnitLists { get; set; }

        public MaterialPurchase()
        {
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            HSN_SAC_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("HSN_SACID", "HSN_SAC", "HSN_SAC_Master", "And IsActive=1"), "Id", "Value");
            Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
            MaterialPurchaseMappings = new List<MaterialPurchaseMapping>();
            MaterialMappingList = new List<MaterialPurchaseMapping>();
        }
        public MaterialPurchase MaterialPurchase_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in MaterialPurchaseMappings)
                {
                    sb.AppendLine(@"<listnode Line_Id=""" + Convert.ToString(item.Line_Id) + @""" Item_Id=""" + Convert.ToString(item.Item_Id) + @"""   
                                    Quantity=""" + Convert.ToString(item.Quantity) + @""" Hsn_Sac=""" + Convert.ToString(item.HSN_SAC) + @"""   
                                    Rate=""" + Convert.ToString(item.Rate) + @""" Amount=""" + Convert.ToString(item.Amount) + @"""   
                                    Discount_1=""" + Convert.ToString(item.Discount_1) + @""" Discount_2=""" + Convert.ToString(item.Discount_2) + @"""  
                                    Taxable_Amount=""" + Convert.ToString(item.Taxable_Amount) + @""" GST=""" + Convert.ToString(item.GST) + @"""   
                                    CGST=""" + Convert.ToString(item.CGST) + @""" SGST=""" + Convert.ToString(item.SGST) + @"""   
                                    IGST=""" + Convert.ToString(item.IGST) + @""" Total_Amount=""" + Convert.ToString(item.Total_Amount) + @""" 
                                    IsUpdate=""" + Convert.ToString(IsUpdateMaterialPurchase) + @""" Unit_Id=""" + Convert.ToString(item.Unit_Id) + @"""
                                 />");
                }
                PurchaseLine = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Purchase_Id", PurchaseId));
                SqlParameters.Add(new SqlParameter("@Invoice_No", InvoiceNo));
                SqlParameters.Add(new SqlParameter("@Office_Id", OfficeId));
                SqlParameters.Add(new SqlParameter("@Purchase_Ref", Purchase_Ref));
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@SupplyState_Id", SupplyStateId));
                if (!string.IsNullOrEmpty(TransactionDate))
                    SqlParameters.Add(new SqlParameter("@Transaction_Date", Convert.ToDateTime(CommonUtility.GetDateDDMMYYYY(TransactionDate))));
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
                SqlParameters.Add(new SqlParameter("@Invoice_No", InvoiceNo));
                SqlParameters.Add(new SqlParameter("@PartyId", PartyId));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Purchase_Getdata", CommandType.StoredProcedure, SqlParameters);
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


        public DataTable GetHSN_Detail(int Item_Id, int Office_Id, int P_State_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Item_Id", Item_Id));
                SqlParameters.Add(new SqlParameter("@Office_Id", Office_Id));
                SqlParameters.Add(new SqlParameter("@P_State_Id", P_State_Id));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Purchase_GetHSN_Detail", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable GetState(int PartyId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Purchase_GetGST_State", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable GetInvoice(int Party_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", Party_Id));
                SqlParameters.Add(new SqlParameter("@InvoiceNo", ""));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Purchase_GetInvoice", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataSet GetMatrialPurchase(int Purchase_Id)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PurchaseId", Purchase_Id));
                ds = DBManager.ExecuteDataSetWithParameter("Material_Purchase_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return ds;
        }

        public MaterialPurchase MaterialPurchase_Delete(int Purchase_Id)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Purchase_Id", Purchase_Id));
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

    public class MaterialPurchaseMapping
    {
        public string Line_Id { get; set; }
        public string Unit_Id { get; set; }
        public string UnitTitle { get; set; }
        public string Item_Id { get; set; }
        public string ItemTitle { get; set; }
        public string HSN_SAC { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string Discount_1 { get; set; }
        public string Discount_2 { get; set; }
        public string Taxable_Amount { get; set; }
        public string GST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string Total_Amount { get; set; }
        public string IsUpdate { get; set; }
    }
}