using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IMS.Models.CBL
{
    public class Authenticate
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
        public bool IsAuthenticated = false;
        public SyssoftechSession SyssoftechSession;
        public Authenticate AuthenticateUser(string loginid, string Password, string SessionID)
        {   
            try
            {
                List<SqlParameter> SqlParameters = new List<SqlParameter>();
                SqlParameters.Add(new SqlParameter("@LoginId", loginid));
                SqlParameters.Add(new SqlParameter("@Password", Password));
                SyssoftechSession = new SyssoftechSession(SessionID, DBManager.ExecuteDataTableWithParameter("User_Master_Authentication", CommandType.StoredProcedure, SqlParameters));
                UserName = SyssoftechSession.UserName;
                UserId = SyssoftechSession.UserId;
                UserType = SyssoftechSession.UserType;
                if (Convert.ToInt32(SyssoftechSession.UserId) > 0)
                     IsAuthenticated = true;
                else
                    IsAuthenticated = false;
            }
            catch (Exception ex)
            { throw ex; }
            return this;
        }
    }
}