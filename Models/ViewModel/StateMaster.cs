using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IMS.Models.ViewModel
{
    public class StateMaster
    {

        public int StateId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }


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
                SqlParameters.Add(new SqlParameter("@Isactive", stateMaster.IsActive));
                SqlParameters.Add(new SqlParameter("@Loginid", stateMaster.Loginid));
                stateMaster.StateId = DBManager.ExecuteScalar("State_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return stateMaster;
        }


        public DataTable StateMaster_Get(StateMaster stateMaster)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                dt = DBManager.ExecuteDataTableWithParameter("State_Master_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

    }
}