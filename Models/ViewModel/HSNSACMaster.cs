using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.ViewModel
{
    public class HSN_SAC_Master
    {
        public string HSN_SACCODE { get; set; }
        public decimal Tax_Slab { get; set; }
        public int HSN_SACID { get; set; }
        public decimal CESS { get; set; }
        public decimal CESSP { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public HSN_SAC_Master()
        {
            HSN_SACCODE = "";
            Tax_Slab = 0;
        }
        public HSN_SAC_Master HSNSAC_InsertUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@HSN_SACID", HSN_SACID));
                SqlParameters.Add(new SqlParameter("@HSN_SAC", HSN_SACCODE));
                SqlParameters.Add(new SqlParameter("@Tax_Slab", Tax_Slab));
                SqlParameters.Add(new SqlParameter("@CESS", CESS));
                SqlParameters.Add(new SqlParameter("@CESSP", CESSP));
                if (!string.IsNullOrEmpty(StartDate))
                        SqlParameters.Add(new SqlParameter("@Start", Convert.ToDateTime(CommonUtility.GetDateDDMMYYYY(StartDate))));
                if (!string.IsNullOrEmpty(EndDate))
                    SqlParameters.Add(new SqlParameter("@End", Convert.ToDateTime(CommonUtility.GetDateDDMMYYYY(EndDate))));               
                SqlParameters.Add(new SqlParameter("@UserID", CommonUtility.GetLoginID()));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("HSN_SAC_InsertUpdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    HSN_SACID = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }
        public DataTable HSN_SAC_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("HSN_SAC_GetData", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
        public HSN_SAC_Master HSN_SAC_Delete()
        {

            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@HSN_SACID", HSN_SACID));
                SqlParameters.Add(new SqlParameter("@Loginid", CommonUtility.GetLoginID()));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("HSN_SAC_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    HSN_SACID = Convert.ToInt32(dr[0]);
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