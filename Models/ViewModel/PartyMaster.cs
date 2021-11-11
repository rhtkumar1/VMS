using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using IMS.Models.CommonModel;

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

        public PartyMaster()
        {
            StateLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("State_Id", "Title", "State_Master", "And IsActive=1"), "Id", "Value");
            OfficeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("office_Id", "Title", "Office_Master", "And IsActive=1"), "Id", "Value");
            GroupLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Group_Id", "Title", "Group_Master", "And IsActive=1"), "Id", "Value");
            LocationLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Location_Id", "Title", "Location_Master", "And IsActive=1"), "Id", "Value");
            GSTTypeLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10008 And Sub_Type=1 And IsActive=1"), "Id", "Value");
            GSTNatureLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Constant_Id", "Constant_Value", "Constant_Values", "And Menu_Id=10006 And Sub_Type=2 And IsActive=1"), "Id", "Value");

            Loginid = CommonUtility.GetLoginID();
        }

        public PartyMaster PartyMaster_InsertUpdate(PartyMaster partyMaster)
        {
            try
            {
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
}