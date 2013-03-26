using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;

namespace EFQMWeb.Common.Util
{
    public class EmailUtil
    {
        private SmtpClient smtp = null;

        public EmailUtil()
        {
            smtp = new SmtpClient();
        }

        public EmailUtil(bool enableSSL)
        {
            smtp = new SmtpClient();
            smtp.EnableSsl = enableSSL;
        }

        public int SendHtmlEmail(string content, string subject, string from, string to, string cc, string bcc)
        {
            //-----
            if (System.Configuration.ConfigurationManager.AppSettings.Get("UseSMTPSSL") == "1")
                smtp.EnableSsl = true;

            if (System.Configuration.ConfigurationManager.AppSettings.Get("UseEmail") != "1") return 1;
            MailMessage message = new MailMessage(from, to, subject, content);
            if (cc != null)
            {
                message.CC.Add(cc);
            }
            if (bcc != null)
            {
                //throw new Exception(bcc);
                message.Bcc.Add(bcc);
            }

            message.IsBodyHtml = true;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(content, System.Text.Encoding.UTF8, "text/html");
            string logo = System.Configuration.ConfigurationManager.AppSettings.Get("ImageLogo");
            if ((logo != null) && (logo != "") && (File.Exists(HttpContext.Current.Server.MapPath("~"+logo))))
            {
                LinkedResource imagelink = new LinkedResource(HttpContext.Current.Server.MapPath("~" + logo), "image/gif");
                imagelink.ContentId = "logoId";
                imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imagelink);
            }
            message.AlternateViews.Add(htmlView);
            /*try
            {*/
            smtp.Send(message);
            /*}
            catch(System.Net.Mail.SmtpException e)
            {
                return -1;
            }*/
            return 0;
        }
    }
}