using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;

namespace ShoppingEcommerce.Web
{
    public class SendEmail
    {
        static Thread SendMail_Thread;

        public static void StartThreadSendMail()
        {
            SendMail_Thread = new Thread(new ThreadStart(SendMailbyThread));
            SendMail_Thread.Start();
        }

        public class EmailQueue
        {
            public string Email { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public bool is_finished { get; set; }

            public EmailQueue()
            {
                Email = "";
                Subject = "";
                Body = "";
                is_finished = false;
            }
        }

        public static bool SendMail(string emailto, string subject, string body, bool InsertToQueue = false)
        {
            if (InsertToQueue)
            {
                EmailQueue emailQueue = new EmailQueue();
                emailQueue.Email = emailto;
                emailQueue.Subject = subject;
                emailQueue.Body = body;
                EmailQueueList.Add(emailQueue);
                return true;
            }
            else
            {
                string displayname = AppSettings.DisplayName;
                if (displayname == null) { displayname = ResoureExtensions.Lang("SurePortalDisPlayName"); }

                string email_account = AppSettings.Email;

                string email_admin = AppSettings.EmailRecive;
                if (emailto == "")
                    emailto = email_admin;

                string password_account = AppSettings.Password;

                string host = AppSettings.Host;

                int port = int.Parse(AppSettings.Port);

                bool enablessl = Convert.ToBoolean(AppSettings.EnalbleSSL);
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential(email_account, password_account);
                SmtpServer.Port = port;
                //SmtpServer.Host = host;
                SmtpServer.EnableSsl = enablessl;
                MailMessage mail = new MailMessage();

                try
                {
                    mail.From = new MailAddress(email_account, displayname, System.Text.Encoding.UTF8);
                    mail.To.Add(emailto);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    //mail.ReplyTo = new MailAddress(email_account);
                    mail.Priority = MailPriority.High;
                    mail.IsBodyHtml = true;
                    SmtpServer.Send(mail);
                    return true;
                }
                catch (Exception) { return false; }
            }
        }


        private static bool ValidateServerCertificate(object sender,
            X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            else
            {
                return true;
            }
        }

        public static bool SendMail(string emailto, string subject, string body)
        {
            string displayname = AppSettings.DisplayName;
            if (displayname == null) { displayname = ResoureExtensions.Lang("SurePortalDisPlayName"); }

            string email_account = AppSettings.Email;

            string email_admin = AppSettings.EmailRecive;
            if (emailto == "")
                emailto = email_admin;

            string password_account = AppSettings.Password;

            string host = AppSettings.Host;

            int port = int.Parse(AppSettings.Port);
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            bool enablessl = Convert.ToBoolean(AppSettings.EnalbleSSL);
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(emailto);
            message.Subject = subject;
            message.From = new MailAddress(email_account, displayname, System.Text.Encoding.UTF8);
            message.Body = body;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;
            SmtpClient smtp = new SmtpClient(AppSettings.SmtpClient);
            smtp.Credentials = new System.Net.NetworkCredential(email_account, password_account);
            smtp.EnableSsl = AppSettings.EnalbleSSL == "true";
            //smtp.UseDefaultCredentials = false;
            smtp.Port = port;
            smtp.Timeout = 100000;
            try
            {
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<EmailQueue> EmailQueueList = new List<EmailQueue>();
        static void SendMailbyThread()
        {
            while (SendMail_Thread.IsAlive)
            {
                while (EmailQueueList.Count > 0)
                {
                    try
                    {
                        var item = EmailQueueList.Where(m => !m.is_finished).FirstOrDefault();
                        if (item == null)
                            break;

                        item.is_finished = true;
                        SendMail(item.Email, item.Subject, item.Body, false);
                        EmailQueueList.Remove(item);
                        Thread.Sleep(2500);
                    }
                    catch (Exception)// ex)
                    {

                    }
                }
                Thread.Sleep(2500);
            }

        }

        /* while (SendMail_Thread.IsAlive)
         {

             for (int i = 0; i < EmailQueueList.Count; i++)
             {
                 try
                 {
                     if (!EmailQueueList[i].is_finished)
                     {
                         SendMail(EmailQueueList[i].Email, EmailQueueList[i].Subject, EmailQueueList[i].Body, false);
                         EmailQueueList[i].is_finished = true;
                         Thread.Sleep(3000);
                     }
                 }
                 catch (Exception ex)
                 {

                 }
             }
             Thread.Sleep(10000);
         }
        
     }*/

        public static bool SendMail(string displayName, string subject, string body, string receiver)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            SmtpClient SmtpServer = new SmtpClient();
            //SmtpServer.Credentials = new System.Net.NetworkCredential(email_account, password_account);
            MailMessage mail = new MailMessage();

            try
            {
                //mail.From = new MailAddress("moss@lacviet.com.vn");
                mail.To.Add(receiver);
                mail.Subject = subject;
                mail.Body = body;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mail.Priority = MailPriority.High;
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                Thread.Sleep(100);
                return true;
            }
            catch (Exception ex)
            {
                Services.LoggingService.Write(ex.ToString());
                return false;
            }
        }
    }
}