using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Common
{
    public class Customer:Person
    {
       public int Id { get; set; }
        public virtual Address Address { get; set; } 
        public int? AddressId { get; set; }
    }
}
