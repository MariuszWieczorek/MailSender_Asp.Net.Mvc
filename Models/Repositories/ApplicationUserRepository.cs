using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.Repositories
{
    public class ApplicationUserRepository
    {

        public ApplicationUser GetApplicationUser(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users
                    .Single(x => x.Id == userId);
            }
        }
        public void UpdateApplicationUser(ApplicationUser user, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // pobieramy pojedynczy rekord z adresem do aktualizacji
                var userToUpdate = context.Users
                    .Single(x => x.Id == user.Id);

                if (userToUpdate.Id != userId)
                {
                    throw new Exception("użytkownik nie ma uprawnień do modyfikacji tego adresu.");
                }

                // dokonujemu zmian  
                userToUpdate.Email = user.Email;
                userToUpdate.SenderEmail = user.SenderEmail;
                userToUpdate.SenderName = user.SenderName;
                userToUpdate.SenderEmailPassword = user.SenderEmailPassword;
                userToUpdate.HostSmtp = user.HostSmtp;
                userToUpdate.Port = user.Port;
                userToUpdate.EnableSsl = user.EnableSsl;

                // zapisujemy zmiany 
                context.SaveChanges();

            }

        }
        
    }
}