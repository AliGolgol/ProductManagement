using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel.Entry
{
    public class BuySlipItemViewModel
    {
        [DisplayName("شناسه")]
        public int Id { get; set; }

        [DisplayName("مبلغ کلا")]
        public int Price { get; set; }

        [DisplayName("تعداد")]
        public int Quantity { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [DisplayName("نام کالا")]
        public string ProductName { get; set; }

        [DisplayName("قیمت")]
        public int PriceInQuantity { get; set; }

    }
}
