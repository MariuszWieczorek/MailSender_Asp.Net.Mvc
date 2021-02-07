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

        internal void AddPosition(Address address, string userId)
        {
            throw new NotImplementedException();
        }

        internal void UpdatePosition(Address address, string userId)
        {
            throw new NotImplementedException();
        }
    }
}