using System.ComponentModel.DataAnnotations;

namespace MailSender.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

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
