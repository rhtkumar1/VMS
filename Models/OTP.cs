using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OnlineServiceProvider.Models
{
    public class OTP
    {
        private string OTPValue;
        private string _OTPKey;
        private string _OTPID;

        public string OTPKey { get { return _OTPKey; } }
        public string OTPID { get { return _OTPID; } }

        public OTP()
        {
            OTPValue = GenerateOTP();
        }

        public string GetOTP(string RefId,int RefType=0)
        {
            if (RefId == "")
            {
                return "";
            }
            if(RefId.Length != 10)
            {
                return "";
            }
            List<SqlParameter> Parameters = new List<SqlParameter>();
            Parameters.Add(new SqlParameter("@RefId", RefId));
            Parameters.Add(new SqlParameter("@RefType", RefType.ToString()));
            Parameters.Add(new SqlParameter("@Value", OTPValue));
            DataTable DT = DBManager.ExecuteDataTableWithParamiter("Proc_OTPManager_Save", CommandType.StoredProcedure, Parameters);
            return DT.Rows[0]["Value"].ToString(); 
        }
        public bool ValidateOTP(string RefId, string Value, int RefType = 0)
        {
            try
            {
                List<SqlParameter> Parameters = new List<SqlParameter>();
                Parameters.Add(new SqlParameter("@RefId", RefId));
                Parameters.Add(new SqlParameter("@RefType", RefType.ToString()));
                Parameters.Add(new SqlParameter("@Value", Value));
                DataTable DT = DBManager.ExecuteDataTableWithParamiter("Proc_OTPManager_Verify", CommandType.StoredProcedure, Parameters);
                if (DT.Rows.Count > 0)
                {
                    _OTPID = DT.Rows[0]["OTPId"].ToString();
                    _OTPKey = Encryption.Encrypt((_OTPID + DT.Rows[0]["Value"].ToString()));    
                    return true;
                }
                else
                {
                    _OTPKey = "";
                    _OTPID = "";
                    return false;
                }
            }
            catch { return false; }           

        }
        private string GenerateOTP()
        {
            string OTP = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                OTP += r.Next(9).ToString();
            }
            return OTP;
        }
        public bool GenerateKey(string OTPID)
        {
            try
            {
                List<SqlParameter> Parameters = new List<SqlParameter>();
                Parameters.Add(new SqlParameter("@OTPID", OTPID));
                DataTable DT = DBManager.ExecuteDataTableWithParamiter("Proc_OTPManager_Select", CommandType.StoredProcedure, Parameters);
                if (DT.Rows.Count > 0)
                {
                    _OTPID = DT.Rows[0]["OTPId"].ToString();
                    _OTPKey = Encryption.Encrypt((_OTPID + DT.Rows[0]["Value"].ToString()));
                    return true;
                }
                else
                {
                    _OTPKey = "";
                    _OTPID = "";
                    return false;
                }
            }
            catch { return false; }
            
        }
    }
}