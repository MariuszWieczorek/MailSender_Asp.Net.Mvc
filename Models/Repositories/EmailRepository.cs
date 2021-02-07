using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.Repositories
{
    public class EmailRepository
    {
        public List<Email> GetEmails(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Where(x => x.UserId == userId).ToList();
            }
        }

        public Email GetEmail(string userId, int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Emails.Single(x => x.UserId == userId && x.Id == id);
            }
        }

    }
}