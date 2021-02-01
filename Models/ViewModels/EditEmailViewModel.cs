using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.ViewModels
{
    public class EditEmailViewModel
    {
        public Email Email { get; set; }
        public List<Address> Addresses { get; set; }
        public string Heading { get; set; }
    }
}