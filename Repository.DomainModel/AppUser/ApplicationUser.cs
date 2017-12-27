using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.BuilderProperties;
using Repository.DomainModel.Catalog;
using Repository.DomainModel.Order;
using Repository.DomainModel.Entry;

namespace Repository.DomainModel.AppUser
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        // Add other properties here
        
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        public int? AddressId { get; set; }

        public virtual ICollection<Order.Order> Orders { get; set; }
        public ICollection<Stock> Stocks { get; set; }

        public ICollection<EntrySlip> EntrySlips { get; set; }        

    }
}
