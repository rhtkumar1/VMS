using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

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
        public SelectList MenuModule { get; set; }

        public RoleMaster()
        {
            MenuModule = new SelectList(DDLValueFromDB.GETDATAFROMDB("Menu_Id", "Menu_Name", "Menu_Master", "And IsActive=1 AND Menu_Parent_Id is null"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }

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
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Role_Id", roleMaster.RoleId));
                SqlParameters.Add(new SqlParameter("@Loginid", roleMaster.Loginid));
                roleMaster.RoleId = DBManager.ExecuteScalar("Role_Master_Delete", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return roleMaster;
        }

        
    }
}