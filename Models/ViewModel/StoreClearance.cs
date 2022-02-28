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
                    sb.AppendLine(@"<listnode Item_Id=""" + Convert.ToString(item.Item_Id) + @""" Sale_Unit=""" + Convert.ToString(item.Sale_Unit) + @""" 
                                           Quantity=""" + Convert.ToString(item.Code) + @""" />");
                }
                StoreData = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Sale_Id", SaleId));
                SqlParameters.Add(new SqlParameter("@Fin_Id", FinId));
                SqlParameters.Add(new SqlParameter("@Company_Id", CompanyId));
                SqlParameters.Add(new SqlParameter("@Store_Line", StoreData));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@MENU_Id", MENU_Id));

                DataTable dt = null;//DBManager.ExecuteDataTableWithParameter("Material_Sale_Insertupdate", CommandType.StoredProcedure, SqlParameters);
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
        public DataTable Material_Sale_Item_ForBarcodegun(int SaleId,int ItemId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@itemID", ItemId));
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
        public string Item_Id { get; set; }
        public string Sale_Unit { get; set; }
        public string Code { get; set; }
    }
}