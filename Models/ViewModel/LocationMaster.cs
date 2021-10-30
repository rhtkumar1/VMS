using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class LocationMaster
    {

        public int LocationId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int StateId { get; set; }
        public SelectList StateLists { get; set; }
        public string State { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }


        public LocationMaster()
        {
            StateLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("State_Id", "Title", "State_Master", "And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }

        public LocationMaster LocationMaster_InsertUpdate(LocationMaster locationMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Location_Id", locationMaster.LocationId));
                SqlParameters.Add(new SqlParameter("@Title", locationMaster.Title));
                SqlParameters.Add(new SqlParameter("@Code", locationMaster.Code));
                SqlParameters.Add(new SqlParameter("@State_Id", locationMaster.StateId));
                SqlParameters.Add(new SqlParameter("@Remarks", locationMaster.Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", locationMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Location_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    LocationId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return locationMaster;
        }


        public DataTable LocationMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Location_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public LocationMaster LocationMaster_Delete(LocationMaster locationMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Location_Id", locationMaster.LocationId));
                SqlParameters.Add(new SqlParameter("@Loginid", locationMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Location_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    LocationId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return locationMaster;
        }

    }

}