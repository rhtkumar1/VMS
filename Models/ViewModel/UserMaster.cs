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
    public class UserMaster
    {
       public int User_Id{ get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
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
        public SelectList RoleLists { get; set; }
        DateTime LastLogin { get; set; }
        public string Remarks { get; set; }
        int IsActive { get; set; }
        int CreatedBy { get; set; }
        int ModifiedBy { get; set; }
        public string AppToken { get; set; }
        public string AuthMode { get; set; }


        private string _LoginId;
        private bool _Islogin;
        public bool Islogin { get { return _Islogin; } }

        public bool IsExists { get; internal set; }
        public dynamic Msg { get; internal set; }

        public UserMaster()
        {
            try
            {
                RoleLists = new SelectList(DDLValueFromDB.GETDATAFROMDB("Role_Id", "Title", "Role_Master", "And IsActive=1"), "Id", "Value");
                CreatedBy = CommonUtility.GetLoginID();
                ModifiedBy = CommonUtility.GetLoginID();
            }
            catch(Exception ex)
            { }
            _LoginId = "0";
            _Islogin = false;
        }     
        public UserMaster UserMaster_InsertUpdate()
        {
            try
            {
                if (ConfirmPassword == Password)
                {
                    List<SqlParameter> SqlParameters = new List<SqlParameter>();
                    SqlParameters.Add(new SqlParameter("@User_Id", User_Id));
                    SqlParameters.Add(new SqlParameter("@UserName", UserName));
                    SqlParameters.Add(new SqlParameter("@PasswordHash", Password));
                    SqlParameters.Add(new SqlParameter("@FirstName", FirstName));
                    SqlParameters.Add(new SqlParameter("@MiddleName", MiddleName));
                    SqlParameters.Add(new SqlParameter("@LastName", LastName));
                    SqlParameters.Add(new SqlParameter("@Mobile", Mobile));
                    SqlParameters.Add(new SqlParameter("@Email", Email));
                    SqlParameters.Add(new SqlParameter("@Gender", Gender));
                    SqlParameters.Add(new SqlParameter("@DOB", Convert.ToDateTime(DOB)));
                    SqlParameters.Add(new SqlParameter("@Address1", Address1));
                    SqlParameters.Add(new SqlParameter("@Address2", Address2));
                    SqlParameters.Add(new SqlParameter("@City", City));
                    SqlParameters.Add(new SqlParameter("@State", State));
                    SqlParameters.Add(new SqlParameter("@Relegion", Relegion));
                    SqlParameters.Add(new SqlParameter("@Pincode", Pincode));
                    SqlParameters.Add(new SqlParameter("@Country", Country));
                    SqlParameters.Add(new SqlParameter("@DOJ", Convert.ToDateTime(DOJ)));
                    SqlParameters.Add(new SqlParameter("@PrimaryRole", PrimaryRole));
                    SqlParameters.Add(new SqlParameter("@Remarks", Remarks));
                    SqlParameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
                    SqlParameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
                    User_Id = Convert.ToInt32(DBManager.ExecuteScalar("User_Master_Insertupdate", CommandType.StoredProcedure, SqlParameters));
                 }
                return this;
            }
            catch (Exception ex)
            { throw ex; }

            
        }
        public void UserMaster_Delete()
        {
             
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@User_Id", User_Id));
                SqlParameters.Add(new SqlParameter("@ModifiedBy", ModifiedBy));
                int UserId = DBManager.ExecuteScalar("User_Master_Delete", CommandType.StoredProcedure, SqlParameters);
                if(User_Id != UserId)
                {
                    throw new Exception("-1");
                }
            }
            catch (Exception ex)
            { throw ex; }

        }
        public DataTable UserMaster_Get()
        {
            DataTable dt = new DataTable();
            try
            {
            
                dt = DBManager.ExecuteDataTable("User_Master_Getdata", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            { throw ex; }

            return dt;
        }

    }
}