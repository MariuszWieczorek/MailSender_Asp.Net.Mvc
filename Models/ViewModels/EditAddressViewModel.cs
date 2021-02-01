using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.ViewModels
{
    public class EditAddressViewModel
    {
        public Address Address { get; set; }
        public string Heading { get; set; }
    }
}