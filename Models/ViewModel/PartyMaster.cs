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
    public class PartyMaster
    {

        public int PartyId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int OfficeId { get; set; }
        public SelectList OfficeLists { get; set; }
        public int GroupId { get; set; }
        public SelectList GroupLists { get; set; }
        public bool MaintainRef { get; set; }
        public int CreditDays { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal VariableLimit { get; set; }
        public string Prefix { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public SelectList StateLists { get; set; }
        public string Region { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public int Createdby { get; set; }
        public int Loginid { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }
        public string ActionMsg { get; set; }
        public bool IsSucceed { get; set; }
        public SelectList LocationLists { get; set; }
        public SelectList GSTTypeLists { get; set; }
        public SelectList GSTNatureLists { get; set; }
        public string PartyMapping { get; set; }
        public bool IsMappingChanged { get; set; }
        public string CategoryId { get; set; }
        public string OpeningBalance { get; set; }
        public List<PartyAndGstMapping> PartyAndGstMapping { get; set; }
        public List<dynamic> ItemMappingList { get; set; }
        public int LocationId { get; set; }
        public int GSTType { get; set; }
        public int GSTNature { get; set; }
        public int SalesManUserID { get; set; }
        public SelectList SalesManList { get; set; }
        public SelectList CategoryList { get; set; }


        public PartyMaster()
        {
            OfficeId = CommonUtility.GetDefault_OfficeID();
            StateLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("State_Id", "Title", "State_Master", "And IsActive=1"), "Id", "Value");
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            GroupLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Group_Id", "Title", "Group_Master", "And IsActive=1"), "Id", "Value");
            LocationLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Location_Id", "Title", "Location_Master", "And IsActive=1"), "Id", "Value");
            GSTTypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10008 And Sub_Type=1 And IsActive=1"), "Id", "Value");
            GSTNatureLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10006 And Sub_Type=2 And IsActive=1"), "Id", "Value");
            SalesManList = new SelectList(DDLValueFromDB.GETDATAFROMDB("User_Id", "UserName", "User_Master", "And IsActive =1"), "Id", "Value");
            CategoryList = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10008 And Sub_Type=2  And IsActive =1"), "Id", "Value");
            Loginid = CommonUtility.GetLoginID();
            ItemMappingList = new List<dynamic>();
            PartyAndGstMapping = new List<PartyAndGstMapping>();
        }
        
        public PartyMaster PartyMaster_InsertUpdate(PartyMaster partyMaster)
        {
            try
            {
                string sString = string.Empty;
                foreach (var item in PartyAndGstMapping)
                {
                    sString += @"<listnode Location_Id=""" + Convert.ToString(item.Location_Id) + @""" GSTNo=""" + Convert.ToString(item.GSTNo) + @""" GSTType=""" + Convert.ToString(item.GSTType_Id) + @"""  GSTNature=""" + Convert.ToString(item.GSTNature_Id) + @"""/>";
                }
                PartyMapping = "<PartyMaping>" + sString + "</PartyMaping>";

                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", partyMaster.PartyId));
                SqlParameters.Add(new SqlParameter("@Title", partyMaster.Title));
                SqlParameters.Add(new SqlParameter("@Code", partyMaster.Code));
                SqlParameters.Add(new SqlParameter("@Office_Id", partyMaster.OfficeId));
                SqlParameters.Add(new SqlParameter("@Group_Id", partyMaster.GroupId));
                SqlParameters.Add(new SqlParameter("@Maintain_Ref", partyMaster.MaintainRef));
                SqlParameters.Add(new SqlParameter("@CreditDays", partyMaster.CreditDays));
                SqlParameters.Add(new SqlParameter("@CreditLimit", partyMaster.CreditLimit));
                SqlParameters.Add(new SqlParameter("@VariableLimit", partyMaster.VariableLimit));
                SqlParameters.Add(new SqlParameter("@FirstName", partyMaster.FirstName));
                SqlParameters.Add(new SqlParameter("@MiddleName", partyMaster.MiddleName));
                SqlParameters.Add(new SqlParameter("@LastName", partyMaster.LastName));
                SqlParameters.Add(new SqlParameter("@Mobile", partyMaster.Mobile));
                SqlParameters.Add(new SqlParameter("@Email", partyMaster.Email));
                SqlParameters.Add(new SqlParameter("@Gender", partyMaster.Gender));
                SqlParameters.Add(new SqlParameter("@Address1", partyMaster.Address1));
                SqlParameters.Add(new SqlParameter("@Address2", partyMaster.Address2));
                SqlParameters.Add(new SqlParameter("@City", partyMaster.City));
                SqlParameters.Add(new SqlParameter("@State", partyMaster.State));
                SqlParameters.Add(new SqlParameter("@Zip", partyMaster.Zip));
                SqlParameters.Add(new SqlParameter("@Country", partyMaster.Country));
                SqlParameters.Add(new SqlParameter("@PartyMapping", PartyMapping));
                SqlParameters.Add(new SqlParameter("@IsMappingChanged", IsMappingChanged));
                SqlParameters.Add(new SqlParameter("@Category_Id", CategoryId));
                SqlParameters.Add(new SqlParameter("@OpeningBalance", OpeningBalance));
                SqlParameters.Add(new SqlParameter("@Remarks", partyMaster.Remarks));
                SqlParameters.Add(new SqlParameter("@Loginid", partyMaster.Loginid));
                SqlParameters.Add(new SqlParameter("@SalesManUserID", partyMaster.SalesManUserID));
                
                
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Party_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    PartyId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return partyMaster;
        }


        public DataTable PartyMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = DBManager.ExecuteDataTable("Party_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }
        public PartyMaster GetPartyById(int partyId)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Clear();
                SqlParameters.Add(new SqlParameter("@Party_Id", partyId));
                DataSet ds = DBManager.ExecuteDataSetWithParameter("Party_Master_Get_By_Id", CommandType.StoredProcedure, SqlParameters);
                DataRow drParty = ds.Tables[0].Rows[0];
                DataTable dtItem_Party_Gst_Mapping = ds.Tables[1];
                foreach (DataRow item in dtItem_Party_Gst_Mapping.AsEnumerable())
                {
                    dynamic dGstMapping = new ExpandoObject();
                    dGstMapping.Party_Id = Convert.ToInt32(item["Party_Id"].ToString());
                    dGstMapping.Location_Id = Convert.ToInt32(item["Location_Id"].ToString());
                    dGstMapping.GSTNo = item["GSTNo"].ToString();
                    dGstMapping.Location = item["Location"].ToString();
                    dGstMapping.GSTType = item["GSTType"].ToString();
                    dGstMapping.GSTNature = item["GSTNature"].ToString();
                    dGstMapping.GSTType_Id = item["GSTType_Id"].ToString();
                    dGstMapping.GSTNature_Id = item["GSTNature_Id"].ToString();
                    ItemMappingList.Add(dGstMapping);
                }

                PartyId = Convert.ToInt32(drParty["Party_Id"]);
                Title = Convert.ToString(drParty["Title"]);
                Code = Convert.ToString(drParty["Code"]);
                OfficeId = Convert.ToInt32(drParty["Office_Id"]);
                GroupId = Convert.ToInt32(drParty["Group_Id"]);
                MaintainRef = Convert.ToBoolean(drParty["Maintain_Ref"]);
                CreditDays = Convert.ToInt32(drParty["CreditDays"]);
                CreditLimit = Convert.ToInt32(drParty["CreditLimit"]);
                VariableLimit = Convert.ToDecimal(drParty["VariableLimit"]);
                FirstName = Convert.ToString(drParty["FirstName"]);
                MiddleName = Convert.ToString(drParty["MiddleName"]);
                LastName = Convert.ToString(drParty["LastName"]);
                Mobile = Convert.ToString(drParty["Mobile"]);
                Email = Convert.ToString(drParty["Email"]);
                Gender = Convert.ToString(drParty["Gender"]);
                Address1 = Convert.ToString(drParty["Address1"]);
                Address2 = Convert.ToString(drParty["Address2"]);
                City = Convert.ToString(drParty["City"]);
                State = Convert.ToString(drParty["State"]);
                Zip = Convert.ToString(drParty["Zip"]);
                Country = Convert.ToString(drParty["Country"]);
                CategoryId = Convert.ToString(drParty["Category_Id"]);
                OpeningBalance = Convert.ToString(drParty["OpeningBalance"]);
                Remarks = Convert.ToString(drParty["Remarks"]);
                SalesManUserID = Convert.ToInt32(drParty["SalesManUserID"]);
                
                return this;
            }
            catch (Exception ex)
            { throw ex; }
        }

        public PartyMaster PartyMaster_Delete(PartyMaster partyMaster)
        {

            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Party_Id", partyMaster.PartyId));
                SqlParameters.Add(new SqlParameter("@Loginid", partyMaster.Loginid));
                DataTable dt = DBManager.ExecuteDataTableWithParameter("Party_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                foreach (DataRow dr in dt.Rows)
                {
                    PartyId = Convert.ToInt32(dr[0]);
                    IsSucceed = Convert.ToBoolean(dr[1]);
                    ActionMsg = dr[2].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }

            return partyMaster;
        }
    }
    public class PartyAndGstMapping
    {
        public int GSTType_Id { get; set; }
        public int Location_Id { get; set; }
        public int GSTNature_Id { get; set; }
        public string GSTNo { get; set; }
    }
}