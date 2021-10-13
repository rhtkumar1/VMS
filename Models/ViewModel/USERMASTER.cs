using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.CBL
{
    public class USERMASTER
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

        public string UserName;
        public string UserId;
        public string UserType;
        public bool IsAuthenticated;
        public ThalesSession ThalesSession;
        public USERMASTER AuthenticateUser(string loginid, string Password, string SessionID)
        {
            DataTable loginAuth = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@LoginId", loginid));
                SqlParameters.Add(new SqlParameter("@Password", Password));
                loginAuth = DBManager.ExecuteDataTableWithParamiter("User_Master_Authentication", CommandType.StoredProcedure, SqlParameters);
                ThalesSession = new ThalesSession(SessionID, loginAuth);
                UserName = ThalesSession.UserName;
                UserId = ThalesSession.UserId;
                UserType = ThalesSession.UserType;
                IsAuthenticated = true;
            }
            catch (Exception ex)
            { throw ex; }
            return this;
        }

        public USERMASTER UserMasterCreation(USERMASTER uSERMASTER)
        {
            DataTable dt = new DataTable();
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@QueryType", Convert.ToInt32(uSERMASTER.UserId) > 0 ? "update" : "insert"));
                SqlParameters.Add(new SqlParameter("@LoginId", uSERMASTER.LoginId));
                SqlParameters.Add(new SqlParameter("@Password", uSERMASTER.Password));
                SqlParameters.Add(new SqlParameter("@UserId", uSERMASTER.UserId));
                SqlParameters.Add(new SqlParameter("@UserType", "2"));
                SqlParameters.Add(new SqlParameter("@FirstName", uSERMASTER.FirstName));
                SqlParameters.Add(new SqlParameter("@MiddleName", uSERMASTER.MiddleName));
                SqlParameters.Add(new SqlParameter("@LastName", uSERMASTER.LastName));
                SqlParameters.Add(new SqlParameter("@MobileNo", uSERMASTER.MobileNo));
                SqlParameters.Add(new SqlParameter("@EmailId", uSERMASTER.EmailId));
                SqlParameters.Add(new SqlParameter("@City", uSERMASTER.City));
                SqlParameters.Add(new SqlParameter("@State", uSERMASTER.State));
                SqlParameters.Add(new SqlParameter("@Country", uSERMASTER.Country));
                SqlParameters.Add(new SqlParameter("@Address", uSERMASTER.Address));
                SqlParameters.Add(new SqlParameter("@PinCode", uSERMASTER.PinCode));
                dt = DBManager.ExecuteDataTableWithParamiter("Proc_Manage_UserMasters", CommandType.StoredProcedure, SqlParameters);
                DataRow dr = dt.Rows[0];
                if (dr["msg"].ToString() != "")
                {
                    uSERMASTER.Msg = dr["msg"].ToString();
                }
            }
            catch (Exception ex)
            { throw ex; }
            
            return uSERMASTER;
        }
    }
}