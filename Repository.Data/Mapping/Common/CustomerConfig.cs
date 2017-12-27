using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.Common;

namespace Repository.Data.Mapping.Common
{
    public class CustomerConfig:EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            this.HasKey(x => x.Id);

            this.HasOptional(c => c.Address)
                .WithMany(c => c.Customers)
                .HasForeignKey(a => a.AddressId)
                .WillCascadeOnDelete(false);
        }
    }
}
