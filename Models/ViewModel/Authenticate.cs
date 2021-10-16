using LiabilitiesManagementSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
enum UserAuthentication { Select,Insert,Update,Delete }
namespace IMS.Models.CBL
{
    
    public class Authenticate
    {
        
        public string UserName;
        public string UserId;
        public string UserType;
        public bool IsAuthenticated = false;
        public SyssoftechSession SyssoftechSession;
        
        public Authenticate AuthenticateUser(string loginid, string Password, string SessionID)
        {   
            try
            {
                SyssoftechSession = new SyssoftechSession(SessionID, new User().Authentication(loginid, Password));
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