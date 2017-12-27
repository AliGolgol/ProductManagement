using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Order;

namespace Repository.DataLayer.Mapping.Order
{
    public class StockConfig:EntityTypeConfiguration<Stock>
    {
        public StockConfig()
        {
            this.HasKey(s => s.Id);

            this.HasOptional(a => a.ApplicationUser)
                .WithMany(d=>d.Stocks)
                .HasForeignKey(s => s.ApplicationUserId)
                .WillCascadeOnDelete(false);

            this.HasRequired(p => p.Period)
                .WithMany()
                .HasForeignKey(p => p.PeriodId)
                .WillCascadeOnDelete(false);

            this.HasOptional(s => s.Supplier)
                .WithMany(x=>x.Stocks)
                .HasForeignKey(x => x.SupplierId)
                .WillCascadeOnDelete(false);

            this.HasOptional(e => e.EntrySlipType)
                .WithMany(e => e.Stocks)
                .HasForeignKey(f => f.EntrySlipTypeId)
                .WillCascadeOnDelete(false);

        }
    }
}
