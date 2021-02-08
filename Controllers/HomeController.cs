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
     // private FakeRepository              _fakeRepository = new FakeRepository();
        private AddressRepository           _addressRepository = new AddressRepository();
        private EmailRepository             _emailRepository = new EmailRepository();
        private EmailRecipientRepository    _emailRecipientRepository = new EmailRecipientRepository();

        #region lista wiadomości i lista adresów

        // Strona główna - lista wiadomości
        [HttpGet]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var emails = _emailRepository.GetEmails(userId);
            return View(emails);
        }

        // Książka adresowa
        [HttpGet]
        public ActionResult Addresses()
        {
            var userId = User.Identity.GetUserId();
            var addresses = _addressRepository.GetAddresses(userId);
            return View(addresses);
        }
        #endregion

        #region Email dodawanie, edycja, usuwanie wiadomości 

        [HttpGet]
        public ActionResult Email(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var email = id == 0 ?
            _emailRepository.GetNewEmail(userId) :
            _emailRepository.GetEmail(userId,id);
            
                        
            var editEmail = PrepareEmailVm(email,userId);
            return View(editEmail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Email(Email email)
        {
            var userId = User.Identity.GetUserId();

            // jeżeli nie przejdzie walidacji
            if (!ModelState.IsValid)
            {
                var vm = PrepareEmailVm(email, userId);
                return View("Email", vm);
            }

  
                if (email.Id == 0)
                    _emailRepository.AddEmail(email, userId);
                else
                    _emailRepository.UpdateEmail(email, userId);


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteEmail(int emailId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRepository.DeleteEmail(emailId, userId);
            }
            catch (Exception exception)
            {
                // TODO: logowanie do pliku niepowodzenie usunięcia pozycji
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }

        #endregion wiadomości

        #region  Address - książka adresowa: dodawanie,edycja,usuwanie adresu
        [HttpGet]
        public ActionResult Address(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var address = id == 0 ?
                 _addressRepository.GetNewAddress(userId) :
                 _addressRepository.GetAddress(userId,id);

            var editAddress = PrepareEditAddressVm(address, userId);

            return View(editAddress);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Address(Address address)
        {
            var userId = User.Identity.GetUserId();
       
            // jeżeli nie przejdzie walidacji
            if (!ModelState.IsValid)
            {
                var vm = PrepareEditAddressVm(address, userId);
                return View("Address", vm);
            }


            if (address.Id == 0)
                _addressRepository.AddAddress(address, userId);
            else
                _addressRepository.UpdateAddress(address, userId);


            return RedirectToAction("Addresses", new { id = address.Id });
        }

        [HttpPost]
        public ActionResult DeleteAddress(int addressId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _addressRepository.DeleteAddress(addressId, userId);
            }
            catch (Exception exception)
            {
                // TODO: logowanie do pliku niepowodzenie usunięcia pozycji
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }
        #endregion

        #region EmailRecipient - listy adresatów wiadomości: dodawanie, edycja, usuwanie

        [HttpGet]
        public ActionResult EmailRecipient(int emailId = 0, int emailRecipientId = 0)
        {
            var userId = User.Identity.GetUserId();

            var emailRecipient = emailRecipientId == 0 ?
                _emailRecipientRepository.GetNewEmailRecipient(emailId) :
                _emailRecipientRepository.GetEmailRecipient(emailId, emailRecipientId, userId);

            var editEmailRecipient  = PrepareEmailRecipientVm(emailRecipient, userId);
            return View(editEmailRecipient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmailRecipient(EmailRecipient emailRecipient)
        {
            var userId = User.Identity.GetUserId();
            // zweryfikujemy, czy uzytkownik próbuje zaktualizować swojego maila

            if (!ModelState.IsValid)
            {
                var vm = PrepareEmailRecipientVm(emailRecipient,userId);
                return View("EmailRecipient", vm);
            }

            if (emailRecipient.Id == 0)
                _emailRecipientRepository.AddEmailRecipient(emailRecipient, userId);
            else
                _emailRecipientRepository.UpdateEmailRecipient(emailRecipient, userId);


            return RedirectToAction("Email", new { id = emailRecipient.EmailId });
        }

        [HttpPost]
        public ActionResult DeleteEmailRecipient(int emailId, int emailRecipientId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                _emailRecipientRepository.DeleteEmailRecipient(emailId, emailRecipientId, userId);
            }
            catch (Exception exception)
            {
                // TODO: logowanie do pliku niepowodzenie usunięcia pozycji
                return Json(new { Success = false, Message = exception.Message });
            }

            return Json(new { Success = true });
        }

        #endregion

        #region prepare View Model
        private EditAddressViewModel PrepareEditAddressVm(Address address, string userId)
        {
            return new EditAddressViewModel
            {
                Address = address,
                Heading = address.Id == 0 ? "Nowy Adres" : "Edycja Adresu",
            };
        }

        private EditEmailViewModel PrepareEmailVm(Email email, string userId)
        {
            return new EditEmailViewModel
            {
                Email = email,
                Heading = email.Id == 0 ? "Nowa Wiadomość" : "Edycja Wiadomości",
                Addresses = _addressRepository.GetAddresses(userId)
            };
        }


        private EditEmailRecipientViewModel PrepareEmailRecipientVm(EmailRecipient emailRecipient, string userId)
        {
            return new EditEmailRecipientViewModel
            {
                EmailRecipient = emailRecipient,
                Heading = emailRecipient.Id == 0 ? "Dodanie Adresata Wiadomości" : "Edycja Adresata Wiadomości",
                Addresses = _addressRepository.GetAddresses(userId)
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