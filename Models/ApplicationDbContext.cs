using System.Data.Entity;
using MailSender.Models.Domains;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MailSender.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<EmailRecipient> EmailRecipients { get; set; }
    
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // jak nie chcemy aby przy usunięciu usera
            // zostały również usunięte przypisane do niego e-maile
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Emails)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            // jak nie chcemy aby przy usunięciu usera
            //zostali również usunięci przypisani do niego adresy e-mail
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Addresses)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}