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
    public class MaterialSales
    {
        public int SaleId { get; set; }
        public string InvoiceNo { get; set; }
        public int OfficeId { get; set; }
        public int POId { get; set; }
        public int PartyId { get; set; }
        public string PartyName { get; set; }
        public int ItemId { get; set; }
        public int StateId { get; set; }
        public int SupplyStateId { get; set; }
        public string TransactionDate { get; set; }
        public decimal SaleAmount { get; set; }
        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public string Remarks { get; set; }
        public string SaleLine { get; set; }
        public bool IsActive { get; set; }
        public string Marka { get; set; }
        public string Transporter { get; set; }

        public SelectList POLists { get; set; }
        public SelectList OfficeLists { get; set; }
        public SelectList PartyLists { get; set; }
        public SelectList StateLists { get; set; }
        public SelectList HSN_SAC_Lists { get; set; }
        public SelectList Item_Lists { get; set; }
        public List<MaterialSalesMapping> MaterialSalesMappings { get; set; }
        public List<MaterialSalesMapping> MaterialMappingList { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int IsUpdateMaterialSales { get; set; }
        public int MENU_Id { get; set; }
        public SelectList UnitLists { get; set; }

        public MaterialSales()
        {
            OfficeId = CommonUtility.GetDefault_OfficeID();
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            string PartyListWhereClouse = "And IsActive=1 and Office_id =" + OfficeId.ToString();
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", PartyListWhereClouse), "Id", "Value");
            //POLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("PO_Id", "PO_No", "VW_Pending_Material_Order", ""), "Id", "Value");
            Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            MENU_Id = CommonUtility.GetActiveMenuID();
            Loginid = CommonUtility.GetLoginID();
            FinId = CommonUtility.GetFYID();
            CompanyId = CommonUtility.GetCompanyID();
            MaterialSalesMappings = new List<MaterialSalesMapping>();
            MaterialMappingList = new List<MaterialSalesMapping>();
        }
        public MaterialSales MaterialSales_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in MaterialSalesMappings)
                {
                    sb.AppendLine(@"<listnode Line_Id=""" + Convert.ToString(item.Line_Id) + @""" Item_Id=""" + Convert.ToString(item.Item_Id) + @"""   
                                    PO_Id=""" + Convert.ToString(item.PO_Id) + @""" POLine_Id=""" + Convert.ToString(item.POLine_Id) + @"""
                                    Quantity=""" + Convert.ToString(item.Quantity) + @""" HSN_SAC=""" + Convert.ToString(item.HSN_SAC) + @"""   
                                    Rate=""" + Convert.ToString(item.Rate) + @""" Amount=""" + Convert.ToString(item.Amount) + @"""   
                                    Discount_1=""" + Convert.ToString(item.Discount_1) + @""" Discount_2=""" + Convert.ToString(item.Discount_2) + @"""  
                                    IsUpdate=""" + Convert.ToString(IsUpdateMaterialSales) + @""" GST=""" + Convert.ToString(item.GST) + @"""   
                                    CGST=""" + Convert.ToString(item.CGST) + @""" SGST=""" + Convert.ToString(item.SGST) + @"""   
                                    IGST=""" + Convert.ToString(item.IGST) + @""" Total_Amount=""" + Convert.ToString(item.Total_Amount) + @"""  UnitId="""+ Convert.ToString(item.Unit_Id) + @"""
                                    />");
                }
                SaleLine = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Sale_Id", SaleId));
                SqlParameters.Add(new SqlParameter("@Invoice_No", InvoiceNo));
                SqlParameters.Add(new SqlParameter("@Office_Id", OfficeId));
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@SupplyState_Id", SupplyStateId));
                if (!string.IsNullOrEmpty(TransactionDate))
                    SqlParameters.Add(new SqlParameter("@Transaction_Date", Convert.ToDateTime(CommonUtility.GetDateDDMMYYYY(TransactionDate))));
                SqlParameters.Add(new SqlParameter("@SaleAmount", SaleAmount));
                SqlParameters.Add(new SqlParameter("@Marka", Marka));
                SqlParameters.Add(new SqlParameter("@Transporter", Transporter == null ? "" : Transporter));
                SqlParameters.Add(new SqlParameter("@Fin_Id", FinId));
                SqlParameters.Add(new SqlParameter("@Company_Id", CompanyId));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Sale_Line", SaleLine));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@MENU_Id", MENU_Id));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    SaleId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        public DataSet MaterialSales_Get(int SaleId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@SaleId", SaleId));
                ds = DBManager.ExecuteDataSetWithParameter("Material_Sale_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return ds;
        }

        public DataTable MaterialSales_GetPOData(int POId, int Office_Id, int SupplyState_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", POId));
                SqlParameters.Add(new SqlParameter("@Office_Id", Office_Id));
                SqlParameters.Add(new SqlParameter("@SupplyState_Id", SupplyState_Id));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_GetPODetail", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialSales_GetInvoice(int Party_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", Party_Id));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_GetInvoice", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable MaterialSales_GetPO_Detail()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@PO_Id", POId));
                SqlParameters.Add(new SqlParameter("@Office_Id", OfficeId));
                SqlParameters.Add(new SqlParameter("@SupplyState_Id", SupplyStateId));
                dt = DBManager.ExecuteDataTable("Material_Sale_GetPODetail", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public MaterialSales MaterialSales_Delete(int saleId)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Sale_Id", saleId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    SaleId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

    }

    public class MaterialSalesMapping
    {
        public string Line_Id { get; set; }
        public string Unit_Id { get; set; }
        public string UnitTitle { get; set; }
        public string Item_Id { get; set; }
        public string PO_Id { get; set; }
        public string POLine_Id { get; set; }
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