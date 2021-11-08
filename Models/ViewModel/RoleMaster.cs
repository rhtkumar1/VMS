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
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public List<RoleMenuMapping> ObjRoleMenuMapping { get; set; }
        public string MenuMapping; //XML Formate '<RoleMaping><listnode Menu_Id="10001" Auth="3"/><listnode Menu_Id="10002" Auth="1"/></RoleMaping>'


        public RoleMaster()
        {
            ObjRoleMenuMapping = new List<RoleMenuMapping>();
            MenuModule = new SelectList(DDLValueFromDB.GETDATAFROMDB("Menu_Id", "Menu_Name", "Menu_Master", "And IsActive=1 AND Menu_Parent_Id is null"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }

        public RoleMaster RoleMaster_InsertUpdate(RoleMaster roleMaster)
        {
            try
            {
                // Role_Master_Insertupdate 0,'Test Role3','Delhi',null,1--,'<RoleMaping><listnode Menu_Id="10001" Auth="3"/><listnode Menu_Id="10002" Auth="1"/></RoleMaping>'
                string sString = string.Empty;
                foreach (var item in ObjRoleMenuMapping)
                {
                    sString += @"<listnode Menu_Id=""" + Convert.ToString(item.MenuId) + @""" Auth=""" + Convert.ToString(item.Auth) + @"""/>";
                }
                MenuMapping = "<RoleMaping>" + sString + "</RoleMaping>";
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Role_Id", roleMaster.RoleId));
                SqlParameters.Add(new SqlParameter("@Title", roleMaster.Title));
                SqlParameters.Add(new SqlParameter("@Code", roleMaster.Code));
                SqlParameters.Add(new SqlParameter("@Remarks", roleMaster.Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", roleMaster.Loginid));
                SqlParameters.Add(new SqlParameter("@MenuMaping", MenuMapping));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Role_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    RoleId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

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
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Role_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    RoleId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return roleMaster;
        }

        public DataTable Role_Menu_Mapping_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Role_Id", RoleId));
                dt = DBManager.ExecuteDataTableWithParameter("Role_Menu_Mapping_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
    }

    public class RoleMenuMapping
    {
        public int MenuId { get; set; }
        public int Auth { get; set; }
    }
}