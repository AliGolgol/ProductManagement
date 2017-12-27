using System.ComponentModel;

namespace Repository.ViewModel.Order
{
    public class InvoiceItemViewModel
    {

        public int Id { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }
        [DisplayName("تعداد")]
        public int Quantity { get; set; }
        [DisplayName("قیمت واحد")]
        public int Price { get; set; }
        [DisplayName("نام کالا")]
        public string ProductName { get; set; }
        [DisplayName("قیمت")]
        public int PriceInQuantity { get; set; }

    }
}
