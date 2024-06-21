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
    public class RateType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Type_Id { get; set; }
        public string Remarks { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int Loginid { get; set; }
        public SelectList TypeLists { get; set; }
        public RateType()
        {
             TypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=50004 And IsActive=1"), "Id", "Value");
             Loginid = CommonUtility.GetLoginID();
        }

        public RateType RateType_InsertUpdate(RateType rateType)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", rateType.Id));
                SqlParameters.Add(new SqlParameter("@Name", rateType.Name));
                SqlParameters.Add(new SqlParameter("@Code", rateType.Code));
                SqlParameters.Add(new SqlParameter("@Type_Id", rateType.Type_Id));
                SqlParameters.Add(new SqlParameter("@Remarks", rateType.Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("RateType_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return rateType;
        }
        public DataTable RateType_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("RateType_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public RateType RateType_Delete(RateType rateType)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", rateType.Id));
                SqlParameters.Add(new SqlParameter("@Loginid", rateType.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("RateType_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return rateType;
        }
    }
}