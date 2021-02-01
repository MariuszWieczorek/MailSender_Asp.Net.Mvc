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
        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Adres")]
        public string Email { get; set; }
        
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<EmailRecipient> EmailRecipients { get; set; }


       
    }
}