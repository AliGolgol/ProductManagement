using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Order;

namespace Repository.DataLayer.Mapping.Order
{
    public class StockItemConfig:EntityTypeConfiguration<StockItem>
    {
        public StockItemConfig()
        {
            this.HasKey(si => si.Id);

            this.HasRequired(x => x.Products)
                .WithMany(p=>p.StockItems)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(false);
        }
    }
}
