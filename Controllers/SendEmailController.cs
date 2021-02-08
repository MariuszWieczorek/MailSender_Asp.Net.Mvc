using MailSender.Models.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MailSender.Controllers
{
      
    public class SendEmailController : Controller
    {

        private EmailRepository _emailRepository = new EmailRepository();
        public ActionResult Send(int id)
        {
            var userId = User.Identity.GetUserId();
            var email = _emailRepository.GetEmail(userId, id);


            return Json(new
            {
                Success = true,
                Message = "Mail został wysłany"
            });
        }

    }
}