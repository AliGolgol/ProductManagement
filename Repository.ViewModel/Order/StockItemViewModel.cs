using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DomainModel.Order;

namespace Repository.ViewModel.Order
{
    public class StockItemViewModel
    {
        public Stock Stocks { get; set; }
        public ICollection<DomainModel.Order.StockItem> StockItems { get; set; }
    }
}
