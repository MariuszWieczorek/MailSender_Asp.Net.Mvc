using MailSender.Models.Domains;
using MailSender.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailSender.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var emails = new List<Email>
            {
                new Email
                {
                    Id = 1,
                    Subject = "uwagi dotyczące błędów a aplikacji",
                    CreatedDate = DateTime.Now,
                    SentDate = DateTime.Now,
                    EmailRecipients = new List<EmailRecipient>
                    {
                        new EmailRecipient {Address = new Address{Name="Jan",Email="jan@onet.pl" } },
                        new EmailRecipient {Address = new Address{Name="Marek",Email="marek@onet.pl" } },
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
                        new EmailRecipient {Address = new Address{Name="Jan",Email="jan@onet.pl" } },
                        new EmailRecipient {Address = new Address{Name="Marek",Email="marek@onet.pl" } },
                    }
                },

            };


            return View(emails);
        }

        public ActionResult Email()
        {
            return View();
        }

        public ActionResult Address(int id = 0)
        {
            var userId = User.Identity.GetUserId();
            
            var address = id == 0 ?
                 GetNewAddress(userId) :
                new Address { Id = 1, Name = "Aleksandra Nowak", Email = "ola.nowak@onet.pl" };

            var editAddress = PrepareEditAddressVm(address, userId);

            return View(editAddress);
        }

        public ActionResult Addresses()
        {
            var userId = User.Identity.GetUserId();
            var addresses = GetFakeAddresses(userId);
            return View(addresses);
        }


        #region prepare View Model
        private EditAddressViewModel PrepareEditAddressVm(Address address, string userId)
        {
            return new EditAddressViewModel
            {
                Address = address,
                Heading = address.Id == 0 ? "Dodawanie Nowego Adresu" : "Edycja",
            };
        }

        private EditEmailViewModel PrepareEmailVm(Email email, string userId)
        {
            return new EditEmailViewModel
            {
                Email = email,
                Heading = email.Id == 0 ? "Dodawanie Nowej Wiadomości" : "Edycja",
                Addresses = GetFakeAddresses(userId)
            };
        }

        #endregion

        private Address GetNewAddress(string userId)
        {
            return new Address
            {
                UserId = userId,
                Name = string.Empty,
                Email = string.Empty
            };
        }

        private List<Address> GetFakeAddresses(string userId)
        {
            return new List<Address>
            {
                new Address{Id = 1, Name="Jan Kowalski",Email="jan.kowalski@onet.pl" },
                new Address{Id = 2, Name="Marek Janicki",Email="marek.janicki@onet.pl"},
                new Address{Id = 3, Name="Aleksandra Nowak",Email="ola.nowak@onet.pl" }
            };
        }

        #region okna informacyjne
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion
    }
}