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
        //private List<SysSoftechMenuAuthentication> _MenuAuthenticationList = new List<SysSoftechMenuAuthentication>();
        public SyssoftechSession(string sessionID, DataSet loginAuth)
        {
            if (loginAuth.Tables.Count > 1)
            {
                foreach (DataRow Dr in loginAuth.Tables[0].Rows)
                {
                    _UserName = Dr["UserName"].ToString();
                    _UserId = Dr["User_Id"].ToString();
                    _UserType = "0";
                    _SessionID = sessionID;
                    _SessionStartDateTime = DateTime.Now;
                }
                //foreach (DataRow Dr in loginAuth.Tables[1].Rows)
                //{
                //   // SysSoftechMenuAuthentication ObjT = new SysSoftechMenuAuthentication(Convert.ToInt32(Dr["MenuID"]), Convert.ToInt32(Dr["Auth"]));
                //   //_MenuAuthenticationList.Add(ObjT);
                //}
            }
        }

        public string UserName { get { return _UserName; } }
        public string UserId { get { return _UserId; } }
        public string UserType { get { return _UserType; } }

        public string SessionID { get { return _SessionID; } }

    }
    
}