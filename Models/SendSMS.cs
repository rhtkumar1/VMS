using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Pinnacle.Models
{
    public class SendSMS
    {
        public void SendMessage(string mobileNo, string Msg)
        {
            try
            {

                String varUserName = ConfigurationSettings.AppSettings["smsUserName"].ToString();
                String varPWD = ConfigurationSettings.AppSettings["smsPwd"].ToString();
                String varSenderID = ConfigurationSettings.AppSettings["smsSenderId"].ToString();              
                String varPhNo = mobileNo;
                String varMSG = Msg;                
                string sURL;
                sURL = ConfigurationSettings.AppSettings["smsUrl"].ToString() + varUserName + "&password=" + varPWD + "&sender=" + varSenderID + "&sendto=" + varPhNo + "&message=" + varMSG;
                using (WebClient client = new WebClient())
                {
                    string s = client.DownloadString(sURL);
                }
            }
            catch (Exception ex)
            { }
        }
        public bool SendMessage_ServiceRequestFormate(string MobileNumber, Dictionary<string, string> MsgDictionary)
        {
            try
            {
                string varSMSServiceRequestFormate = ConfigurationSettings.AppSettings["SMSServiceRequestFormate"].ToString();
                SendMessage(MobileNumber, FormateStringMsg(MsgDictionary,varSMSServiceRequestFormate));
                return true;
            }
            catch (Exception ex)
            { return false; }
        }
        private String FormateStringMsg(Dictionary<string, string> MsgDictionary, string MsgFormate)
        {
            foreach (string MKey in MsgDictionary.Keys)
            {
                try
                {
                    MsgFormate = MsgFormate.Replace(("#" + MKey + "#"), MsgDictionary[MKey].ToString());
                }
                catch { }
            }
            return MsgFormate;
        }


    }
}