using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.ViewModel
{
    public class CreateUserMaster
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Address { get; set; }
        public bool IsExists { get; set; }
        public string Msg { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserType { get; set; }
        public CreateUserMaster ManageUsers(CreateUserMaster createUser)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@QueryType", Convert.ToInt32(createUser.UserId) > 0 ? "update" : "insert"));
                SqlParameters.Add(new SqlParameter("@LoginId", createUser.LoginId));
                SqlParameters.Add(new SqlParameter("@Password", createUser.Password));
                SqlParameters.Add(new SqlParameter("@UserId", createUser.UserId));
                SqlParameters.Add(new SqlParameter("@UserType", "2"));
                SqlParameters.Add(new SqlParameter("@FirstName", createUser.FirstName));
                SqlParameters.Add(new SqlParameter("@MiddleName", createUser.MiddleName));
                SqlParameters.Add(new SqlParameter("@LastName", createUser.LastName));
                SqlParameters.Add(new SqlParameter("@MobileNo", createUser.MobileNo));
                SqlParameters.Add(new SqlParameter("@EmailId", createUser.EmailId));
                SqlParameters.Add(new SqlParameter("@City", createUser.City));
                SqlParameters.Add(new SqlParameter("@State", createUser.State));
                SqlParameters.Add(new SqlParameter("@Country", createUser.Country));
                SqlParameters.Add(new SqlParameter("@Address", createUser.Address));
                SqlParameters.Add(new SqlParameter("@PinCode", createUser.PinCode));
                dt = DBManager.ExecuteDataTableWithParameter("Proc_Manage_UserMasters", CommandType.StoredProcedure, SqlParameters);
                DataRow dr = dt.Rows[0];
                if (dr["msg"].ToString() != "")
                {
                    createUser.Msg = dr["msg"].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }
            
            return createUser;
        }
    }
}