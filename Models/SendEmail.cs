using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Pinnacle.Models
{
    public class SendEmail
    {
        private string senderEmail;
        private string password;
        private string host;
        private int port;
        private bool useDefaultCredentials;
        private bool enableSsl;

        public string Error;

        public SendEmail(string msg, string receiverEmail, string subject )
        {
            try
            {
                senderEmail = ConfigurationSettings.AppSettings["senderEmail"].ToString();
                password = ConfigurationSettings.AppSettings["senderEmailPassword"].ToString();
                host = ConfigurationSettings.AppSettings["host"].ToString();
                port = Convert.ToInt32(ConfigurationSettings.AppSettings["port"]);
                useDefaultCredentials = Convert.ToBoolean(ConfigurationSettings.AppSettings["defaultCredentials"]);
                enableSsl = Convert.ToBoolean(ConfigurationSettings.AppSettings["enableSsl"]);
                Error = "";
                System.Net.Mail.MailMessage MyMailMessage = new MailMessage();
                MyMailMessage.From = new MailAddress(senderEmail);
                MyMailMessage.To.Add(receiverEmail);
                MyMailMessage.Subject = subject;
                MyMailMessage.IsBodyHtml = true;
                string tempBody = msg;
                MyMailMessage.Body = tempBody;
                SmtpClient SMTPServer = new SmtpClient(host);
                SMTPServer.Port = port;
                SMTPServer.TargetName = host;
                SMTPServer.Credentials = new NetworkCredential(senderEmail, password);
                SMTPServer.EnableSsl = false;
                SMTPServer.Send(MyMailMessage);
            }
            catch (Exception ex)
            {
                Error = "Error At Object Creation " +ex.Message;
            }
        }
        public bool SendOTP(string ReceiverEmail,string OTPValue)
        {
            
            try
            {
                Error = "";
                MailMessage msg = new MailMessage();
                msg.Subject = "Pinnacle OTP Validation";
                msg.From = new MailAddress(senderEmail);
                msg.Body = "Use " + OTPValue + " as OTP at Pinnacle";
                msg.To.Add(new MailAddress(ReceiverEmail));
                return SendEmaiWithNetwork(msg);
            }
            catch (Exception ex)
            {
                Error = "Error At Email Creation " + ex.Message;
                return false;
            }
        }
        private bool SendEmaiWithNetwork(MailMessage MSG)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;
                smtp.Port = port;
                smtp.UseDefaultCredentials = useDefaultCredentials;
                smtp.EnableSsl = enableSsl;
                NetworkCredential nc = new NetworkCredential(senderEmail, password);
                smtp.Credentials = nc;
                smtp.Send(MSG);
                return true;
            }
            catch(Exception Ex)
            {
                Error = "Error At Email Send " + Ex.Message;
                return false;
                
            }
        }
    }
}