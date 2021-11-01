using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IMS.Models.ViewModel
{
    public class ItemGroupMaster
    {

        public int GroupId { get; set; }
        public string Title { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }


        public ItemGroupMaster()
        {
            Loginid = CommonUtility.GetLoginID();
        }
        public ItemGroupMaster ItemGroupMaster_InsertUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Group_Id", GroupId));
                SqlParameters.Add(new SqlParameter("@Title", Title));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Item_Group_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    GroupId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }


        public DataTable ItemGroupMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Item_Group_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public ItemGroupMaster ItemGroupMaster_Delete()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Group_Id", GroupId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Item_Group_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    GroupId = Convert.ToInt32(dr[0]);
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