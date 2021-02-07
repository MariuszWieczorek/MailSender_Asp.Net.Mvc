using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.Repositories
{
    public class FakeRepository
    {


        public List<Address> GetAddresses(string userId)
        {
            return new List<Address>
            {
                new Address{Id = 1, Name="Jan Kowalski",Email="jan.kowalski@onet.pl",UserId = "b3a63cf4-45ef-4476-a96a-0ec5caac1ebb" },
                new Address{Id = 2, Name="Marek Janicki",Email="marek.janicki@onet.pl",UserId = "b3a63cf4-45ef-4476-a96a-0ec5caac1ebb"},
                new Address{Id = 3, Name="Aleksandra Nowak",Email="ola.nowak@onet.pl",UserId = "b3a63cf4-45ef-4476-a96a-0ec5caac1ebb" },
                new Address{Id = 4, Name="Marek Nowak",Email="marek.nowak@gmial.com",UserId = "b3a63cf4-45ef-4476-a96a-0ec5caac1ebb" },
                new Address{Id = 5, Name="Wioletta Krysiak",Email="wiola.k@gmial.com",UserId = "b3a63cf4-45ef-4476-a96a-0ec5caac1ebb" },
            };
        }

        // Testowa lista wiadomości Email
        public List<Email> GetEmails(string userId)
        {

            var addreses = GetAddresses(userId);

            return new List<Email>
            {
                new Email
                {
                    Id = 1,
                    UserId = "b3a63cf4-45ef-4476-a96a-0ec5caac1ebb",
                    Subject = "uwagi dotyczące błędów a aplikacji",
                    CreatedDate = DateTime.Now,
                    SentDate = DateTime.Now,
                    Message = "treść maila nr 1",
                    EmailRecipients = new List<EmailRecipient>
                    {
                        
                        new EmailRecipient {Id = 1, EmailId = 1, AddressId = 1,Address = addreses.First(x => x.Id == 1) },
                        new EmailRecipient {Id = 2, EmailId = 1, AddressId = 2,Address = addreses.First(x => x.Id == 2) },
                    }
                },

                new Email
                {
                    Id = 2,
                    UserId = "b3a63cf4-45ef-4476-a96a-0ec5caac1ebb",
                    Subject = "potwierdzenie dokonania płatności",
                    CreatedDate = DateTime.Now,
                    SentDate = DateTime.Now,
                    Message = "treść maila nr 2",
                    EmailRecipients = new List<EmailRecipient>
                    {
                       new EmailRecipient {Id = 1, EmailId = 2,AddressId = 4,Address = addreses.First(x => x.Id == 4) },
                       new EmailRecipient {Id = 2, EmailId = 2,AddressId = 2,Address = addreses.First(x => x.Id == 2) },
                       new EmailRecipient {Id = 3, EmailId = 2,AddressId = 1,Address = addreses.First(x => x.Id == 1) },
                    }
                },

                new Email
                {
                    Id = 3,
                    UserId = "b3a63cf4-45ef-4476-a96a-0ec5caac1ebb",
                    Subject = "potwierdzenie wysłania zamówienia",
                    CreatedDate = DateTime.Now,
                    SentDate = DateTime.Now,
                    Message = "treść maila nr 3",
                    EmailRecipients = new List<EmailRecipient>
                    {
                        new EmailRecipient {Id = 1, EmailId = 3,AddressId = 5,Address = addreses.First(x => x.Id == 5) },
                        new EmailRecipient {Id = 2, EmailId = 3,AddressId = 4,Address = addreses.First(x => x.Id == 4) },
                    }
                }

            };
        }

        // Pobiera do edycji odbiorcę wiadomości z listy
        public EmailRecipient GetEmailRecipient(int emailId, int emailRecipientId, string userId)
        {
            var email =  GetEmails(userId).First(x => x.Id == emailId);
            return email.EmailRecipients.First(x => x.Id == emailRecipientId); 
        }

        // zwraca nowego odbiorcę wiadomości
        public EmailRecipient GetNewEmailRecipient(int emailId, int emailRecipientId)
        {
            return new EmailRecipient
            {
                EmailId = emailId
            };
        }

        public Email GetEmail(string userId, int id)
        {
            return GetEmails(userId).First(x => x.Id == id);
        }

        public Address GetAddress(string userId, int id)
        {
            return GetAddresses(userId).First(x => x.Id == id);
        }

        public Address GetNewAddress(string userId)
        {
            return new Address
            {
                UserId = userId,
                Name = string.Empty,
                Email = string.Empty
            };
        }

        public Email GetNewEmail(string userId)
        {
            return new Email
            {
                Id = 0,
                UserId = userId,
                Subject = "",
                CreatedDate = DateTime.Now,
                SentDate = DateTime.Now,
                EmailRecipients = new List<EmailRecipient>()
            };   
        }

        // -------------------------------------------------------------------------------------
    }
}