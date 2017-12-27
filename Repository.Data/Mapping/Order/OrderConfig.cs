using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Mapping.Order
{
    public class OrderConfig:EntityTypeConfiguration<DomainModel.Order.Order>
    {
        public DomainModel.Order.Order Order { get; set; }

        public OrderConfig()
        {
            this.HasKey(o => o.Id);
            this.HasOptional(ou => ou.Unit)
                .WithMany()
                .HasForeignKey(u => u.UnitId);

            this.HasOptional(p=>p.Period)
                .WithMany()
                .HasForeignKey(op=>op.PeriodId)
                .WillCascadeOnDelete(false);

        }
    }
}
