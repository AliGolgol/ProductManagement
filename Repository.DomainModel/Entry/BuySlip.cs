using Repository.DomainModel.AppUser;
using Repository.DomainModel.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Repository.DomainModel.Entry
{
    public class BuySlip
    {
        public int Id { get; set; }
        [DisplayName("تاریخ ثبت")]
        [Required(ErrorMessage = "تاریخ را وارد کنید")]
        public string DateCreation { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("تحویل دهنده")]
        public string DeliveryMan { get; set; }

        public virtual Supplier Supplier { get; set; }
        public int? SupplierId { get; set; }

        public virtual Period.Period Period { get; set; }
        public int? PeriodId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public int? AppUserId { get; set; }

        public virtual EntrySlipType  EntrySlipType { get; set; }
        public int? EntrySlipTypeId { get; set; }
        public ICollection<BuySlipItem> BuySlipItems { get; set; }

       
    }
}
