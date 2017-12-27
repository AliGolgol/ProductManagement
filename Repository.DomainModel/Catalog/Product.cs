using System.Collections.Generic;
using System.ComponentModel;
using Repository.DomainModel.Order;
using Repository.DomainModel.Entry;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Catalog
{
    public class Product
    {
        public int Id { get; set; }
        [DisplayName("نام کالا")]
        [Required(ErrorMessage = "نام کالا را وارد کنید")]
        public string  Name { get; set; }
        [DisplayName("واحد")]
        [Required(ErrorMessage = "نام واحد را وارد کنید")]
        public string  QuantityPerUnit { get; set; }

        [DisplayName("تعداد در بسته")]
        public decimal PackageCount { get; set; }

        [DisplayName("حداقل موجودی")]
        public decimal MinimumBalance { get; set; }

        [DisplayName("درصد فروش")]
        public decimal Fee { get; set; }

        public ICollection<ProductBill> ProductBills { get; set; }
        
        public virtual ProductCategory ProductCategory { get; set; }

        public int ProductCategoryId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public int? ManufacturerId { get; set; }

        public ICollection<StockItem> StockItems { get; set; }

        public ICollection<BuySlipItem> BuySlipItems { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; }

        public ICollection<BillItem> BillItems { get; set; }
    }
}
