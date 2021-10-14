using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMS.Models.CBL
{
    public class SyssoftechSession
    {
        private string _UserName;
        private string _UserId;
        private string _UserType;
        private string _SessionID;
        private DateTime _SessionStartDateTime;
       
        public SyssoftechSession(string sessionID, DataTable loginAuth)
        {
            foreach (DataRow Dr in loginAuth.Rows)
            {
                _UserName = Dr["UserName"].ToString();
                _UserId = Dr["User_Id"].ToString();
                _UserType = "0";
                _SessionID = sessionID;
                _SessionStartDateTime = DateTime.Now;
            }

        }

        public string UserName { get {  return _UserName; } }
        public string UserId { get { return _UserId; } }
        public string UserType { get { return _UserType; } }

        public string  SessionID { get { return _SessionID; } }

    }
}