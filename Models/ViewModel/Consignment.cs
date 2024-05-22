using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using IMS.Models.CommonModel;
using System.Dynamic;

namespace IMS.Models.ViewModel
{
    public class Consignment
    {
        public int GR_Id { get; set; }
        public SelectList GR_List { get; set; }
        public string GR_No { get; set; }
        public SelectList Stationary_List { get; set; }
        public int TypeId { get; set; }
        public SelectList Type_List { get; set; }
        public int GR_OfficeId { get; set; }
        public SelectList Office_List { get; set; }
        public int Billing_OfficeId { get; set; }
        public DateTime GR_Date { get; set; }
        public int Client_Id { get; set; }
        public SelectList Client_List { get; set; }
        public int Consignee_Id { get; set; }
        public SelectList PartyLists { get; set; }
        public int Consigner_Id { get; set; }
        public int Origin_Id { get; set; }
        public SelectList LocationLists { get; set; }
        public int Destination_Id { get; set; }
        public int Vehicle_Id { get; set; }
        public SelectList Vehicle_List { get; set; }
        public int Contract_Id { get; set; }
        public SelectList Contract_List { get; set; }
        public int Material_Id { get; set; }
        public SelectList Material_List { get; set; }
        public string Actual_Weight { get; set; }
        public string Charge_Weight { get; set; }
        public string Quantity { get; set; }
        public int Unit_Id { get; set; }
        public SelectList UnitLists { get; set; }
        public int Rate_TypeId { get; set; }
        public string Rate { get; set; }
        public string Basic_Freight { get; set; }
        public string Loading { get; set; }
        public string Unloading { get; set; }
        public string Detention { get; set; }
        public string Other_Charge { get; set; }
        public string Two_Point_Delivery { get; set; }
        public string Pickup { get; set; }
        public string Local_Delivery { get; set; }
        public string Hamali { get; set; }
        public string Labour { get; set; }
        public string Total_Freight { get; set; }
        public string Advance_Amount { get; set; }
        public DateTime Advance_Date { get; set; }
        public int Advance_LedgerId { get; set; }
        public bool Is_Auto_Bill { get; set; }
        public bool Is_Auto_Trip { get; set; }
        public int Driver_Id { get; set; }
        public SelectList Driver_List { get; set; }
        public string Remarks { get; set; }
        public int IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int CreatedOn { get; set; }
        public int ModifiedOn { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public int IsUpdateConsignment { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int OfficeId { get; set; }

        public Consignment()
        {
            OfficeId = CommonUtility.GetDefault_OfficeID();
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            Office_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            Stationary_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Stationery_ID", "Title", "Stationery_Master", "And IsActive=1"), "Id", "Value");
            GR_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10008 And Sub_Type=1 And IsActive=1"), "Id", "Value");
            LocationLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("location_Id", "Title", "Location_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            Driver_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("User_Id", "UserName", "User_Master", "And IsActive =1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
        }

        public Consignment Consignment_InsertUpdate(Consignment consignment)
        {
            return consignment;
        }

        public DataTable Consignment_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Consignment_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
        public Consignment GetPartyById(int partyId)
        {
            try
            {
                return this;
            }
            catch (Exception ex)
            { throw ex; }
        }

        public Consignment Consignment_Delete(Consignment consignment)
        {

            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@GR_Id", consignment.GR_Id));
                SqlParameters.Add(new SqlParameter("@Loginid", consignment.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Consignment_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    GR_Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return consignment;
        }
    }
    public class MaterialDetail
    {
        public int GSTType_Id { get; set; }
        public int Location_Id { get; set; }
        public int GSTNature_Id { get; set; }
        public string GSTNo { get; set; }
    }
}