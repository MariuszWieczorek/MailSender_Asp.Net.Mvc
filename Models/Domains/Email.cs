using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;

namespace MailSender.Models.Domains
{
    public class Email
    {
        public Email()
        {
            EmailRecipients = new Collection<EmailRecipient>();
        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Temat")]
        public string Subject { get; set; }
        [Display(Name = "Data Utworzenia")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Data Wysłania")]
        public DateTime SentDate { get; set; }
        [Display(Name = "Treść")]
        public string Message { get; set; }
      
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        
        public ApplicationUser User { get; set; }
        
        public ICollection<EmailRecipient> EmailRecipients { get; set; }
    }
}