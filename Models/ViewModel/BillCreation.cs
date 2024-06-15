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
        public string Bill_Date { get; set; }
        public int Type_Id { get; set; }
        public int Billing_OfficeId { get; set; }
        public int Client_Id { get; set; }
        public int SupplyState_Id { get; set; }
        public int GST_TypeId { get; set; }
        public int HSN_SAC { get; set; }
        public string Transaction_Date { get; set; }
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

        public int MENU_Id { get; set; }
        public string SaleLine { get; set; }

        public List<GoodRecieptBillLine> GoodRecieptBillLineList { get; set; }
        public List<GoodRecieptBillLine> GoodRecieptBillLines { get; set; }


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
            MENU_Id = CommonUtility.GetActiveMenuID();
            Fin_Id = CommonUtility.GetFYID();
            Company_Id = CommonUtility.GetCompanyID();
        }

        public DataSet Getbillcreationdata(int Client_Id, int HSN,int OfficeId)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Client_Id", Client_Id));
                SqlParameters.Add(new SqlParameter("@Hsn_id", HSN));
                SqlParameters.Add(new SqlParameter("@OfficeId", OfficeId));
                ds = DBManager.ExecuteDataSetWithParameter("GETBillCReation_Data", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return ds;
        }

        public BillCreation BillCreation_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                //foreach (var item in GoodRecieptBillLineList)
                //{
                    //sb.AppendLine(@"<listnode Line_Id=""" + Convert.ToString(item.Line_Id) + @""" Item_Id=""" + Convert.ToString(item.Item_Id) + @"""   
                    //                PO_Id=""" + Convert.ToString(item.PO_Id) + @""" POLine_Id=""" + Convert.ToString(item.POLine_Id) + @"""
                    //                Quantity=""" + Convert.ToString(item.Quantity) + @""" HSN_SAC=""" + Convert.ToString(item.HSN_SAC) + @"""   
                    //                Rate=""" + Convert.ToString(item.Rate) + @""" Amount=""" + Convert.ToString(item.Amount) + @"""   
                    //                Discount_1=""" + Convert.ToString(item.Discount_1) + @""" Discount_2=""" + Convert.ToString(item.Discount_2) + @"""  
                    //                IsUpdate=""" + Convert.ToString(IsUpdateMaterialSales) + @""" GST=""" + Convert.ToString(item.GST) + @"""   
                    //                CGST=""" + Convert.ToString(item.CGST) + @""" SGST=""" + Convert.ToString(item.SGST) + @"""   
                    //                IGST=""" + Convert.ToString(item.IGST) + @""" Total_Amount=""" + Convert.ToString(item.Total_Amount) + @"""  
                    //                UnitId=""" + Convert.ToString(item.Unit_Id) + @""" Discount_1_Amount=""" + Convert.ToString(item.Discount_1_Amount) + @"""
                    //                Discount_2_Amount=""" + Convert.ToString(item.Discount_2_Amount) + @""" Taxable_Amount=""" + Convert.ToString(item.Taxable_Amount) + @""" 
                    //                LastPurchaseRate=""" + Convert.ToString(item.LastRate) + @""" />");
               // }
                SaleLine = "<Line>" + sb + "</Line>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Bill_Id", Bill_Id));
                SqlParameters.Add(new SqlParameter("@Bill_No", Bill_No));
                // SqlParameters.Add(new SqlParameter("@Bill_Date", Bill_Date));
                if (!string.IsNullOrEmpty(Bill_Date))
                    SqlParameters.Add(new SqlParameter("@Bill_Date", Convert.ToDateTime(CommonUtility.GetDateYYYYMMDD(Bill_Date))));
                SqlParameters.Add(new SqlParameter("@Type_Id", Type_Id));
                SqlParameters.Add(new SqlParameter("@Billing_OfficeId", Billing_OfficeId));
                SqlParameters.Add(new SqlParameter("@Client_Id", Client_Id));
                SqlParameters.Add(new SqlParameter("@SupplyState_Id", SupplyState_Id));
                SqlParameters.Add(new SqlParameter("@GST_TypeId", GST_TypeId));
                SqlParameters.Add(new SqlParameter("@HSN_SAC", HSN_SAC));
                //SqlParameters.Add(new SqlParameter("@Transaction_Date", Transaction_Date));
                SqlParameters.Add(new SqlParameter("@Basic_Freight", Basic_Freight));
                SqlParameters.Add(new SqlParameter("@Other_Charges", Other_Charges));
                SqlParameters.Add(new SqlParameter("@Total_Freight", Total_Freight));
                SqlParameters.Add(new SqlParameter("@GST", GST));
                SqlParameters.Add(new SqlParameter("@CGST", CGST));
                SqlParameters.Add(new SqlParameter("@SGST", SGST));
                SqlParameters.Add(new SqlParameter("@IGST", IGST));
                SqlParameters.Add(new SqlParameter("@BillAmount", BillAmount));
                if (!string.IsNullOrEmpty(Transaction_Date))
                    SqlParameters.Add(new SqlParameter("Transaction_Date", Convert.ToDateTime(CommonUtility.GetDateYYYYMMDD(Transaction_Date))));
                //SqlParameters.Add(new SqlParameter("@Fin_Id", Fin_Id));
                SqlParameters.Add(new SqlParameter("@Fin_Id", 5));
                SqlParameters.Add(new SqlParameter("@Company_Id", Company_Id));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Sale_Line", SaleLine));
                SqlParameters.Add(new SqlParameter("@LoginId", Loginid));
                SqlParameters.Add(new SqlParameter("@MENU_Id", MENU_Id));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("BillCreation_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    Bill_Id = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }


        public class GoodRecieptBillLine
        {
            public string Line_Id { get; set; }
            public string Bill_Id { get; set; }
            public string GST { get; set; }
            public string CGST { get; set; }
            public string SGST { get; set; }
            public string IGST { get; set; }
            public string Remarks { get; set; }

        }
    }
}