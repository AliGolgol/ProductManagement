using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel.Common
{
    public class HomeViewModel<T>
    {
        [DisplayName("درخواست کالا")]
        public int OrderCount { get; set; }
        [DisplayName("حواله/فاکتور")]
        public int? InvoiceCount { get; set; }
        [DisplayName("رسید")]
        public int? BuySlipCount { get; set; }
        [DisplayName("درخواست خرید")]
        public int? BuyOrderCount { get; set; }
        [DisplayName("موجودی")]
        public int? StockCount { get; set; }
        public Int32 CurrentPage { get; set; }
        public Int32 PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((decimal)TotalRecords / PageSize);
            }
        }
        public IList<T> Content { get; set; }
    }
}
