using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.ViewModels
{
    public class ApplicationUserDataViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
          
        [Required]
        [Display(Name = "Nazwa Użytkownika")]
        public string SenderName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "e-mail nadawcy")]
        public string SenderEmail { get; set; }

        [Required]
        [Display(Name = "hasło do e-maila nadawcy")]
        public string SenderEmailPassword { get; set; }

        [Required]
        [Display(Name = "Serwer SMTP")]
        public string HostSmtp { get; set; }

        [Required]
        [Display(Name = "Port")]
        public int Port { get; set; }

        [Required]
        [Display(Name = "Włączony SSL")]
        public bool EnableSsl { get; set; }

    }
}