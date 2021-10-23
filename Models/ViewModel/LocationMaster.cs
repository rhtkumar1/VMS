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
        public string State { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }


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
                locationMaster.LocationId = DBManager.ExecuteScalar("Location_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
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
            int locationId = 0;
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Location_Id", locationMaster.LocationId));
                SqlParameters.Add(new SqlParameter("@Loginid", locationMaster.Loginid));
                locationId = DBManager.ExecuteScalar("Location_Master_Delete", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return locationMaster;
        }

        

        List<LocationData> lstLocationData = new List<LocationData>();
        public List<LocationData> Location_List_Get()
        {
            DataTable dt = new DataTable();
            LocationData locationData = new LocationData();
            try
            {
                dt = DBManager.ExecuteDataTable("Location_Master_Getdata", CommandType.StoredProcedure);
                foreach (DataRow dr in dt.Rows)
                {
                    locationData.LocationId = Convert.ToInt32(dr["Location_Id"]);
                    locationData.Title = Convert.ToString(dr["Title"]);
                    lstLocationData.Add(locationData);
                }
            }
            catch (Exception ex)
            { throw ex; }

            return lstLocationData;
        }


    }
    public class LocationData
    {
        public int LocationId { get; set; }
        public string Title { get; set; }
    }
}