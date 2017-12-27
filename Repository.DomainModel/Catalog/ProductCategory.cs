using System.Collections.Generic;
using System.ComponentModel;
using Repository.DomainModel.Order;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Catalog
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [DisplayName("نام کالا")]
        [Required(ErrorMessage = "نام کالا را وارد کنید")]
        public string Name { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [DisplayName("تعداد در بسته")]
        public decimal PackageCount { get; set; }

        [DisplayName("حداقل موجودی")]
        public decimal MinimumBalance { get; set; }

        [DisplayName("درصد فروش")]
        public decimal Fee { get; set; }

        [DisplayName("سطح پایانی است؟")]
        public bool IsLastLevel { get; set; }
        [DisplayName("گروه کالا")]
        public virtual ProductCategory Parent { get; set; }
        public int? ParentId { get; set; }

        //public ICollection<ProductCat> ProductCats { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<StockItem> StockItems { get; set; }

    }
}
