using Repository.DomainModel.Entry;
using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Entry
{
    public class BuySlipConfig:EntityTypeConfiguration<BuySlip>
    {
        public BuySlipConfig()
        {
            this.HasKey(x => x.Id);

            this.HasOptional(x => x.Period)
                .WithMany()
                .HasForeignKey(x => x.PeriodId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.ApplicationUser)
                .WithMany()
                .HasForeignKey(x => x.AppUserId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.EntrySlipType)
                .WithMany()
                .HasForeignKey(x => x.EntrySlipTypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
