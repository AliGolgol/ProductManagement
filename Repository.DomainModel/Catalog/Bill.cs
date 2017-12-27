using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.AppUser;
using Repository.DomainModel.Common;
using Repository.DomainModel.Order;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Catalog
{
    public class Bill
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="لطفا تاریخ را وارد کنید")]
        [DisplayName("تاریخ ثبت")]
        public string CreatedDate { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("تحویل گیرنده")]
        public string Reciver { get; set; }

        public virtual Period.Period Period { get; set; }
        public int? PeriedId { get; set; }

        public virtual ApplicationUser ApplicattionUser { get; set; }
        public int? AppUserId { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public int? PaymentTypeId { get; set; }

        public virtual BillType BillType  { get; set; }
        public int?  BiilTypeId { get; set; }

        public ICollection<BillItem> BillItems  { get; set; }
    }
}
