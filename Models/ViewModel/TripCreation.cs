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
    public class TripCreation
    {
        public int Id { get; set; }
        public int Office_Id { get; set; }
        public DateTime Load_Date { get; set; }
        public int Trip_No { get; set; }
        public int Client_Id { get; set; }
        public int Vehicle_Id { get; set; }
        public int Driver_Id { get; set; }
        public int Origin_Id { get; set; }
        public int Destination_Id { get; set; }
        public int Reporting_office_Id { get; set; }
        public decimal Start_Odo_Meter { get; set; }
        public decimal End_Odo_Meter { get; set; }
        public DateTime Reportng_Date { get; set; }
        public DateTime Loading_Date { get; set; }
        public DateTime Reach { get; set; }
        public DateTime Unload { get; set; }
        public int Movement_Type_Id { get; set; }
        public int Client_Contract_Id { get; set; }
        public decimal Frieght { get; set; }
        public decimal Hire_Frieght { get; set; }
        public string Remarks { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int Loginid { get; set; }
        public SelectList OfficeLists { get; set; }
        public SelectList LocationLists { get; set; }
        public SelectList ClientLists { get; set; }
        public SelectList Vehicle_List { get; set; }
        public SelectList Stationary_List { get; set; }
        public SelectList TypeLists { get; set; }

        public string SaleLine { get; set; }
        public int MENU_Id { get; set; }
        public int Fin_Id { get; set; }
        public int Company_Id { get; set; }
        public List<TripCreationLine> TripCreationLineList { get; set; }
        public TripCreation()
        {
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            LocationLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("location_Id", "Title", "Location_Master", "And IsActive=1"), "Id", "Value");
            ClientLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            Vehicle_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Id", "Vehicle_No", "VEHICLE_MASTER", "And IsActive=1"), "Id", "Value");
            Stationary_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Stationery_ID", "Title", "Stationery_Master", "And IsActive=1 "), "Id", "Value");

            TypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=60002   And IsActive=1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
            MENU_Id = CommonUtility.GetActiveMenuID();
            Fin_Id = CommonUtility.GetFYID();
            Company_Id = CommonUtility.GetCompanyID();
        }
        public DataTable GRDetails_Getdata(int Vehicle_Id, string loaddate)
        {
            DateTime dateTime = DateTime.Parse(loaddate);

            DateTime fromdate = dateTime.AddDays(-1);
            DateTime todate = dateTime.AddDays(1);

            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Vehicle_Id", Vehicle_Id));
                SqlParameters.Add(new SqlParameter("@From_Date", fromdate));
                SqlParameters.Add(new SqlParameter("@To_Date", todate));
                dt = DBManager.ExecuteDataTableWithParameter("GRDetails_Getdata", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public TripCreation TripCreation_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in TripCreationLineList)
                {
                    sb.AppendLine(@"<listnode    
                                GR_No=""" + Convert.ToString(item.GR_No) + @""" GR_Date=""" + Convert.ToString(item.GR_Date) + @"""
                                Charge_Weight=""" + Convert.ToString(item.Charge_Weight) + @""" Quentity=""" + Convert.ToString(item.Quentity) + @"""
                                Consignee_Id=""" + Convert.ToString(item.Consignee_Id) + @"""  Origin_Id=""" + Convert.ToString(item.Origin_Id) + @"""   
                                Total_Freight=""" + Convert.ToString(item.Total_Freight) + @""" 
                                Hire_Freight=""" + Convert.ToString(item.Hire_Freight) + @""" />");
                }
                SaleLine = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Id", Id));
                SqlParameters.Add(new SqlParameter("@Office_Id", Office_Id));
                SqlParameters.Add(new SqlParameter("@Load_Date", Load_Date));
                SqlParameters.Add(new SqlParameter("@Trip_No", Trip_No));
                SqlParameters.Add(new SqlParameter("@Client_Id", Client_Id));
                SqlParameters.Add(new SqlParameter("@Vehicle_Id", Vehicle_Id));
                SqlParameters.Add(new SqlParameter("@Driver_Id", Driver_Id));
                SqlParameters.Add(new SqlParameter("@Origin_Id", Origin_Id));
                SqlParameters.Add(new SqlParameter("@Destination_Id", Destination_Id));
                SqlParameters.Add(new SqlParameter("@Reporting_office_Id", Reporting_office_Id));
                SqlParameters.Add(new SqlParameter("@Start_Odo_Meter", Start_Odo_Meter));
                SqlParameters.Add(new SqlParameter("@End_Odo_Meter", End_Odo_Meter));
                SqlParameters.Add(new SqlParameter("@Reportng_Date", Reportng_Date));
                SqlParameters.Add(new SqlParameter("@Loading_Date", Loading_Date));
                SqlParameters.Add(new SqlParameter("@Reach", Reach));
                SqlParameters.Add(new SqlParameter("@Unload", Unload));
                SqlParameters.Add(new SqlParameter("@Movement_Type_Id", Movement_Type_Id));
                SqlParameters.Add(new SqlParameter("@Client_Contract_Id", Client_Contract_Id));
                SqlParameters.Add(new SqlParameter("@Frieght", Frieght));
                SqlParameters.Add(new SqlParameter("@Hire_Frieght", Hire_Frieght));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                //SqlParameters.Add(new SqlParameter("@Fin_Id", Fin_Id));
                SqlParameters.Add(new SqlParameter("@Fin_Id", 5));
                SqlParameters.Add(new SqlParameter("@Company_Id", Company_Id));
                SqlParameters.Add(new SqlParameter("@Sale_Line", SaleLine));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@MENU_Id", MENU_Id));

                //if (!string.IsNullOrEmpty(Bill_Date))
                //    SqlParameters.Add(new SqlParameter("@Bill_Date", Convert.ToDateTime(CommonUtility.GetDateYYYYMMDD(Bill_Date))));
                //SqlParameters.Add(new SqlParameter("@BillAmount", BillAmount));
                //if (!string.IsNullOrEmpty(Transaction_Date))
                //    SqlParameters.Add(new SqlParameter("Transaction_Date", Convert.ToDateTime(CommonUtility.GetDateYYYYMMDD(Transaction_Date))));



                DataTable dt = DBManager.ExecuteDataTableWithParameter("TripCreation_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        //filterdate not done
        //public DataTable TripCreation_Getdata(int Vehicleno, int DriverId,string tripno, string FromDate, string ToDate)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        List<SqlParameter> SqlParameters = new List<SqlParameter>();
        //        SqlParameters.Add(new SqlParameter("@Vehicleno", Vehicleno));
        //        SqlParameters.Add(new SqlParameter("@DriverId", DriverId));
        //        SqlParameters.Add(new SqlParameter("@tripno", tripno));
        //        SqlParameters.Add(new SqlParameter("@From_Date", FromDate));
        //        SqlParameters.Add(new SqlParameter("@To_Date", ToDate));
        //        dt = DBManager.ExecuteDataTableWithParameter("Material_Sale_Discount_Getdata", CommandType.StoredProcedure, SqlParameters);
        //    }
        //    catch (Exception ex)
        //    { throw ex; }

        //    return dt;
        //}

        public class TripCreationLine
        {
            //public int GR_No { get; set; }
            //public DateTime GR_Date { get; set; }
            //public decimal Charge_Weight { get; set; }
            //public decimal Quentity { get; set; }
            //public int Consignee_Id { get; set; }
            //public int Origin_Id { get; set; }
            //public decimal Total_Freight { get; set; }
            //public decimal Hire_Freight { get; set; }

            public string GR_No { get; set; }
            public string GR_Date { get; set; }
            public string Charge_Weight { get; set; }
            public string Quentity { get; set; }
            public string Consignee_Id { get; set; }
            public string Origin_Id { get; set; }
            public string Total_Freight { get; set; }
            public string Hire_Freight { get; set; }

        }
    }
}