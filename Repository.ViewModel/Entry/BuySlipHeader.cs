using System.ComponentModel;

namespace Repository.ViewModel.Entry
{
    public class BuySlipHeader
    {
        public int Id { get; set; }
        [DisplayName("تاریخ ثبت")]
        public string DateCreation { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("تحویل دهنده")]
        public string DeliveryMan { get; set; }
        [DisplayName("تعمین کننده")]
        public string Supplier { get; set; }
        [DisplayName("نام کاربر")]
        public string UserName { get; set; }
        [DisplayName("نوع ورود")]
        public string  EntrySlipType { get; set; }
    }
}
