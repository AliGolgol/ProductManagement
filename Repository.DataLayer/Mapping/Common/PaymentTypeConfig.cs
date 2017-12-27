using Repository.DomainModel.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataLayer.Mapping.Common
{
    public class PaymentTypeConfig:EntityTypeConfiguration<PaymentType>
    {
        public PaymentTypeConfig()
        {
            this.HasKey(x => x.Id);

            this.Property(p => p.Description)
                .HasMaxLength(500);

        }
    }
}
