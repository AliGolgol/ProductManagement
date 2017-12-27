using Repository.DomainModel.Catalog;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataLayer.Mapping.Catalog
{
    public class BillItemConfig:EntityTypeConfiguration<BillItem>
    {
        public BillItemConfig()
        {
            this.HasKey(x => x.Id);

            this.HasRequired(x => x.Product)
                .WithMany(x => x.BillItems)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);

            this.HasRequired(x => x.Bill)
                .WithMany(x => x.BillItems)
                .HasForeignKey(x => x.BillId)
                .WillCascadeOnDelete();
        }
    }
}
