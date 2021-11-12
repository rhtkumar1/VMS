using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IMS.Models.ViewModel
{
    // This Master is not in USE 
    // We are creating Fy at the time of Company Creation
    public class FinancialMaster
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int FinancialId { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }

        public FinancialMaster()
        {
            Loginid = CommonUtility.GetLoginID();
        }

        public FinancialMaster FinancialMaster_InsertUpdate(FinancialMaster financialMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Financial_id", financialMaster.FinancialId));
                SqlParameters.Add(new SqlParameter("@From_date", Convert.ToDateTime(CommonUtility.GetDateDDMMYYYY(financialMaster.FromDate))));
                SqlParameters.Add(new SqlParameter("@To_date", Convert.ToDateTime(CommonUtility.GetDateDDMMYYYY(financialMaster.ToDate))));
                SqlParameters.Add(new SqlParameter("@Loginid", financialMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Financial_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    FinancialId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
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

        public FinancialMaster FinancialMaster_Delete(FinancialMaster financialMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Financial_id", financialMaster.FinancialId));
                SqlParameters.Add(new SqlParameter("@Loginid", financialMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Financial_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    FinancialId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return financialMaster;
        }


    }
}