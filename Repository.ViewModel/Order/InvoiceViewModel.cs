using Repository.DomainModel.Common;
using Repository.DomainModel.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel.Order
{
    public class InvoiceViewModel
    {        
        public int Id { get; set; }
        [DisplayName("تاریخ ثبت")]
        public string CreatedDate { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("تحویل گیرنده")]
        public string Reciver { get; set; }
        [DisplayName("نوع خروج کالا")]
        public string InvoiceTypeName { get; set; }

        public int? PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }

        public int? BillTypeId { get; set; }
        public string BillTypeName { get; set; }
        public string UserName { get; set; }
        public int? AppUserId { get; set; }
        public IList<InvoiceItemViewModel> InvoiceItems { get; set; }
        public About AboutFooter { get; set; }
        public int TotalPrice { get; set; }
    }
}
