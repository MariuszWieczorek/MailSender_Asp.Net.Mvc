using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MailSender.Models.Repositories
{
    public class EmailRepository
    {
        public List<Email> GetEmails(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails
                    .Include(x=>x.EmailRecipients)
                    .Include(x => x.EmailRecipients.Select(y => y.Address))
                    .Where(x => x.UserId == userId).ToList();
            }
        }

        public Email GetEmail(string userId, int id)
        {
            using (var context = new ApplicationDbContext())
            {   

             
                    var z = context.Emails
                    .Include(x => x.EmailRecipients)
                    .Include(x => x.EmailRecipients.Select(y => y.Address))
                    .Single(x => x.UserId == userId && x.Id == id);

                return z;
            }
        }

            //       .Include(x => x.EmailRecipients)
            //    .Include(x => x.EmailRecipients.Select(y => y.EmailRecipient))

        public void AddEmail(Email email, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Emails.Add(email);
                context.SaveChanges();
            }
        }

        public void UpdateEmail(Email email, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // pobieramy pojedynczy rekord z adresem do aktualizacji
                var emailToUpdate = context.Emails
                    .Single(x => x.Id == email.Id && x.UserId == email.UserId);

                // dokonujemu zmian  
                emailToUpdate.Subject = email.Subject;
                emailToUpdate.Message = email.Message;

                // zapisujemy zmiany 
                context.SaveChanges();
            }
        }

        public Email GetNewEmail(string userId)
        {
            return new Email
            {
                Id = 0,
                UserId = userId,
                Subject = "",
                CreatedDate = DateTime.Now,
                SentDate = null,
                EmailRecipients = new List<EmailRecipient>()
            };
        }

        // Usuwamy adresata wiadomości
        public void DeleteEmail(int emailId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // załączamy Email aby dobrać się do właściwości UserId
                var emailToDelete = context.Emails
                    .Single(x =>
                    x.Id == emailId &&
                    x.UserId == userId);

                context.Emails.Remove(emailToDelete);
                context.SaveChanges();
            }
        }
    }
}