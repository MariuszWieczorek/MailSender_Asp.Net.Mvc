using MailSender.Models.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cipher;
using EmailSender;
using System.Configuration;
using System.Threading.Tasks;

namespace MailSender.Controllers
{
      
    public class SendEmailController : AsyncController
    {

        private EmailRepository _emailRepository = new EmailRepository();
        public async Task<ActionResult> Send(int id)
        {
            var userId = User.Identity.GetUserId();
            var email = _emailRepository.GetEmail(userId, id);


            var emailSender = new EmailSender.Email(new EmailParams
            {
                HostSmtp = "smtp.gmail.com",
                Port = int.Parse("587"),
                EnableSsl = bool.Parse("true"),
                SenderName = "Mariusz Wieczorek",
                SenderEmail = "mariusz.wieczorek.testy@gmail.com",
                SenderEmailPassword = "hasełko"
            });

            // DecryptSenderEmailPassword(),"rmhfvaurzyxnuztn"

            // Listę odbiorców maila konwertuję to listy adresów
            var  listOfEmailRecipients = new List<string>();
            foreach (var emailRecipient in email.EmailRecipients)
                listOfEmailRecipients.Add(emailRecipient.Address.Email);

            
            await emailSender.Send(email.Subject, email.Message, listOfEmailRecipients);

            _emailRepository.UpdateSentDate(email, userId);

            return Json(new
            {
                Success = true,
                Message = "Mail został wysłany"
            });
        }

        private static string DecryptSenderEmailPassword()
        {
            var stringCipher = new StringCipher("163F0C86-673A-426F-97CA-2A60A44134C7");
            var encryptedPassword = ConfigurationManager.AppSettings["SenderEmailPassword"];
            if (encryptedPassword.StartsWith("encrypt:"))
            {
                var passwordToEncrypt = encryptedPassword.Replace("encrypt:", string.Empty);
                encryptedPassword = stringCipher.Encrypt(passwordToEncrypt);

                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configFile.AppSettings.Settings["SenderEmailPassword"].Value = encryptedPassword;
                configFile.Save();
            }
            return stringCipher.Decrypt(encryptedPassword);
        }

    }
}