using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.ViewModel
{
    public class GatePass
    {
        public int Sale_Id { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int MENU_Id { get; set; }
        public int OfficeId { get; set; }
        public int Loginid { get; set; }
        public int FinId { get; set; }
        public int CompanyId { get; set; }
        public GatePass()
        {
            OfficeId = CommonUtility.GetDefault_OfficeID();
            MENU_Id = CommonUtility.GetActiveMenuID();
            Loginid = CommonUtility.GetLoginID();
            FinId = CommonUtility.GetFYID();
            CompanyId = CommonUtility.GetCompanyID();
        }
        public DataTable Material_GetPassData()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@OfficeID", OfficeId));
                dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_GatePass_Data", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
    }
}