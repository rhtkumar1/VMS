using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.Models.CommonModel;

namespace IMS.Models.ViewModel
{
    public class VehicleModelMaster
    {

        public int Id { get; set; }
        public string Model_Name { get; set; }
        public string Code { get; set; }
        public int Oem_Id { get; set; }
        public int Tyre_Count { get; set; }
        public int Battery_Count { get; set; }
        public int Stephny_Count { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public SelectList Oem_List { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int Loginid { get; set; }

        public VehicleModelMaster()
        {
           // OfficeId = CommonUtility.GetDefault_OfficeID();
            Oem_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            //ConsignmentLines = new List<ConsignmentLine>();
            Loginid = CommonUtility.GetLoginID();
        }


        public VehicleModelMaster VehicleModelMaster_InsertUpdate(VehicleModelMaster vehicleModelMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", vehicleModelMaster.Id));
                SqlParameters.Add(new SqlParameter("@Model_Name", vehicleModelMaster.Model_Name));
                SqlParameters.Add(new SqlParameter("@Code", vehicleModelMaster.Code));
                SqlParameters.Add(new SqlParameter("@Oem_Id", vehicleModelMaster.Oem_Id));
                SqlParameters.Add(new SqlParameter("@Tyre_Count", vehicleModelMaster.Tyre_Count));
                SqlParameters.Add(new SqlParameter("@Battery_Count", vehicleModelMaster.Battery_Count));
                SqlParameters.Add(new SqlParameter("@Stephny_Count", vehicleModelMaster.Stephny_Count));
                SqlParameters.Add(new SqlParameter("@Loginid", vehicleModelMaster.Loginid));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("VehicleModelMaster_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return vehicleModelMaster;
        }

        public DataTable VehicleModelMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("VehicleModelMaster_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public VehicleModelMaster VehicleModelMaster_Delete(VehicleModelMaster vehicleModelMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", vehicleModelMaster.Id));
                SqlParameters.Add(new SqlParameter("@Loginid", vehicleModelMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("VehicleModelMaster_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return vehicleModelMaster;
        }
    }
}