using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class CompanyMaster
    {
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }
        public int FinancialId { get; set; }
        //public SelectList FinancialLists { get; set; }
        public SelectList TypeLists { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Ownership { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public string StartDate { get; set; }
        public decimal OpeningBalance { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }

        public CompanyMaster()
        {
            //FinancialLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Financial_Id", "Cast(YEAR([From_date]) AS nvarchar)+'-'+Cast(YEAR([To_date]) AS nvarchar)", "Financial_Master", "And IsActive=1"), "Id", "Value");
            TypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10004 And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }

        public CompanyMaster CompanyMaster_InsertUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Company_Id", CompanyId));
                SqlParameters.Add(new SqlParameter("@Title", Title));
                SqlParameters.Add(new SqlParameter("@Code", Code));
                SqlParameters.Add(new SqlParameter("@Type_Id", TypeId));
                SqlParameters.Add(new SqlParameter("@StartDate", Convert.ToDateTime(CommonUtility.GetDateDDMMYYYY(StartDate))));
                SqlParameters.Add(new SqlParameter("@Address1", Address1));
                SqlParameters.Add(new SqlParameter("@Address2", Address2));
                SqlParameters.Add(new SqlParameter("@Ownership", Ownership));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                SqlParameters.Add(new SqlParameter("@OpeningBalance", OpeningBalance));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Company_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    CompanyId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        public DataTable CompanyMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Company_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public CompanyMaster CompanyMaster_Delete()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Company_Id", CompanyId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Company_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    CompanyId = Convert.ToInt32(dr[0]);
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