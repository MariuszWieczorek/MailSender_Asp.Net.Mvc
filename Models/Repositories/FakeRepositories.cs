using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.Repositories
{
    public class FakeRepositories
    {
        public List<Address> GetFakeAddresses(string userId)
        {
            return new List<Address>
            {
                new Address{Id = 1, Name="Jan Kowalski",Email="jan.kowalski@onet.pl" },
                new Address{Id = 2, Name="Marek Janicki",Email="marek.janicki@onet.pl"},
                new Address{Id = 3, Name="Aleksandra Nowak",Email="ola.nowak@onet.pl" },
                new Address{Id = 4, Name="Marek Nowak",Email="marek.nowak@gmial.com" },
                new Address{Id = 5, Name="Wioletta Krysiak",Email="wiola.k@gmial.com" },
            };
        }

        // Testowa lista wiadomości Email
        public List<Email> GetFakeEmails(string userId)
        {

            var addreses = GetFakeAddresses("1");

            return new List<Email>
            {
                new Email
                {
                    Id = 1,
                    Subject = "uwagi dotyczące błędów a aplikacji",
                    CreatedDate = DateTime.Now,
                    SentDate = DateTime.Now,
                    EmailRecipients = new List<EmailRecipient>
                    {
                        
                        new EmailRecipient {Address = addreses.First(x => x.Id == 1) },
                        new EmailRecipient {Address = addreses.First(x => x.Id == 2) },
                    }
                },

                new Email
                {
                    Id = 2,
                    Subject = "potwierdzenie dokonania płatności",
                    CreatedDate = DateTime.Now,
                    SentDate = DateTime.Now,
                    EmailRecipients = new List<EmailRecipient>
                    {
                       new EmailRecipient {Address = addreses.First(x => x.Id == 4) },
                       new EmailRecipient {Address = addreses.First(x => x.Id == 2) },
                       new EmailRecipient {Address = addreses.First(x => x.Id == 1) },
                    }
                },

                new Email
                {
                    Id = 3,
                    Subject = "potwierdzenie wysłania zamówienia",
                    CreatedDate = DateTime.Now,
                    SentDate = DateTime.Now,
                    EmailRecipients = new List<EmailRecipient>
                    {
                        new EmailRecipient {Address = addreses.First(x => x.Id == 5) },
                        new EmailRecipient {Address = addreses.First(x => x.Id == 4) },
                    }
                }

            };
        }

    }
}