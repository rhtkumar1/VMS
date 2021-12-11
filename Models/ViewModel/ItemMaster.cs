using IMS.Models.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;
using System.Web.Mvc;

namespace IMS.Models.ViewModel
{
    public class ItemMaster
    {

        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int GroupId { get; set; }
        public int Nature { get; set; }
        public int DeadStockDays { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
        public int ReorderLevel { get; set; }
        public int BaseUnitId { get; set; }
        public int InwardUnitId { get; set; }
        public int OutwardUnitId { get; set; }
        public int HSN_SAC_Id { get; set; }
        public int LocationId { get; set; }
        public int PartyId { get; set; }
        public string HSN_SAC { get; set; }
        public SelectList HSN_SAC_Lists { get; set; }
        public SelectList NatureLists { get; set; }
        public SelectList GroupLists { get; set; }
        public SelectList UnitLists { get; set; }
        public SelectList Unit_In_Lists { get; set; }
        public SelectList Unit_Out_Lists { get; set; }
        public SelectList LocationLists { get; set; }
        public SelectList PartyLists { get; set; }
        public string GroupName { get; set; }
        public string ItemBarCode { get; set; }
        // it would be a xml string for item location and party mapping grid
        // e.i <ItemMaping><listnode Location_Id="1" Party_Id="3"/><listnode Location_Id="2" Party_Id="4"/></ItemMaping>
        public string ItemMapping { get; set; }
        public string ItemMasterValues { get; set; }
        public List<PartyAndLocationMapping> PartyAndLocationMapping { get; set; }
        public List<dynamic> ItemMappingList { get; set; }
        // by default it would be 0 and if make any change in item location and party mapping grid then set it by 1 
        public bool IsMappingChanged { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public int MaxDiscount { get; set; }
        public string DeactivateDate { get; set; }
        public bool Scheme { get; set; }
        public decimal MRP { get; set; }
        public decimal ListPrice { get; set; }

        public ItemMaster()
        {
            GroupLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Group_Id", "Title", "Group_Master", "And IsActive=1"), "Id", "Value");
            UnitLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Unit_Id", "Title", "Unit_Master", "And IsActive=1"), "Id", "Value");
            HSN_SAC_Lists = new SelectList(DDLValueFromDB.GETDATAFROMDB("HSN_SACID", "HSN_SAC", "HSN_SAC_Master", "And IsActive=1"), "Id", "Value");
            LocationLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("location_Id", "Title", "Location_Master", "And IsActive=1"), "Id", "Value");
            PartyLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Party_Id", "Title", "Party_Master", "And IsActive=1"), "Id", "Value");
            GroupLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Group_Id", "Title", "item_group_master", "And IsActive=1"), "Id", "Value");
            NatureLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10010 And IsActive=1"), "Id", "Value");
            Unit_In_Lists = UnitLists;
            Unit_Out_Lists = UnitLists;
            Loginid = CommonUtility.GetLoginID();
            ItemMappingList = new List<dynamic>();
            PartyAndLocationMapping = new List<PartyAndLocationMapping>();
        }

        public ItemMaster ItemMaster_InsertUpdate()
        {
            try
            {
                string sString = string.Empty;
                foreach (var item in PartyAndLocationMapping)
                {
                    sString += @"<listnode Location_Id=""" + Convert.ToString(item.LocationId) + @""" Party_Id=""" + Convert.ToString(item.PartyId) + @"""/>";
                }
                ItemMapping = "<ItemMaping>" + sString + "</ItemMaping>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Item_Id", ItemId));
                SqlParameters.Add(new SqlParameter("@Title", Title));
                SqlParameters.Add(new SqlParameter("@Code", Code));
                SqlParameters.Add(new SqlParameter("@Group_Id", GroupId));
                SqlParameters.Add(new SqlParameter("@Nature", Nature));
                SqlParameters.Add(new SqlParameter("@HSN_SAC", HSN_SAC));
                SqlParameters.Add(new SqlParameter("@DeadStockDays", DeadStockDays));
                SqlParameters.Add(new SqlParameter("@MinLevel", MinLevel));
                SqlParameters.Add(new SqlParameter("@MaxLevel", MaxLevel));
                SqlParameters.Add(new SqlParameter("@ReorderLevel", ReorderLevel));
                SqlParameters.Add(new SqlParameter("@BaseUnitId", BaseUnitId));
                SqlParameters.Add(new SqlParameter("@InwardUnitId", InwardUnitId));
                SqlParameters.Add(new SqlParameter("@OutwardUnitId", OutwardUnitId));
                SqlParameters.Add(new SqlParameter("@ItemMapping", ItemMapping));
                SqlParameters.Add(new SqlParameter("@IsMappingChanged", IsMappingChanged));
                SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                SqlParameters.Add(new SqlParameter("@MaxDiscount", MaxDiscount));
                if(DeactivateDate != "")
                    SqlParameters.Add(new SqlParameter("@DeactivateDate", Convert.ToDateTime(CommonUtility.GetDateDDMMYYYY(DeactivateDate))));
                SqlParameters.Add(new SqlParameter("@Scheme", Scheme));
                SqlParameters.Add(new SqlParameter("@MRP", MRP));
                SqlParameters.Add(new SqlParameter("@ListPrice", ListPrice));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Item_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    ItemId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        public DataTable ItemMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Item_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public DataTable Item_Party_Mapping_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Item_Party_Mapping_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

        public ItemMaster ItemMaster_Delete()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Item_Id", ItemId));
                SqlParameters.Add(new SqlParameter("@Loginid", Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Item_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    ItemId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }

        public ItemMaster ItemMaster_Get_By_Id(int item_Id)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Item_Id", item_Id));
                DataSet ds = DBManager.ExecuteDataSetWithParameter("Item_Master_Getdata_By_Id", CommandType.StoredProcedure, SqlParameters);
                DataTable dtItem = ds.Tables[0];
                DataTable dtItem_Party_Mapping = ds.Tables[1];
                foreach (DataRow item in dtItem_Party_Mapping.AsEnumerable())
                {
                    dynamic dPartyMapping = new ExpandoObject();
                    dPartyMapping.Item_Id = item["Item_Id"].ToString();
                    dPartyMapping.Location_Id = item["Location_Id"].ToString();
                    dPartyMapping.Party_Id = item["Party_Id"].ToString();
                    ItemMappingList.Add(dPartyMapping);
                }
                foreach (DataRow dr in dtItem.Rows)
                {
                    ItemId = Convert.ToInt32(dr["Item_Id"]);
                    Title = dr["Title"].ToString();
                    Code = dr["Code"].ToString();
                    GroupId = Convert.ToInt32(dr["Group_Id"]);
                    Nature = Convert.ToInt32(dr["Nature"]);
                    HSN_SAC = dr["HSN_SAC"].ToString();
                    DeadStockDays = Convert.ToInt32(dr["DeadStockDays"]);
                    MinLevel = Convert.ToInt32(dr["MinLevel"]);
                    MaxLevel = Convert.ToInt32(dr["MaxLevel"]);
                    ReorderLevel = Convert.ToInt32(dr["ReorderLevel"]);
                    BaseUnitId = Convert.ToInt32(dr["BaseUnit_Id"]);
                    InwardUnitId = Convert.ToInt32(dr["InwardUnit_Id"]);
                    OutwardUnitId = Convert.ToInt32(dr["OutwardUnit_Id"]);
                    MaxDiscount=Convert.ToInt32(dr["MaxDiscount"]);
                    object value = dr["DeactivateDate"];
                    if (value != DBNull.Value)
                    {
                        DeactivateDate = Convert.ToString(dr["DeactivateDate"]);
                    }   
                    Scheme=Convert.ToBoolean(dr["Scheme"]);
                    MRP = Convert.ToDecimal(dr["MRP"]); ; 
                    ListPrice = Convert.ToDecimal(dr["ListPrice"]); ;
                    //ItemMapping = dr["ItemMapping"].ToString();
                    //IsMappingChanged = Convert.ToBoolean(dr["IsMappingChanged"]);
                    Remarks = dr["Remarks"].ToString();
                    //Loginid = Convert.ToInt32(dr["LoginId"]);
                }
            }
            catch (Exception ex)
            { throw ex; }

            return this;
        }
    }
    public class PartyAndLocationMapping
    {
        public int PartyId { get; set; }
        public int LocationId { get; set; }
    }
}