using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IMS.Models
{
    public static class EncryptDecrypt
    {
        private static string KeyVal = "SYS#@12#";
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        private static string OutPut(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private static string Input(string stringToEncrypt, string SEncryptionKey)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string EncryptString(string Value)
        {           
            return Input(Value, KeyVal);
        }
        public static string DecryptString(string Value)
        {   
            return OutPut(Value, KeyVal);
        }
    }
    public static class URLEncryption
    {
        static string EncryptionKey = "SYSSOFTECH201284";
        public static string Encrypt(string InVal)
        {
            
            byte[] clearBytes = Encoding.Unicode.GetBytes(InVal);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes objRfc2898DeriveBytes = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = objRfc2898DeriveBytes.GetBytes(32);
                encryptor.IV = objRfc2898DeriveBytes.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    InVal = Convert.ToBase64String(ms.ToArray());
                }
            }
            return HttpUtility.UrlEncode(InVal);
        }
        public static string Decrypt(string OutVal)
        {
            OutVal = HttpUtility.UrlDecode(OutVal).Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(OutVal);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes objRfc2898DeriveBytes = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = objRfc2898DeriveBytes.GetBytes(32);
                encryptor.IV = objRfc2898DeriveBytes.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    OutVal = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return OutVal;
        }
    }
}