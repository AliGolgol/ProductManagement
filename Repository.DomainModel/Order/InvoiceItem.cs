using Repository.DomainModel.Catalog;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Order
{
    public class InvoiceItem
    {
        public int  Id { get; set; }
        [DisplayName("توضیحات")]
        public string  Description { get; set; }
        [DisplayName("تعداد")]
        [Required(ErrorMessage = "تعداد را وارد کنید")]
        public int  Quantity { get; set; }
        [DisplayName("مبلغ")]
        [Required(ErrorMessage = "مبلغ را وارد کنید")]
        public int  Price { get; set; }

        public virtual Product Product { get; set; }
        public int  ProductId { get; set; }

        public virtual Invoice  Invoice { get; set; }
        public int  InvoiceId { get; set; }

        public virtual Repository.Repository Repository { get; set; }
        public int? RepositoryId { get; set; }

    }
}
