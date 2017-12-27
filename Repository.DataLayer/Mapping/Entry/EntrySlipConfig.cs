using Repository.DomainModel.Entry;
using System.Data.Entity.ModelConfiguration;

namespace Repository.DataLayer.Mapping.Entry
{
    public class EntrySlipConfig:EntityTypeConfiguration<EntrySlip>
    {
        public EntrySlipConfig()
        {
            this.HasKey(pk => pk.Id);

            this.HasOptional(x => x.ApplicationUser)
                .WithMany(x => x.EntrySlips)
                .HasForeignKey(fk => fk.UserId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.EntrySlipType)
                .WithMany(x => x.EntrySlips)
                .HasForeignKey(fk => fk.ESTypeId)
                .WillCascadeOnDelete(false);

            this.HasOptional(x => x.Period)
                .WithMany(x => x.EntrySlips)
                .HasForeignKey(x => x.PeriodId)
                .WillCascadeOnDelete(false);

            this.Property(p => p.Description).HasMaxLength(500);
        }
    }
}
