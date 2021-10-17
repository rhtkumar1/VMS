using IMS.Models.ViewModel;
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
        
        public string UserName;
        public string UserId;
        public string UserType;
        public bool IsAuthenticated = false;
        public SyssoftechSession SyssoftechSession;
        public List<Menu_Master_Display> Menu_List = new List<Menu_Master_Display>();
        public List<Menu_Master_Role_Wise> ObjMenu_Master_Role_Wise;
        public Authenticate AuthenticateUser(string loginid, string Password, string SessionID)
        {   
            try
            {
                SyssoftechSession = new SyssoftechSession(SessionID, new User().Authentication(loginid, Password));
                UserName = SyssoftechSession.UserName;
                UserId = SyssoftechSession.UserId;
                UserType = SyssoftechSession.UserType;
                if (Convert.ToInt32(SyssoftechSession.UserId) > 0)
                {
                    
                    Menu_List =new  Menue_Master().GetMinu(Convert.ToInt32(SyssoftechSession.UserId),out ObjMenu_Master_Role_Wise);
                    IsAuthenticated = true;
                }
                else
                {
                    IsAuthenticated = false;
                }
            }
            catch (Exception ex)
            { throw ex; }
            return this;
        }
    }
}