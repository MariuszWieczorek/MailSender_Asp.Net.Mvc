using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace MailSender.Models.Domains
{
    public class Address
    {
        public Address()
        {
            EmailRecipients = new Collection<EmailRecipient>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Pole nazwa jest wymagane !!!")]
        [Display(Name = "Nazwa")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Pole adres jest wymagane !!!")]
        [Display(Name = "Adres e-mail")]
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "adres e-mail jest nieprawidłowy")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Brak właściciela wpisu w książce adresowej !!!")]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<EmailRecipient> EmailRecipients { get; set; }

    
    }
}