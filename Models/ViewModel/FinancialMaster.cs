using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IMS.Models.ViewModel
{
    public class FinancialMaster
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int FinancialId { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }


        public FinancialMaster FinancialMaster_InsertUpdate(FinancialMaster financialMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Financial_id", financialMaster.FinancialId));
                SqlParameters.Add(new SqlParameter("@From_date", Convert.ToDateTime(financialMaster.FromDate)));
                SqlParameters.Add(new SqlParameter("@To_date", Convert.ToDateTime(financialMaster.ToDate)));
                SqlParameters.Add(new SqlParameter("@Loginid", financialMaster.Loginid));
                financialMaster.FinancialId = DBManager.ExecuteScalar("Financial_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return financialMaster;
        }

        public DataTable FinancialMaster_Get()
        {
            DataTable dt = new DataTable();
            List<FinancialMaster> financialMaster = new List<FinancialMaster>();
            try
            {
                dt = DBManager.ExecuteDataTable("Financial_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }
            return dt;
        }

        public int FinancialMaster_Delete(FinancialMaster financialMaster)
        {
            int financialId=0;
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Financial_id", financialMaster.FinancialId));
                SqlParameters.Add(new SqlParameter("@Loginid", financialMaster.Loginid));
                financialId = DBManager.ExecuteScalar("Financial_Master_Delete", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return financialId;
        }

        List<Financial_Dropdown> lstFinancialData = new List<Financial_Dropdown>();
        public List<Financial_Dropdown> Financial_Dropdown()
        {
            DataTable dt = new DataTable();
            Financial_Dropdown financial_Dropdown = new Financial_Dropdown();
            try
            {
                dt = DBManager.ExecuteDataTable("Financial_Master_Getdata", CommandType.StoredProcedure);
                foreach (DataRow dr in dt.Rows)
                {
                    financial_Dropdown.FinancialId = Convert.ToInt32(dr["Financial_Id"]);
                    financial_Dropdown.FinancialYear = Convert.ToString(dr["From_date"]).Substring(6,9) +"-"+ Convert.ToString(dr["To_date"]).Substring(6, 9);
                    lstFinancialData.Add(financial_Dropdown);
                }
            }
            catch (Exception ex)
            { throw ex; }

            return lstFinancialData;
        }

    }
    public class Financial_Dropdown
    { 
        public int FinancialId { get; set; }
        public string FinancialYear { get; set; }
    }
}