using SuperHero.BL.DomainModelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.Helper
{
    public static class MaullSetting
    {
        public static string MailSender(string Host, int Port, string from, string password, EmailSetting mail, string TempleteHtml, Attachment? Attchments = null)
        {
            try
            {
                var STMP = new SmtpClient(Host, Port);
                STMP.DeliveryMethod = SmtpDeliveryMethod.Network;
                STMP.UseDefaultCredentials = false;
                STMP.EnableSsl = true;
                STMP.Timeout = 100000;



                STMP.Credentials = new NetworkCredential(from, password, "Super Hero");

                MailMessage mail2 = new MailMessage(from, mail.ToEmail);
                mail2.IsBodyHtml = true;
                mail2.Subject = mail.Subject;
                mail2.Body = TempleteHtml;




                if (Attchments != null)
                {

                    mail2.Attachments.Add(Attchments);

                }

                STMP.Send(mail2);
                return "Send Success";
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
