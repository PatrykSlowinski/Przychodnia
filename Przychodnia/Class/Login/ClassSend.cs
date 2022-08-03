using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
namespace Przychodnia.Class.Login
{
    static class ClassSend
    {
        //Method that sends code to change password on email.
        public static string EmailSender(string login, string emailAddress)
        {
            try
            {
                var fromAddress = new MailAddress("ClinicWarsaw@gmail.com", "Clinic");
                var toAddress = new MailAddress(emailAddress, login);
                const string fromPassword = "ClinicWarsaw1";
                Random rnd = new Random();
                string subject = "Reset password";
               //Generate random code
                string body = (rnd.Next(9999999)).ToString();

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body + " is your verification code"
                })
                {
                    smtp.Send(message);
                }
                return body;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
