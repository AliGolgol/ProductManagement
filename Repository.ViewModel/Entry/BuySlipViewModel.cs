using Repository.DomainModel.Common;
using System.Collections.Generic;
using System.ComponentModel;

namespace Repository.ViewModel.Entry
{
    public class BuySlipViewModel
    {
        [DisplayName("شماره رسید")]
        public int Id { get; set; }
        [DisplayName("تاریخ ثبت")]
        public string DateCreation { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("تحویل دهنده")]
        public string DeliveryMan { get; set; }
        [DisplayName("تعمین کننده")]
        public string Supplier { get; set; }
        public int? SupplierId { get; set; }
        [DisplayName("نام کاربر")]
        public string UserName { get; set; }
        public int? AppUserId { get; set; }
        [DisplayName("نوع ورود")]
        public string EntrySlipType { get; set; }
        public int? EntrySlipTypeId { get; set; }
        public IList<BuySlipItemViewModel> BuySlipItems { get; set; }
        public About AboutFotter { get; set; }
        public int TotalPrice { get; set; }
    }
}
