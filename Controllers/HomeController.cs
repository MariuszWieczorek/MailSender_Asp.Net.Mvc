using MailSender.Models.Domains;
using MailSender.Models.Repositories;
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

        private FakeRepository _fakeRepository = new FakeRepository();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var emails = _fakeRepository.GetEmails(userId);
            return View(emails);
        }

        public ActionResult Email(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var email = id == 0 ?
            _fakeRepository.GetNewEmail(userId) :
            _fakeRepository.GetEmail(userId,id);
            
                        
            var editEmail = PrepareEmailVm(email,userId);
            return View(editEmail);
        }

        public ActionResult Address(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var address = id == 0 ?
                 _fakeRepository.GetNewAddress(userId) :
                 _fakeRepository.GetAddress(userId,id);

            var editAddress = PrepareEditAddressVm(address, userId);

            return View(editAddress);
        }

        public ActionResult Addresses()
        {
            var userId = User.Identity.GetUserId();
            var addresses = _fakeRepository.GetAddresses(userId);
            return View(addresses);
        }

        public ActionResult EmailRecipient(int emailId = 0, int emailRecipientId = 0)
        {
            var userId = User.Identity.GetUserId();

            var emailRecipient = emailRecipientId == 0 ?
                _fakeRepository.GetNewEmailRecipient(emailId, emailRecipientId) :
                _fakeRepository.GetEmailRecipient(emailId, emailRecipientId, userId);

            var editEmailRecipient  = PrepareEmailRecipientVm(emailRecipient, userId);
            return View(editEmailRecipient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailRecipient(EmailRecipient emailRecipient)
        {
            var userId = User.Identity.GetUserId();
            // zweryfikujemy, czy uzytkownik próbuje zaktualizować swoją fakturę
            // dodajemy lub aktualizujemy pozycję faktury
            // wyliczymy wartość pozycji
            // wyliczymy i zaktualizujemy wrtość faktury


            if (!ModelState.IsValid)
            {
                var vm = PrepareEmailRecipientVm(emailRecipient,userId);
                return View("EmailRecipient", vm);
            }

            /*
            var product = _productRepository.GetProduct(invoicePosition.ProductId);
            invoicePosition.Value = product.Value * invoicePosition.Quantity;

            if (invoicePosition.Id == 0)
                _invoiceRepository.AddPosition(invoicePosition, userId);
            else
                _invoiceRepository.UpdatePosition(invoicePosition, userId);

            _invoiceRepository.UpdateInvoiceValue(invoicePosition.InvoiceId, userId);
            */

            return RedirectToAction("Email", new { id = emailRecipient.EmailId });
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
                Addresses = _fakeRepository.GetAddresses(userId)
            };
        }


        private EditEmailRecipientViewModel PrepareEmailRecipientVm(EmailRecipient emailRecipient, string userId)
        {
            return new EditEmailRecipientViewModel
            {
                EmailRecipient = emailRecipient,
                Heading = emailRecipient.Id == 0 ? "Dodawanie Nowego Adresata" : "Edycja Adresata Wiadomości",
                Addresses = _fakeRepository.GetAddresses(userId)
            };
        }
        #endregion

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