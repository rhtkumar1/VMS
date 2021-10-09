using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMS.Models.CBL
{
    public class ThalesSession
    {
        private string _UserName;
        private string _UserId;
        private string _UserType;
        private string _SessionID;
        private DateTime _SessionStartDateTime;
       
        public ThalesSession(string sessionID, DataTable loginAuth)
        {
            _UserName = loginAuth.Rows[0].ItemArray[3].ToString();
            _UserId = loginAuth.Rows[0].ItemArray[0].ToString();
            _UserType = loginAuth.Rows[0].ItemArray[2].ToString();
            _SessionID = sessionID;
            _SessionStartDateTime = DateTime.Now;

        }

        public string UserName { get {  return _UserName; } }
        public string UserId { get { return _UserId; } }
        public string UserType { get { return _UserType; } }

        public string  SessionID { get { return _SessionID+";;;"+ _SessionStartDateTime.ToString("YYYYDDMMHHmmssfffffffK"); } }

    }
}