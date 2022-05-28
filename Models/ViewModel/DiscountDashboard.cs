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
    public class DiscountDashboard
    {
        public int SaleDisId { get; set; }
        public int SaleId { get; set; }
        public string Invoice_No { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int OfficeId { get; set; }
        public int PartyId { get; set; }
        public string TransactionDate { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal BalAmount { get; set; }
        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public int Loginid { get; set; }
        public string Remarks { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public SelectList OrderList { get; set; }
        public SelectList PartyLists { get; set; }
        public List<Sale_Discount> Sale_Discounts { get; set; }
        public string DisDash { get; set; }
        public int OrderId { get; set; }

        public DiscountDashboard()
        {
            OfficeId = CommonUtility.GetDefault_OfficeID();
            OrderList = new SelectList(DDLValueFromDB.GETDATAFROMDB("PO_Id", "PO_No", "VW_Pending_Material_Order", ""), "Id", "Value");
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
            FinId = CommonUtility.GetFYID();
            CompanyId = CommonUtility.GetCompanyID();
            Sale_Discounts = new List<Sale_Discount>();
        }

        public DiscountDashboard DiscountDashboard_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in Sale_Discounts)
                {
                    sb.AppendLine(@"<listnode Sale_Id=""" + Convert.ToString(item.Sale_Id) + @""" Invoice_No=""" + Convert.ToString(item.Invoice_No) + @"""   
                              Office_Id=""" + Convert.ToString(OfficeId) + @""" Party_Id=""" + Convert.ToString(item.Party_Id) + @"""
                              Transaction_Date=""" + (string.IsNullOrEmpty(item.Transaction_Date) ? "" : CommonUtility.GetDateYYYYMMDD(item.Transaction_Date)) + @""" DiscountAmount=""" + Convert.ToString(item.DiscountAmount) + @""" 
                              BalAmount=""" + Convert.ToString(item.BalAmount) + @""" Fin_Id=""" + Convert.ToString(FinId) + @"""   
                              Company_Id=""" + Convert.ToString(CompanyId) + @""" Remarks=""" + Convert.ToString(item.Remarks) + @""" Is_Disc2_Settled=""" + Convert.ToString(item.Is_Disc2_Settled) + @""" />"); 
                }
                DisDash = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@XML", DisDash));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                //SqlParameters.Add(new SqlParameter("@SaleId", SaleId));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_Discount_InsertUpdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    SaleDisId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        public DataTable DiscountDashboard_Getdata(int PartyId, int SaleId, string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", PartyId));
                SqlParameters.Add(new SqlParameter("@From_Date", FromDate));
                SqlParameters.Add(new SqlParameter("@To_Date", ToDate));
                SqlParameters.Add(new SqlParameter("@Sale_Id", SaleId));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_Discount_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
    }
    public class Sale_Discount
    {
        public string Sale_Id { get; set; }
        public string Invoice_No { get; set; }
        public string Office_Id { get; set; }
        public string Party_Id { get; set; }
        public string Transaction_Date { get; set; }
        public string DiscountAmount { get; set; }
        public string BalAmount { get; set; }
        public string Remarks { get; set; }
        public string Is_Disc2_Settled { get; set; }
    }
}