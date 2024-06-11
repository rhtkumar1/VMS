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
        public decimal Rate { get; set; }
        public decimal Basic_Freight { get; set; }
        public decimal Loading { get; set; }
        public decimal Unloading { get; set; }
        public decimal Detention { get; set; }
        public decimal Other_Charge { get; set; }
        public decimal Two_Point_Delivery { get; set; }
        public decimal Pickup { get; set; }
        public decimal Local_Delivery { get; set; }
        public decimal Hamali { get; set; }
        public decimal Labour { get; set; }
        public decimal Total_Freight { get; set; }
        public decimal Advance_Amount { get; set; }
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

        public string CSGLine { get; set; }

        public List<ConsignmentLine> ConsignmentLines { get; set; }

        public List<dynamic> ItemMappingList { get; set; }

        public Consignment()
        {
            OfficeId = CommonUtility.GetDefault_OfficeID();
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            Office_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            Stationary_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Stationery_ID", "Title", "Stationery_Master", "And IsActive=1 "), "Id", "Value");
            //GR_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10008 And Sub_Type=1 And IsActive=1"), "Id", "Value");
            GR_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("Id", "Title", "Constant", "And IsActive=1"), "Id", "Value");

            ////GR_List.OrderBy(x => x.Id).TOList();

            //GR_List.OrderBy(x => x.Value);

            //GR_List.Sort(Function() a.Text < b.Text);

            // GR_List = GR_List.Items.OrderBy(item => item.Id).Tolist();
            LocationLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("location_Id", "Title", "Location_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            Driver_List = new SelectList(DDLValueFromDB.GETDATAFROMDB("User_Id", "UserName", "User_Master", "And IsActive =1"), "Id", "Value");
            ConsignmentLines = new List<ConsignmentLine>();
            Loginid = CommonUtility.GetLoginID();
        }

        public Consignment Consignment_InsertUpdate()
        {
            try
            {
                var sb = new System.Text.StringBuilder();
                foreach (var item in ConsignmentLines)
                {
                       sb.AppendLine(@"<listnode Line_Id=""" + Convert.ToString(item.Line_Id) + @""" Item_Id=""" + Convert.ToString(item.Item_Id) + @"""   
                                       GR_Id=""" + Convert.ToString(item.GR_Id) + @""" Actual_Weight=""" + Convert.ToString(item.Actual_Weight) + @""" 
                                       Charge_Weight=""" + Convert.ToString(item.Charge_Weight) + @"""   
                                       Quantity=""" + Convert.ToString(item.Quantity) + @""" Unit_Id=""" + Convert.ToString(item.Unit_Id) + @"""   
                                       Rate_TypeId=""" + Convert.ToString(item.Rate_TypeId) + @""" Rate=""" + Convert.ToString(item.Rate) + @"""  
                                       Basic_Freight=""" + Convert.ToString(Basic_Freight) + @""" />");
                   
                }
                CSGLine = "<Line>" + sb + "</Line>";


                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@GR_Id", GR_Id));
                SqlParameters.Add(new SqlParameter("@GR_No", GR_No));
                SqlParameters.Add(new SqlParameter("@Type_Id", TypeId));
                SqlParameters.Add(new SqlParameter("@GR_OfficeId", GR_OfficeId));           
                SqlParameters.Add(new SqlParameter("@Billing_OfficeId", Billing_OfficeId));
                SqlParameters.Add(new SqlParameter("@GR_Date", GR_Date));
                SqlParameters.Add(new SqlParameter("@Client_Id", Client_Id));
                SqlParameters.Add(new SqlParameter("@Consignee_Id", Consignee_Id));
                SqlParameters.Add(new SqlParameter("@Consigner_Id", Consigner_Id));
                SqlParameters.Add(new SqlParameter("@Origin_Id", Origin_Id));
                SqlParameters.Add(new SqlParameter("@Destination_Id", Destination_Id));
                SqlParameters.Add(new SqlParameter("@Vehicle_Id", Vehicle_Id));
                SqlParameters.Add(new SqlParameter("@Contract_Id", Contract_Id));
                SqlParameters.Add(new SqlParameter("@Total_Basic_Freight", Convert.ToDecimal(Basic_Freight)));
                SqlParameters.Add(new SqlParameter("@Loading", Convert.ToDecimal(Loading)));
                SqlParameters.Add(new SqlParameter("@Unloading", Convert.ToDecimal(Unloading)));
                SqlParameters.Add(new SqlParameter("@Detention", Convert.ToDecimal(Detention)));               
                SqlParameters.Add(new SqlParameter("@Other_Charge", Convert.ToDecimal(Other_Charge)));
                SqlParameters.Add(new SqlParameter("@Two_Point_Delivery", Convert.ToDecimal(Two_Point_Delivery)));
                SqlParameters.Add(new SqlParameter("@Pickup", Convert.ToDecimal(Pickup)));
                SqlParameters.Add(new SqlParameter("@Local_Delivery", Convert.ToDecimal(Local_Delivery)));
                SqlParameters.Add(new SqlParameter("@Hamali", Convert.ToDecimal(Hamali)));     
                SqlParameters.Add(new SqlParameter("@Labour", Convert.ToDecimal(Labour)));
                SqlParameters.Add(new SqlParameter("@Total_Freight", Convert.ToDecimal(Total_Freight)));
                SqlParameters.Add(new SqlParameter("@Advance_Amount", Convert.ToDecimal(Advance_Amount)));
                SqlParameters.Add(new SqlParameter("@Advance_Date", Advance_Date));
                SqlParameters.Add(new SqlParameter("@Advance_LedgerId", Advance_LedgerId));
                SqlParameters.Add(new SqlParameter("@Is_Auto_Bill", Is_Auto_Bill));
                SqlParameters.Add(new SqlParameter("@Is_Auto_Trip", Is_Auto_Trip));
                SqlParameters.Add(new SqlParameter("@Driver_Id", Driver_Id));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@CSGLine", CSGLine));

                DataTable dt = DBManager.ExecuteDataTableWithParameter("Consignment_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    OfficeId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }

            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }


        public DataSet GetConsigmentData_By_Id(int GR_Id)
        {
            DataSet ds = new DataSet();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@GR_Id", GR_Id));
                ds = DBManager.ExecuteDataSetWithParameter("GetConsigmentData_By_Id", CommandType.StoredProcedure, SqlParameters);
            }
            catch (Exception ex)
            { throw ex; }

            return ds;
        }

        //public Consignment GetConsigmentData_By_Id(int GR_Id)
        //{
        //    try
        //    {
        //        List<SqlParameter> SqlParameters = new List<SqlParameter>();
        //        SqlParameters.Add(new SqlParameter("@GR_Id", GR_Id));
        //        DataSet ds = DBManager.ExecuteDataSetWithParameter("GetConsigmentData_By_Id", CommandType.StoredProcedure, SqlParameters);
        //        DataTable dtItem = ds.Tables[0];
        //        DataTable dtItem_Party_Mapping = ds.Tables[1];

        //        //foreach (DataRow item in dtItem_Party_Mapping.AsEnumerable())
        //        //{
        //        //    dynamic dPartyMapping = new ExpandoObject();
        //        //    dPartyMapping.Line_Id = item["Line_Id"].ToString();
        //        //    dPartyMapping.GR_Id = item["GR_Id"].ToString();
        //        //    dPartyMapping.Item_Id = item["Item_Id"].ToString();
        //        //    dPartyMapping.Actual_Weight = item["Actual_Weight"].ToString();
        //        //    dPartyMapping.Charge_Weight = item["Charge_Weight"].ToString();
        //        //    dPartyMapping.Quantity = item["Quantity"].ToString();
        //        //    dPartyMapping.Unit_Id = item["Unit_Id"].ToString();
        //        //    dPartyMapping.Rate_TypeId = item["Rate_TypeId"].ToString();
        //        //    dPartyMapping.Rate = item["Rate"].ToString();
        //        //    dPartyMapping.Basic_Freight = item["Basic_Freight"].ToString();
        //        //    ItemMappingList.Add(dPartyMapping);
        //        //}
        //        foreach (DataRow dr in dtItem.Rows)
        //        {
        //            GR_Id = Convert.ToInt32(dr["GR_Id"]);
        //            GR_No = dr["GR_No"].ToString();
        //            //TypeId = Convert.ToInt32(dr["TypeId"]);
        //            GR_OfficeId = Convert.ToInt32(dr["GR_OfficeId"]);
        //            Billing_OfficeId = Convert.ToInt32(dr["Billing_OfficeId"]);
        //           // GR_Date = Convert.ToDateTime(dr["Billing_OfficeId"]);
        //            Client_Id = Convert.ToInt32(dr["Client_Id"]);
        //            Consignee_Id = Convert.ToInt32(dr["Consignee_Id"]);
        //            Consigner_Id = Convert.ToInt32(dr["Consigner_Id"]);
        //            Origin_Id = Convert.ToInt32(dr["Origin_Id"]);
        //            Destination_Id = Convert.ToInt32(dr["Destination_Id"]);
        //            Vehicle_Id = Convert.ToInt32(dr["Vehicle_Id"]);
        //            Contract_Id = Convert.ToInt32(dr["Contract_Id"]);
        //            Loading = Convert.ToDecimal(dr["Loading"]);
        //            Two_Point_Delivery = Convert.ToDecimal(dr["Two_Point_Delivery"]);
        //            Unloading = Convert.ToDecimal(dr["Unloading"]);
        //            Pickup = Convert.ToDecimal(dr["Pickup"]);
        //            Detention = Convert.ToDecimal(dr["Detention"]);
        //            Local_Delivery = Convert.ToDecimal(dr["Local_Delivery"]);
        //            Other_Charge = Convert.ToDecimal(dr["Other_Charge"]);
        //            Hamali = Convert.ToDecimal(dr["Hamali"]);
        //            Labour = Convert.ToDecimal(dr["Labour"]);
        //            Total_Freight = Convert.ToDecimal(dr["Total_Freight"]);
        //            Advance_Amount = Convert.ToDecimal(dr["Advance_Amount"]);
        //            Driver_Id = Convert.ToInt32(dr["Driver_Id"]);
        //            Advance_LedgerId = Convert.ToInt32(dr["Advance_LedgerId"]);
        //            Is_Auto_Bill = Convert.ToBoolean(dr["Is_Auto_Bill"]);
        //            Is_Auto_Trip = Convert.ToBoolean(dr["Is_Auto_Trip"]);

        //            //GroupId = Convert.ToInt32(dr["Group_Id"]);
        //            //Nature = Convert.ToInt32(dr["Nature"]);
        //            //HSN_SAC = dr["HSN_SAC"].ToString();
        //            //DeadStockDays = Convert.ToInt32(dr["DeadStockDays"]);
        //            //MinLevel = Convert.ToInt32(dr["MinLevel"]);
        //            //MaxLevel = Convert.ToInt32(dr["MaxLevel"]);
        //            //ReorderLevel = Convert.ToInt32(dr["ReorderLevel"]);
        //            //BaseUnitId = Convert.ToInt32(dr["BaseUnit_Id"]);
        //            //InwardUnitId = Convert.ToInt32(dr["InwardUnit_Id"]);
        //            //OutwardUnitId = Convert.ToInt32(dr["OutwardUnit_Id"]);
        //            //MaxDiscount = Convert.ToInt32(dr["MaxDiscount"]);
        //            //object value = dr["DeactivateDate"];
        //            //if (value != DBNull.Value)
        //            //{
        //            //    DeactivateDate = Convert.ToString(dr["DeactivateDate"]);
        //            //}
        //            //Scheme = Convert.ToBoolean(dr["Scheme"]);
        //            //MRP = Convert.ToDecimal(dr["MRP"]);
        //            //ListPrice = Convert.ToDecimal(dr["ListPrice"]); ;
        //            ////ItemMapping = dr["ItemMapping"].ToString();
        //            ////IsMappingChanged = Convert.ToBoolean(dr["IsMappingChanged"]);
        //            //Remarks = dr["Remarks"].ToString();
        //            //OpeningQty = Convert.ToInt32(dr["OpeningQty"]);
        //            //ItemLocationId = Convert.ToInt32(dr["ItemLocationId"]);
        //            //BarCoadID = dr["BarCoadID"].ToString();
        //            //BarCoadQty = dr["BarcodeQty"].ToString();
        //            //BarCoadUnit = dr["BarcodeUnit"].ToString();
        //            //Loginid = Convert.ToInt32(dr["LoginId"]);
        //        }
        //    }
        //    catch (Exception ex)
        //    { throw ex; }

        //    return this;
        //}

        //public DataTable Consignment_Get()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = DBManager.ExecuteDataTable("Consignment_Getdata", CommandType.StoredProcedure);
        //    }
        //    catch (Exception ex)
        //    { throw ex; }

        //    return dt;
        //}
        public DataTable Consignment_Get_BydateFilter(DateTime fromdate, DateTime todate, string GR_No, int? GR_OfficeId, int? Vehicle_Id, int? Billing_OfficeId)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Fromdate", fromdate));
                SqlParameters.Add(new SqlParameter("@Todate", todate));
                SqlParameters.Add(new SqlParameter("@GR_No", GR_No));
                SqlParameters.Add(new SqlParameter("@GR_OfficeId", GR_OfficeId));
                SqlParameters.Add(new SqlParameter("@Vehicle_Id", Vehicle_Id));
                SqlParameters.Add(new SqlParameter("@Billing_OfficeId", Billing_OfficeId));
                dt = DBManager.ExecuteDataTableWithParameter("Consignment_Getdata", CommandType.StoredProcedure, SqlParameters);
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

    public class ConsignmentLine
    {
        public string Line_Id { get; set; }
        public string GR_Id { get; set; }
        public string Item_Id { get; set; }
        public string Actual_Weight { get; set; }
        public string Charge_Weight { get; set; }
        public string Quantity { get; set; }
        public string Unit_Id { get; set; }
        public string Rate_TypeId { get; set; }
        public string Rate { get; set; }
        public string Basic_Freight { get; set; }

    }
}