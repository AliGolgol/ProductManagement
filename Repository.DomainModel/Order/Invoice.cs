using Repository.DomainModel.AppUser;
using Repository.DomainModel.Catalog;
using Repository.DomainModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DomainModel.Order
{
    public class Invoice
    {
        public int Id { get; set; }
        [DisplayName("تاریخ ثبت")]
        [Required(ErrorMessage = "تاریخ ثبت را وارد کنید")]
        public string CreatedDate  { get; set; }
        [DisplayName("توضیحات")]
        public string  Description { get; set; }
        [DisplayName("تحویل گیرنده")]
        public string  Reciver { get; set; }

        public virtual Period.Period  Period { get; set; }
        public int? PeriedId { get; set; }

        public virtual ApplicationUser ApplicattionUser { get; set; }
        public int?  AppUserId { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public int? PaymentTypeId { get; set; }

        public virtual BillType  BillType { get; set; }
        public int? BillTypeId { get; set; }

        public ICollection<InvoiceItem> InvoicesItems { get; set; }

    }
}
