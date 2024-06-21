using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class TripAdvance
    {
        public int Id { get; set; }
        public int Office_Id { get; set; }
        public SelectList OFFICENAME { get; set; }
        public DateTime ADVANCEDATE { get; set; }
        public string AdvanceDateString { get; set; }
        public string ADVANCENO { get; set; }
        public string VEHICLENO { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string DRIVERNAME { get; set; }
        public string TRIPNO { get; set; }
        public string ADVANCETYPE { get; set; }
        public string DRACCOUNT { get; set; }
        public string CRACCOUNT { get; set; }
        public string QTY { get; set; }
        public string RATE { get; set; }
        public string AMOUNT { get; set; }
        public string REMARK { get; set; }
        public string IMAGE { get; set; }
        public string IMAGEPath { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }


        public SelectList OfficeLists { get; set; }
        public SelectList AdvanceNoLists { get; set; }
        public SelectList VehicleNoLists { get; set; }
        public SelectList DriverNameLists { get; set; }
        public SelectList TripNoLists { get; set; }
        public SelectList AdvanceTypeLists { get; set; }
        public SelectList DrAccountLists { get; set; }
        public SelectList CrAccountLists { get; set; }

        public int MENU_Id { get; set; }
        public int Fin_Id { get; set; }
        public int Company_Id { get; set; }

        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int Loginid { get; set; }

        public string VehicleNOSearch { get; set; }
        public string DriverNOSearch { get; set; }
        public string AdvanceNOSearch { get; set; }

        public TripAdvance()
        {
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
             AdvanceNoLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Stationery_ID", "Title", "Stationery_Master", "And IsActive=1"), "Id", "Value");
             DriverNameLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
             VehicleNoLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Id", "Vehicle_No", "VEHICLE_MASTER", "And IsActive=1"), "Id", "Value");
            CrAccountLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            DrAccountLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            AdvanceTypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Id", "Type_Id", "Advance_Expense_Type", "And IsActive=1"), "Id", "Value");
            TripNoLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Id", "Trip_No", "Trip_Creation", "And IsActive=1"), "Id", "Value");


            // TypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=60002   And IsActive=1"), "Id", "Value");
             Loginid = CommonUtility.GetLoginID();
            
            MENU_Id = CommonUtility.GetActiveMenuID();
            Fin_Id = CommonUtility.GetFYID();
            Company_Id = CommonUtility.GetCompanyID();
        }

        public TripAdvance TripAdvance_InsertUpdate(TripAdvance tripAdvance)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", tripAdvance.Id));
                SqlParameters.Add(new SqlParameter("@Office_Id", tripAdvance.Office_Id));
                SqlParameters.Add(new SqlParameter("@Advance_Date", tripAdvance.ADVANCEDATE));
                SqlParameters.Add(new SqlParameter("@AdvanceNo", tripAdvance.ADVANCENO));
                SqlParameters.Add(new SqlParameter("@VehicleNo", tripAdvance.VEHICLENO));
                SqlParameters.Add(new SqlParameter("@DriverName", tripAdvance.DRIVERNAME));
                SqlParameters.Add(new SqlParameter("@TripNo", tripAdvance.TRIPNO));
                SqlParameters.Add(new SqlParameter("@AdvanceType", tripAdvance.ADVANCETYPE));
                SqlParameters.Add(new SqlParameter("@DrAccount", tripAdvance.DRACCOUNT));
                SqlParameters.Add(new SqlParameter("@CrAccount", tripAdvance.CRACCOUNT));
                SqlParameters.Add(new SqlParameter("@Quantity", tripAdvance.QTY));
                SqlParameters.Add(new SqlParameter("@Rate", tripAdvance.RATE));
                SqlParameters.Add(new SqlParameter("@Amount", tripAdvance.AMOUNT));
                SqlParameters.Add(new SqlParameter("@Remarks", tripAdvance.REMARK));
                SqlParameters.Add(new SqlParameter("@Image", tripAdvance.IMAGE));
                SqlParameters.Add(new SqlParameter("@Modifiedby", tripAdvance.Loginid));
                SqlParameters.Add(new SqlParameter("@CreatedBy", tripAdvance.Loginid));


                DataTable dt = DBManager.ExecuteDataTableWithParameter("TripAdvance_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                   // OfficeId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[0]);
                    ActionMsg = dr[1].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return tripAdvance;
        }


        public DataTable GetstationarygByOfficeid(int Office_Id)
        {
          

            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Office_Id", Office_Id));
                SqlParameters.Add(new SqlParameter("@Menu_Id", MENU_Id));
                SqlParameters.Add(new SqlParameter("@Financial_Id", Fin_Id));
                SqlParameters.Add(new SqlParameter("@Company_Id", Company_Id));
                SqlParameters.Add(new SqlParameter("@StationeryNumber", ParameterDirection.Output));

                dt = DBManager.ExecuteDataTableWithParameter("Stationery_Master_GenerateSequence", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable GetAdvanceTrip(string vehicleNo,string DriverName)
        {


            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@vehicleNo", vehicleNo));
                SqlParameters.Add(new SqlParameter("@DriverName", DriverName));
                //SqlParameters.Add(new SqlParameter("@Financial_Id", Fin_Id));
                //SqlParameters.Add(new SqlParameter("@Company_Id", Company_Id));
                //SqlParameters.Add(new SqlParameter("@StationeryNumber", ParameterDirection.Output));

                dt = DBManager.ExecuteDataTableWithParameter("TripAdvance_GetData", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public ReturnObject TripAdvance_Delete(int TripAdvanceId)
        {
            ReturnObject obj = new ReturnObject();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
              
                SqlParameters.Add(new SqlParameter("@TripAdvanceId", TripAdvanceId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("TripAdvance_Delete", CommandType.StoredProcedure, SqlParameters);

                foreach (DataRow dr in dt.Rows)
                {
                    obj.Id = Convert.ToInt32(dr[0]);
                    obj.IsSucceed = Convert.ToBoolean(dr[1]);
                    obj.ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            {
                obj.ActionMsg = ex.Message.ToString();
                //throw ex;
            }
            return obj;
        }

        public TripAdvance TripAdvance_Edit(int TripAdvanceId)
        {
            TripAdvance obj = new TripAdvance();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();

                SqlParameters.Add(new SqlParameter("@TripAdvanceId", TripAdvanceId));
               // SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("TripAdvance_Edit", CommandType.StoredProcedure, SqlParameters);

                foreach (DataRow dr in dt.Rows)
                {
                    obj.Id = Convert.ToInt32(dr[0]);
                    obj.Office_Id = Convert.ToInt16(dr[1]);
                    obj.AdvanceDateString = Convert.ToString(dr[2]);//string date

                    obj.QTY = (dr[3].ToString());
                    obj.RATE = (dr[4].ToString());
                    obj.AMOUNT = (dr[5].ToString());
                    obj.ADVANCENO = (dr[6].ToString());
                    obj.VEHICLENO = (dr[7].ToString());
                    obj.DRIVERNAME = (dr[8].ToString());
                    obj.TRIPNO =(dr[9].ToString());
                    obj.ADVANCETYPE = (dr[10].ToString());

                    obj.DRACCOUNT = (dr[11].ToString());
                    obj.CRACCOUNT = (dr[12].ToString());
                    obj.REMARK = (dr[13].ToString());
                    obj.IMAGEPath = (dr[14].ToString());

                }
            }
            catch (Exception ex)
            {
                obj.ActionMsg = ex.Message.ToString();
                //throw ex;
            }
            return obj;
        }

        public  DataTable TripAdvance_Get_VehicleNo(string VehicleNo)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                
                SqlParameters.Add(new SqlParameter("@VehicleNo", VehicleNo));
                dt = DBManager.ExecuteDataTableWithParameter("TripAdvance_Get", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
        public DataTable TripAdvance_Get_DriverName(string DriverName)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();

                SqlParameters.Add(new SqlParameter("@DriverName", DriverName));
                dt = DBManager.ExecuteDataTableWithParameter("TripAdvanceDriver_Get", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }


    }
}