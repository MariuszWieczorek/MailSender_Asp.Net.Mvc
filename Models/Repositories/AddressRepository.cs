using MailSender.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailSender.Models.Repositories
{
    public class AddressRepository
    {
        public List<Address> GetAddresses(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Addresses.Where(x => x.UserId == userId).ToList();
            }
        }

        public Address GetAddress(string userId, int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Addresses.Single(x => x.UserId == userId && x.Id == id);
            }
        }

        public void AddPosition(Address address, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
              
                context.Addresses.Add(address);
                context.SaveChanges();
            }
        }

        public void UpdatePosition(Address address, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                // pobieramy pojedynczy rekord z adresem do aktualizacji
                var addressToUpdate = context.Addresses
                    .Single(x => x.Id == address.Id && x.UserId == address.UserId);

                if (addressToUpdate.UserId != userId )
                {
                    throw new Exception("użytkownik nie ma uprawnień do modyfikacji tego adresu.");
                } 

                // dokonujemu zmian  
                addressToUpdate.Name = address.Name;
                addressToUpdate.Email = address.Email;

                // zapisujemy zmiany 
                context.SaveChanges();
            }
        }

        public Address GetNewAddress(string userId)
        {
            return new Address
            {
                UserId = userId,
                Name = string.Empty,
                Email = string.Empty
            };
        }
    }
}