using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.ViewModel
{
    public class UserMaster
    {
       public int User_Id{ get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Relegion { get; set; }
        public string Pincode { get; set; }
        public string Country { get; set; }
        public DateTime DOJ { get; set; }
        public int PrimaryRole { get; set; }
        DateTime LastLogin { get; set; }
        public string Remarks { get; set; }
        int IsActive { get; set; }
        int CreatedBy { get; set; }
        int ModifiedBy { get; set; }

        private string _LoginId;
        private bool _Islogin;
        public bool Islogin { get { return _Islogin; } }

        public bool IsExists { get; internal set; }
        public dynamic Msg { get; internal set; }

        public UserMaster()
        {
            CreatedBy = CommonUtility.GetLoginID();
            ModifiedBy = CommonUtility.GetLoginID();
            _LoginId = "0";
            _Islogin = false;
        }     

        internal UserMaster ManageUsers(UserMaster createUser)
        {
            throw new NotImplementedException();
        }
        public UserMaster UserMaster_InsertUpdate()
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@Financial_id", User_Id));
                SqlParameters.Add(new SqlParameter("@From_date", Convert.ToDateTime(DOB)));
                SqlParameters.Add(new SqlParameter("@To_date", Convert.ToDateTime(DOJ)));
                SqlParameters.Add(new SqlParameter("@Loginid", DOJ));
                User_Id =Convert.ToInt32(DBManager.ExecuteScalar("User_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters));
                return this;
            }
            catch (Exception ex)
            { throw ex; }

            
        }
        public DataSet Authentication(string LoginID, string Password)
        {
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@LoginId", LoginID));
                SqlParameters.Add(new SqlParameter("@Password", Password));
                return DBManager.ExecuteDataSetWithParameter("User_Master_Authentication", System.Data.CommandType.StoredProcedure, SqlParameters);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}