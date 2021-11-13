using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class UnitConversonFactor
    {

        public int ConversionId { get; set; }
        public int FromUnitId { get; set; }
        public int ToUnitId { get; set; }
        public int Factor { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public SelectList FromUnitLists { get; set; }
        public SelectList ToUnitLists { get; set; }

        public UnitConversonFactor()
        {
            FromUnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            ToUnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }
        public UnitConversonFactor UnitConversonFactor_InsertUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Conversion_Id", ConversionId));
                SqlParameters.Add(new SqlParameter("@FromUnit_Id", FromUnitId));
                SqlParameters.Add(new SqlParameter("@ToUnit_Id", ToUnitId)); 
                SqlParameters.Add(new SqlParameter("@Factor", Factor));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Unit_Conversion_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    ConversionId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }


        public DataTable UnitConversonFactor_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Unit_Conversion_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public UnitConversonFactor UnitConversonFactor_Delete()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Conversion_id", ConversionId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Unit_Conversion_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    ConversionId = Convert.ToInt32(dr[0]);
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