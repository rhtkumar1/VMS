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



        public DiscountDashboard DiscountDashboard_InsertUpdate()
        {
            try
            {
                
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                //SqlParameters.Add(new SqlParameter("@XML", POId));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                //SqlParameters.Add(new SqlParameter("@MENU_Id", MENU_Id));
                SqlParameters.Add(new SqlParameter("@Office_Id", CommonUtility.GetDefault_OfficeID()));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_Discount_InsertUpdate", CommandType.StoredProcedure, SqlParameters);
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

        public DataTable DiscountDashboard_Getdata()
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