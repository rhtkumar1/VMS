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
    public class BillCreation
    {

        public int Bill_Id { get; set; }
        public string Bill_No { get; set; }
        public DateTime Bill_Date { get; set; }
        public int Type_Id { get; set; }
        public int Billing_OfficeId { get; set; }
        public int Client_Id { get; set; }
        public int SupplyState_Id { get; set; }
        public int GST_TypeId { get; set; }
        public int HSN_SAC { get; set; }
        public DateTime Transaction_Date { get; set; }
        public decimal Basic_Freight { get; set; }
        public decimal Other_Charges { get; set; }
        public decimal Total_Freight { get; set; }
        public decimal GST { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal BillAmount { get; set; }
        public int Fin_Id { get; set; }
        public int Company_Id { get; set; }
        public string Remarks { get; set; }


        //public int Id { get; set; }
        //public int Bill_Type_Id { get; set; }
        //public int Billing_Office_Id { get; set; }
        //public DateTime Bill_Date { get; set; }
        //public int Bill_No_Id { get; set; }
        //public int Client_Id { get; set; }
        //public int State_Id { get; set; }
        //public int Gst_Type_Id { get; set; }
        //public DateTime Submition_Date { get; set; }
        //public string Reamrk { get; set; }

        //public int Consignee_Id { get; set; }
        //public int Consigner_Id { get; set; }
        //public int Route_Id { get; set; }
        //public int Metarial_Id { get; set; }
        //public int Vehicle_Id { get; set; }
        //public int Date_Range { get; set; }
        //public int GR_Office_Id { get; set; }

        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int Loginid { get; set; }

        public SelectList OfficeLists { get; set; }
        public SelectList StateLists { get; set; }
        public SelectList BillTypeLists { get; set; }
        public SelectList GSTTypeLists { get; set; }
        public SelectList ClientLists { get; set; }
        public SelectList Stationary_List { get; set; }
        public SelectList HSN_SAC_Lists { get; set; }

        public int Origin_Id { get; set; }
        public int Destination_Id { get; set; }
        public int Consignee_Id { get; set; }
        public int Consigner_Id { get; set; }
        public int GRoffice_Id { get; set; }
        public int Item_Id { get; set; }
        public int Vehicle_Id { get; set; }
        public SelectList LocationLists { get; set; }
        public SelectList Item_Lists { get; set; }
        public SelectList Vehicle_Model_List { get; set; }


        public DateTime? Fromdate { get; set; }
        public DateTime? Todate { get; set; }

        public BillCreation()
        { 
           OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
           StateLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("State_Id", "Title", "State_Master", "And IsActive=1"), "Id", "Value");
           BillTypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And IsActive=1 And Menu_Id=50003 And Sub_Type = 2 order by Constant_Id"), "Id", "Value");
           GSTTypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And IsActive=1 And Menu_Id=50003 And Sub_Type = 1 order by Constant_Id"), "Id", "Value");
           ClientLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
           Stationary_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Stationery_ID", "Title", "Stationery_Master", "And IsActive=1 "), "Id", "Value");
           HSN_SAC_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("HSN_SACID", "HSN_SAC", "HSN_SAC_Master", "And IsActive=1"), "Id", "Value");
           LocationLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("location_Id", "Title", "Location_Master", "And IsActive=1"), "Id", "Value");
           Item_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Item_Id", "Title", "Item_Master", "And IsActive=1"), "Id", "Value");
           Vehicle_Model_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Id", "Model_Name", "vehicle_model_master", "And IsActive=1"), "Id", "Value");



            Loginid = CommonUtility.GetLoginID();
        }

        public DataTable Getbillcreationdata(int Client_Id)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Client_Id", Client_Id));
                dt = DBManager.ExecuteDataTableWithParameter("GETBillCReation_Data", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
    }
}