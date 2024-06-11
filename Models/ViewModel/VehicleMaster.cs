using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class VehicleMaster
    {
        public int Id { get; set; }
        public string Vehicle_No { get; set; }
        public int Vehicle_Owner_Id { get; set; }
        public int Vehicle_Model_Id { get; set; }
        public string Registration_No { get; set; }
        public DateTime? Registration_Date { get; set; }
        public DateTime Manufacturing_year { get; set; }
        public int Office_Id { get; set; }
        public string Chassis_No { get; set; }
        public string Engine_No { get; set; }
        public string Trolly_No { get; set; }

        [Range(0,double.MaxValue, ErrorMessage ="Value must be gratter then or mjust be 0")]
        public decimal Gross_Weight { get; set; }
        public decimal Unload_Weight { get; set; }
        public int Vehicle_Type_Id { get; set; }
        public decimal Installment { get; set; }
        public decimal Ac_Fuel_Consumption { get; set; }
        public decimal Fuel_Avg { get; set; }
        public decimal Fuel_Avg_Empty { get; set; }
        public DateTime? Purchase_Date { get; set; }
        public decimal Purchase_Amount { get; set; }
        public DateTime? Sold_Date { get; set; }
        public decimal Sold_Amount { get; set; }
        public string Vehicle_Img { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int Loginid { get; set; }
        public SelectList Vehicle_List { get; set; }
        public SelectList Office_List { get; set; }
        public SelectList Vehicle_Model_List { get; set; }
        public SelectList TypeLists { get; set; }
        public HttpPostedFileBase UploadFile { get; set; }
        public VehicleMaster()
        {
            Vehicle_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            Office_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            Vehicle_Model_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Id", "Model_Name", "vehicle_model_master", "And IsActive=1"), "Id", "Value");
            TypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10018 And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }

        public VehicleMaster VehicleMaster_InsertUpdate(VehicleMaster vehicleMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", vehicleMaster.Id));
                SqlParameters.Add(new SqlParameter("@Vehicle_No", vehicleMaster.Vehicle_No));
                SqlParameters.Add(new SqlParameter("@Vehicle_Owner_Id", vehicleMaster.Vehicle_Owner_Id));
                SqlParameters.Add(new SqlParameter("@Vehicle_Model_Id", vehicleMaster.Vehicle_Model_Id));
                SqlParameters.Add(new SqlParameter("@Registration_No", vehicleMaster.Registration_No));
                SqlParameters.Add(new SqlParameter("@Registration_Date", vehicleMaster.Registration_Date));
                SqlParameters.Add(new SqlParameter("@Manufacturing_year", vehicleMaster.Manufacturing_year));
                SqlParameters.Add(new SqlParameter("@Office_Id", vehicleMaster.Office_Id));
                SqlParameters.Add(new SqlParameter("@Chassis_No", vehicleMaster.Chassis_No));
                SqlParameters.Add(new SqlParameter("@Engine_No", vehicleMaster.Engine_No));
                SqlParameters.Add(new SqlParameter("@Trolly_No", vehicleMaster.Trolly_No));
                SqlParameters.Add(new SqlParameter("@Gross_Weight", vehicleMaster.Gross_Weight));
                SqlParameters.Add(new SqlParameter("@Unload_Weight", vehicleMaster.Unload_Weight));
                SqlParameters.Add(new SqlParameter("@Vehicle_Type_Id", vehicleMaster.Vehicle_Type_Id));
                SqlParameters.Add(new SqlParameter("@Installment", vehicleMaster.Installment));
                SqlParameters.Add(new SqlParameter("@Ac_Fuel_Consumption", vehicleMaster.Ac_Fuel_Consumption));
                SqlParameters.Add(new SqlParameter("@Fuel_Avg", vehicleMaster.Fuel_Avg));
                SqlParameters.Add(new SqlParameter("@Fuel_Avg_Empty", vehicleMaster.Fuel_Avg_Empty));
                SqlParameters.Add(new SqlParameter("@Purchase_Date", vehicleMaster.Purchase_Date));
                SqlParameters.Add(new SqlParameter("@Purchase_Amount", vehicleMaster.Purchase_Amount));
                SqlParameters.Add(new SqlParameter("@Sold_Date", vehicleMaster.Sold_Date));
                SqlParameters.Add(new SqlParameter("@Sold_Amount", vehicleMaster.Sold_Amount));
                SqlParameters.Add(new SqlParameter("@Vehicle_Img", vehicleMaster.Vehicle_Img));
                SqlParameters.Add(new SqlParameter("@Loginid", vehicleMaster.Loginid));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("VehicleMaster_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return vehicleMaster;
        }

        public DataTable VehicleMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("VehicleMaster_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public VehicleMaster VehicleMaster_Delete(VehicleMaster vehicleMaster)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", vehicleMaster.Id));
                SqlParameters.Add(new SqlParameter("@Loginid", vehicleMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("VehicleMaster_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return vehicleMaster;
        }
    }
}