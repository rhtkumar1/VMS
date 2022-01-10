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
        private int _CompanyID;
        private int _Default_OfficeId;
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
                    object OfficeId = Dr["Default_OfficeId"];
                    if (OfficeId != DBNull.Value)
                    {
                        _Default_OfficeId = Convert.ToInt32(OfficeId);
                    }
                    else
                    {
                        _Default_OfficeId = 1;
                    }
                    object PrimaryRole = Dr["PrimaryRole"];
                    if (PrimaryRole != DBNull.Value)
                    {
                        _UserType = Convert.ToString(PrimaryRole);
                    }
                    else
                    {
                        _UserType = "0";
                    }
                    object CompanyId = Dr["CompanyId"];
                    if (CompanyId != DBNull.Value)
                    {
                        _CompanyID = Convert.ToInt32(CompanyId);
                    }
                    else
                    {
                        _CompanyID = 0;
                    }
                    


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
        public int CompanyID { get { return _CompanyID; } }
        public int Default_OfficeId { get { return _Default_OfficeId; } }
        public string SessionID { get { return _SessionID; } }

    }
    
}