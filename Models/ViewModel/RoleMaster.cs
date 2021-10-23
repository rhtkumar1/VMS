using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IMS.Models.ViewModel
{
    public class RoleMaster
    {
        public int RoleId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }


        public RoleMaster RoleMaster_InsertUpdate(RoleMaster roleMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Role_Id", roleMaster.RoleId));
                SqlParameters.Add(new SqlParameter("@Title", roleMaster.Title));
                SqlParameters.Add(new SqlParameter("@Code", roleMaster.Code));
                SqlParameters.Add(new SqlParameter("@Remarks", roleMaster.Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", roleMaster.Loginid));
                roleMaster.RoleId = DBManager.ExecuteScalar("Role_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return roleMaster;
        }

        public DataTable RoleMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Role_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public RoleMaster RoleMaster_Delete(RoleMaster roleMaster)
        {
            int roleId = 0;
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Role_Id", roleMaster.RoleId));
                SqlParameters.Add(new SqlParameter("@Loginid", commo));
                roleId = DBManager.ExecuteScalar("Role_Master_Delete", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return roleMaster;
        }
    }
}