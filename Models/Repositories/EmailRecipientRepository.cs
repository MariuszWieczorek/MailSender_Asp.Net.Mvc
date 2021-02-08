using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MailSender.Models.Repositories
{
    public class EmailRecipientRepository
    {

        public EmailRecipient GetNewEmailRecipient(int emailId)
        {
            return new EmailRecipient
            {
                EmailId = emailId
            };
        }


        public EmailRecipient GetEmailRecipient(int emailId, int emailRecipientId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EmailRecipients
                    .Include(x=>x.Email)
                    .Include(x => x.Address)
                    .Single(x => x.Id == emailRecipientId
                    && x.EmailId == emailId
                    && x.Email.UserId == userId);
            }
        }

        public void AddEmailRecipient(EmailRecipient emailRecipient, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                context.EmailRecipients.Add(emailRecipient);
                context.SaveChanges();
            }
        }

        public void UpdateEmailRecipient(EmailRecipient emailRecipient, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // pobieramy pojedynczy rekord z adresem do aktualizacji
                var emailRecipientToUpdate = context.EmailRecipients
                    .Include(x=>x.Email)
                    .Single(x => x.Id == emailRecipient.Id && x.EmailId == emailRecipient.EmailId);

                if (emailRecipientToUpdate.Email.UserId != userId)
                {
                    throw new Exception("użytkownik nie ma uprawnień do modyfikacji tego e-maila.");
                }

                // dokonujemu zmian  
                emailRecipientToUpdate.AddressId = emailRecipient.AddressId;

                // zapisujemy zmiany 
                context.SaveChanges();
            }
        }

        // Usuwamy adresata wiadomości
        public void DeleteEmailRecipient(int emailId, int emailRecipientId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // załączamy Email aby dobrać się do właściwości UserId
                var emailRecipientToDelete = context.EmailRecipients
                    .Include(x => x.Email)
                    .Single(x =>
                    x.EmailId == emailId &&
                    x.Id == emailRecipientId &&
                    x.Email.UserId == userId);

                context.EmailRecipients.Remove(emailRecipientToDelete);
                context.SaveChanges();
            }
        }

    }
}