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

        private FakeRepositories _fakeRepositories = new FakeRepositories();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var emails = _fakeRepositories.GetFakeEmails(userId);
            return View(emails);
        }

        public ActionResult Email(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var email = id == 0 ?
            _fakeRepositories.GetNewEmail(userId) :
            _fakeRepositories.GetFakeEmails(userId).First(x => x.Id == 1);
            
                        
            var editEmail = PrepareEmailVm(email,userId);
            return View(editEmail);
        }

        public ActionResult Address(int id = 0)
        {
            var userId = User.Identity.GetUserId();

            var address = id == 0 ?
                 _fakeRepositories.GetNewAddress(userId) :
                 _fakeRepositories.GetFakeAddresses(userId).First(x => x.Id == 1);

            var editAddress = PrepareEditAddressVm(address, userId);

            return View(editAddress);
        }

        public ActionResult Addresses()
        {
            var userId = User.Identity.GetUserId();
            var addresses = _fakeRepositories.GetFakeAddresses(userId);
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
                Addresses = _fakeRepositories.GetFakeAddresses(userId)
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