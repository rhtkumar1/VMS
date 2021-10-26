using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.ViewModel
{
    class HSN_SAC_Master
    {
        public string HSN_SACCODE { get; set; }
        public decimal Tax_Slab { get; set; }
        public int HSN_SACID { get; set; }
        public decimal CESS { get; set; }
        public decimal CESSP { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
                SqlParameters.Add(new SqlParameter("@Start", StartDate));
                SqlParameters.Add(new SqlParameter("@End", EndDate));
                SqlParameters.Add(new SqlParameter("@UserID", CommonUtility.GetLoginID()));
                HSN_SACID = DBManager.ExecuteScalar("HSN_SAC_InsertUpdate", CommandType.StoredProcedure, SqlParameters);
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
                HSN_SACID = DBManager.ExecuteScalar("HSN_SAC_Master_Delete", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

    }
}