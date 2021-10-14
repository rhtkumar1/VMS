using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IMS.Models.ViewModel
{
    public class LocationMaster
    {

        public int LocationId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int StateId { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }


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
                SqlParameters.Add(new SqlParameter("@Isactive", locationMaster.IsActive));
                SqlParameters.Add(new SqlParameter("@Loginid", locationMaster.Loginid));
                locationMaster.LocationId = DBManager.ExecuteScalar("Location_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return locationMaster;
        }

        public List<LocationMaster> LocationMaster_Get(LocationMaster locationMaster)
        {
            DataTable dt = new DataTable();
            List<LocationMaster> lstlocationMaster = new List<LocationMaster>();
            try
            {
                dt = GetLocationData(locationMaster);
                if (dt.Rows.Count > 0)
                    lstlocationMaster = CommonUtility.ConvertToList<LocationMaster>(dt);
            }
            catch (Exception ex)
            { throw ex; }
            return lstlocationMaster;
        }

        public DataTable GetLocationData(LocationMaster locationMaster)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Location_Id", locationMaster.LocationId));
                SqlParameters.Add(new SqlParameter("@Title", locationMaster.Title));
                dt = DBManager.ExecuteDataTableWithParameter("Location_Master_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }


    }
}