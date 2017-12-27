using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.AppUser;

namespace Repository.DomainModel.Common
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Tell { get; set; }
        public string Fax { get; set; }
        
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
