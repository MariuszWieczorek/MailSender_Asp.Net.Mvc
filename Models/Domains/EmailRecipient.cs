using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MailSender.Models.Domains
{
    public class EmailRecipient
    {
        public int Id { get; set; }
        public int EmailId { get; set; }
        [Display(Name = "Odbiorca")]
        [Required(ErrorMessage = "Pole Adres e-mail jest wymagane !!!")]
        public int AddressId { get; set; }
        public Email Email { get; set; }
        public Address Address { get; set; }
    }
}