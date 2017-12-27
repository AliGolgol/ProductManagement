using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel.Order
{
    public class InvoiceHeader
    {
        public int Id { get; set; }
        [DisplayName("تاریخ ثبت")]
        public string CreatedDate { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("تحویل گیرنده")]
        public string Reciver { get; set; }
        public string UserName { get; set; }
        public string PaymentTypeName { get; set; }
        public string BillTypeName { get; set; }

    }
}
