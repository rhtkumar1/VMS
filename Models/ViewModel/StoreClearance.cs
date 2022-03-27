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
    public class StoreClearance
    {
        public int SaleId { get; set; }
        public int OfficeId { get; set; }
        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public int MENU_Id { get; set; }
        public int GatePass_Id { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public string StoreData { get; set; }
        public List<StoreClearanceMapping> StoreClearanceMappings { get; set; }
        public StoreClearance()
        {
            OfficeId = CommonUtility.GetDefault_OfficeID();
            MENU_Id = CommonUtility.GetActiveMenuID();
            Loginid = CommonUtility.GetLoginID();
            FinId = CommonUtility.GetFYID();
            CompanyId = CommonUtility.GetCompanyID();
            StoreClearanceMappings = new List<StoreClearanceMapping>();
        }
        public StoreClearance MaterialStoreClearance_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in StoreClearanceMappings)
                {
                    sb.AppendLine(@"<listnode Line_Id=""" + Convert.ToString(item.Line_Id) + @"""  SaleLine_Id=""" + Convert.ToString(item.SaleLine_Id) + @""" 
                                   Item_Id=""" + Convert.ToString(item.Item_Id) + @"""  Quantity=""" + Convert.ToString(item.Quantity) + @"""
                                   Unit_Id=""" + Convert.ToString(item.Sale_Unit) + @""" />");
                }
                StoreData = "<Line>" + sb + "</Line>";
                GatePass_Id = 0;
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Sale_Id", SaleId));
                SqlParameters.Add(new SqlParameter("@Store_Line", StoreData));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@GatePass_Id", GatePass_Id));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_StoreClearance_Insertupdate", CommandType.StoredProcedure, SqlParameters);
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
        public DataTable Material_Sale_GetStoreData(int SaleId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@SaleId", SaleId));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_GetStore_Data", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
        public DataTable Material_GatePass_GetRecord(int SaleId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@SaleId", SaleId));
                dt = DBManager.ExecuteDataTableWithParameter("Material_GatePass_GetRecord", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
        public DataTable Material_Sale_Item_ForBarcodegun(int SaleId,string ItemId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@barcoadID", ItemId));
                SqlParameters.Add(new SqlParameter("@saleID", SaleId));
                dt = DBManager.ExecuteDataTableWithParameter("Sale_Item_ForBarcodegun", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }


    }
    public class StoreClearanceMapping
    {
        public string Line_Id { get; set; }
        public string SaleLine_Id { get; set; }
        public string Item_Id { get; set; }
        public string Sale_Unit { get; set; }
        public string Quantity { get; set; }
    }
}