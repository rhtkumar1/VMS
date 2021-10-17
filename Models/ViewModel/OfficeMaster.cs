using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IMS.Models.ViewModel
{
    public class OfficeMaster
    {

        public int OfficeId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int TypeId { get; set; }
        public int LocationId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string GSTNo { get; set; }
        public int GSTNature { get; set; }
        public string PanNo { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }


        public OfficeMaster OfficeMaster_InsertUpdate(OfficeMaster officeMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Office_Id", officeMaster.OfficeId));
                SqlParameters.Add(new SqlParameter("@Title", officeMaster.Title));
                SqlParameters.Add(new SqlParameter("@Code", officeMaster.Code));
                SqlParameters.Add(new SqlParameter("@Type_Id", officeMaster.TypeId));
                SqlParameters.Add(new SqlParameter("@Location_Id", officeMaster.LocationId));
                SqlParameters.Add(new SqlParameter("@Address1", officeMaster.Address1)); 
                SqlParameters.Add(new SqlParameter("@Address2", officeMaster.Address2));
                SqlParameters.Add(new SqlParameter("@GSTNo", officeMaster.GSTNo));
                SqlParameters.Add(new SqlParameter("@GSTNature", officeMaster.GSTNature));
                SqlParameters.Add(new SqlParameter("@PanNo", officeMaster.PanNo));
                SqlParameters.Add(new SqlParameter("@Email", officeMaster.EmailId));
                SqlParameters.Add(new SqlParameter("@ContactNo", officeMaster.ContactNo));
                SqlParameters.Add(new SqlParameter("@ContactPerson", officeMaster.ContactPerson));
                SqlParameters.Add(new SqlParameter("@Remarks", officeMaster.Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", officeMaster.Loginid));
                officeMaster.OfficeId = DBManager.ExecuteScalar("Office_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return officeMaster;
        }


        public DataTable OfficeMaster_Get(OfficeMaster officeMaster)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Office_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }


        public int OfficeMaster_Delete(OfficeMaster officeMaster)
        {
            int officeId = 0;
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Office_Id", officeMaster.OfficeId));
                SqlParameters.Add(new SqlParameter("@Loginid", officeMaster.Loginid));
                officeId = DBManager.ExecuteScalar("Office_Master_Delete", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return officeId;
            
        }


    }
}