using System.Data.Entity.ModelConfiguration;
using Repository.DomainModel.Order;

namespace Repository.DataLayer.Mapping.Order
{
    public class OrderItemConfig:EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfig()
        {
            this.HasRequired(o => o.Products)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            this.HasRequired(orderitem => orderitem.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId);

           
        }
    }
}
