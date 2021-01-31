using System.ComponentModel.DataAnnotations;

namespace MailSender.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
