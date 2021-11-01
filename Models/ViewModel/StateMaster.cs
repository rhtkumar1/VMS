using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class StateMaster
    {

        public int StateId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }
        public SelectList TypeLists { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }


        public StateMaster()
        {
            TypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10009 And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }
        public StateMaster StateMaster_InsertUpdate(StateMaster stateMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@State_Id", stateMaster.StateId));
                SqlParameters.Add(new SqlParameter("@Title", stateMaster.Title));
                SqlParameters.Add(new SqlParameter("@Code", stateMaster.Code));
                SqlParameters.Add(new SqlParameter("@Type_Id", stateMaster.TypeId));
                SqlParameters.Add(new SqlParameter("@Remarks", stateMaster.Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", stateMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("State_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    StateId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return stateMaster;
        }


        public DataTable StateMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("State_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public StateMaster StateMaster_Delete(StateMaster stateMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@State_Id", stateMaster.StateId));
                SqlParameters.Add(new SqlParameter("@Loginid", stateMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("State_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    StateId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return stateMaster;
        }

    }
    
}